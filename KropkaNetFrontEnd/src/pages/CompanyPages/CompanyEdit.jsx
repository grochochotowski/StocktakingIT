import React, { useState, useContext, useEffect } from 'react';
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';

function CompanyEdit({updateData, selected}) {

    const { state, setState } = useContext(GlobalStateContext);

	const [company, setCompany] = useState({})
	const [departments, setDepartments] = useState([])
	const [users, setUsers] = useState([])
	const [notInUsers, setNotInUsers] = useState([])
	
	const [companyEdit, setCompanyEdit] = useState({})
	const [newUser, setNewUser] = useState("")
	const [newDepartment, setNewDepartment] = useState({"departmentName": ""})

    const [sortingUser, setSortingUser] = useState(["id", 0])
	const [sortingDepartment, setSortingDepartment] = useState(["id", 0])

    async function fetchData() {
        const token = await refreshToken();
        getCompany(token)
        getDepartments(token)
        getUsers(token)
        getNotInUsers(token)
    }

    async function getCompany(token) {
        let apiCall = `kropkaNet/company/${selected}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setCompany(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }
    async function getDepartments(token) {
        let apiCall = `kropkaNet/department/company/${selected}?` +
        `sortBy=${sortingDepartment[0]}&` +
        `sortDirection=${sortingDepartment[1] == 0 ? "ASC" : "DESC"}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setDepartments(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }
    async function getUsers(token) {
        let apiCall = `kropkaNet/user/getFromCompany/${selected}?` +
            `sortBy=${sortingUser[0]}&` +
            `sortDirection=${sortingUser[1] == 0 ? "ASC" : "DESC"}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setUsers(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }
    async function getNotInUsers(token) {
        let apiCall = `kropkaNet/user/getAll/list?companyId=${selected}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setNotInUsers(response.data);
			setNewUser(response.data[0].id)
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    useEffect(() => {
		fetchData();
    }, [sortingUser, sortingDepartment])
	useEffect(() => {
        if (company.address) {
            updateInputs();
        }
    }, [company]);

	function updateInputs() {
		setCompanyEdit({
			"nip": company.nip,
			"krs": company.krs,
			"companyName": company.companyName,
			"note": company.note,
			"country": company.address.country,
			"city": company.address.city,
			"zipCode": company.address.zipCode,
			"street": company.address.street,
			"building": company.address.building,
			"premises": company.address.premises
		})
	}
	
	async function handleSubmit(e) {
		e.preventDefault();
        const token = await refreshToken();
		let apiCall = `kropkaNet/company/update/${selected}`;
        try {
            const response = await axiosInstance.put(apiCall, companyEdit,{
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setCompany(response.data);
			updateData();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
	};
	async function addDepartment(e) {
		e.preventDefault();
		
		const token = await refreshToken();
        const apiCall = `kropkaNet/department/create?companyId=${selected}`;
        try {
            const response = await axiosInstance.post(apiCall, newDepartment, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

			fetchData();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
	};
	async function addUser(e) {
		e.preventDefault();
		
		const token = await refreshToken();
        const apiCall = `kropkaNet/company/addUser?userId=${newUser}&companyId=${selected}`;
        try {
            const response = await axiosInstance.patch(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

			fetchData();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
	};


	async function handleDeleteUser(element) {
		const token = await refreshToken();
        const apiCall = `kropkaNet/company/removeUser/?userId=${element}&companyId=${selected}`;
        try {
            const response = await axiosInstance.patch(apiCall, newDepartment, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

			fetchData();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
	}
	async function handleDeleteDepartment(element) {
		const token = await refreshToken();
        const apiCall = `kropkaNet/department/delete/${element}`;
        try {
            const response = await axiosInstance.delete(apiCall, newDepartment, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

			fetchData();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
	}


	const handleUserChange = (e) => setNewUser(e.target.value);
    function handleInputChange(inputId) {
        setCompanyEdit(prev => (
            {
                ...prev,
                [inputId]: document.getElementById(inputId).value
            }
        ))
    }
	function handleInputChange(inputId) {
        setNewDepartment(prev => (
            {
                ...prev,
                [inputId]: document.getElementById(inputId).value
            }
        ))
    }



	function sortTableUser(column) {
        setSortingUser(prev => {
            if (prev[0] === column && prev[1] === 0) return [column, 1]
            return [column, 0]
        })
    }
	function sortTableDepartment(column) {
        setSortingDepartment(prev => {
            if (prev[0] === column && prev[1] === 0) return [column, 1]
            return [column, 0]
        })
    }
    
	function generateHeaderUser() {
        return (
            <thead>
                <tr>
                    <th className="u-thin" onClick={() => sortTableUser("id")}>
                        {
                            sortingUser[0] == "id" &&
                            (
                                sortingUser[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        ID
                    </th>
                    <th className="wide" onClick={() => sortTableUser("name")}>
                        {
                            sortingUser[0] == "name" &&
                            (
                                sortingUser[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Name
                    </th>
                    <th className="wide" onClick={() => sortTableUser("surname")}>
                        {
                            sortingUser[0] == "surname" &&
                            (
                                sortingUser[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Surname
                    </th>
                    <th className="u-thin"></th>
                </tr>
            </thead>
        );
    }
    function generateHeaderDepartment() {
        return (
            <thead>
                <tr>
                    <th className="u-thin" onClick={() => sortTableDepartment("id")}>
                        {
                            sortingDepartment[0] == "id" &&
                            (
                                sortingDepartment[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        ID
                    </th>
                    <th className="wide" onClick={() => sortTableDepartment("departmentName")}>
                        {
                            sortingDepartment[0] == "departmentName" &&
                            (
                                sortingDepartment[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Name
                    </th>
                    <th className="u-thin"></th>
                </tr>
            </thead>
        );
    }
    function generateBodyUser() {
        return (
            <tbody>
                {users.map((user) => (
                    <tr className="not-clickable" key={user.id} id={user.id}>
                        <td className="u-thin">{user.id}</td>
                        <td>{user.name}</td>
                        <td>{user.surname}</td>
						<td className="u-thin delete-item" onClick={() => handleDeleteUser(user.id)}>
							<i className="fa-solid fa-trash-can"></i>
						</td>
                    </tr>
                ))}
            </tbody>
        )
    }
    function generateBodyDepartment() {
        return (
            <tbody>
                {departments.map((department) => (
                    <tr className="not-clickable" key={department.id} id={department.id}>
                        <td className="u-thin">{department.id}</td>
                        <td>{department.departmentName}</td>
						<td className="u-thin delete-item" onClick={() => handleDeleteDepartment(department.id)}>
							<i className="fa-solid fa-trash-can"></i>
						</td>
                    </tr>
                ))}
            </tbody>
        )
    }

	return (
		<div className="outside-box">
			<div className="content big scroll">
				<div className="top">
					<h1>Edit company</h1>
					<form>
						<h4>Company data</h4>
						<div className="layer row">
                	        <div className="input-container">
                	            <label htmlFor="companyName">Company name:</label>
                	            <input
                	                type="text"
                	                id="companyName"
                	                onChange={() => handleInputChange("companyName")}
                	                value={companyEdit.companyName}
                	            />
                	        </div>
                	        <div className="input-container">
                	            <label htmlFor="nip">NIP:</label>
                	            <input
                	                type="text"
                	                id="nip"
                	                onChange={() => handleInputChange("nip")}
                	                value={companyEdit.nip}
                	            />
                	        </div>
                	        <div className="input-container">
                	            <label htmlFor="krs">KRS:</label>
                	            <input
                	                type="text"
                	                id="krs"
                	                onChange={() => handleInputChange("krs")}
                	                value={companyEdit.krs}
                	            />
                	        </div>
                	    </div>
						<h4>Addres</h4>
						{
							company.address != null &&
							<>
								<div className="layer row">
									<div className="input-container">
										<label htmlFor="country">Country:</label>
										<input
											type="text"
											id="country"
											onChange={() => handleInputChange("country")}
											value={companyEdit.country}
										/>
									</div>
									<div className="input-container">
										<label htmlFor="city">City:</label>
										<input
											type="text"
											id="city"
											onChange={() => handleInputChange("city")}
											value={companyEdit.city}
										/>
									</div>
									<div className="input-container">
										<label htmlFor="zipCode">Zip code:</label>
										<input
											type="text"
											id="zipCode"
											onChange={() => handleInputChange("zipCode")}
											value={companyEdit.zipCode}
										/>
									</div>
								</div>
								<div className="layer row">
									<div className="input-container">
										<label htmlFor="street">Street:</label>
										<input
											type="text"
											id="street"
											onChange={() => handleInputChange("street")}
											value={companyEdit.street}
										/>
									</div>
									<div className="input-container">
										<label htmlFor="building">Building:</label>
										<input
											type="text"
											id="building"
											onChange={() => handleInputChange("building")}
											value={companyEdit.building}
										/>
									</div>
									<div className="input-container">
										<label htmlFor="premises">Premises:</label>
										<input
											type="text"
											id="premises"
											onChange={() => handleInputChange("premises")}
											value={companyEdit.premises}
										/>
									</div>
								</div>
							</>
						}
						<h4>Notes</h4>
						<div className="layer row">
                	        <div className="input-container">
                	            <textarea
                	                type="text"
                	                id="note"
                	                onChange={() => handleInputChange("note")}
                	                value={companyEdit.note}
                	            />
                	        </div>
                	    </div>
						<button type="submit" onClick={handleSubmit}>Update</button>
					</form>
				</div>
				<div className="bottom divide">
					<div className="half">
						<h3>Users</h3>
						<div className="list rows-5">
							<table>
								{ generateHeaderUser() }
								{ generateBodyUser() }
							</table>
						</div>
						<div className="new-object">
							<select name="user" id="user" onChange={handleUserChange}>
								{notInUsers.map(user => (
									<option key={user.id} value={user.id}>{user.name} {user.surname}</option>
								))}
							</select>
							<button type="submit" onClick={addUser}>Add</button>
						</div>
					</div>
					<div className="half">
						<h3>Departments</h3>
						<div className="list rows-5">
							<table>
								{ generateHeaderDepartment() }
								{ generateBodyDepartment() }
							</table>
						</div>
						<div className="new-object">
							<input
								type="text"
								id="departmentName"
								onChange={() => handleInputChange("departmentName")}
								value={newDepartment.departmentName}
							/>
							<button type="submit" onClick={addDepartment}>Create</button>
						</div>
					</div>
				</div>
			</div>
		</div>
	)
}

export default CompanyEdit