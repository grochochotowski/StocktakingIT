import React, { useState } from 'react'

function CompanyEdit({ hideBox, updateData }) {

	const [company, setCompany] = useState({
		"id": 2003,
		"nip": "testcompany",
		"krs": "testcompany",
		"companyName": "testcompany",
		"note": "testcompany",
		"addressId": 4003,
		"address": {
		  "id": 4003,
		  "country": "testcompany",
		  "city": "testcompany",
		  "zipCode": "testcompany",
		  "street": "testcompany",
		  "building": "testcompany",
		  "premises": "testcompany"
		},
		"departments": [],
		"users": []
	})

	const [companyEdit, setCompanyEdit] = useState({
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

	const [departments, setDepartments] = useState([
		{ "id": 1, "departmentName": "name1" },
		{ "id": 2, "departmentName": "name2" },
		{ "id": 3, "departmentName": "name3" },
		{ "id": 4, "departmentName": "name4" },
		{ "id": 5, "departmentName": "name5" },
		{ "id": 1, "departmentName": "name1" },
		{ "id": 2, "departmentName": "name2" },
		{ "id": 3, "departmentName": "name3" },
		{ "id": 4, "departmentName": "name4" },
		{ "id": 5, "departmentName": "name5" },
		{ "id": 1, "departmentName": "name1" },
		{ "id": 2, "departmentName": "name2" },
		{ "id": 3, "departmentName": "name3" },
		{ "id": 4, "departmentName": "name4" },
		{ "id": 5, "departmentName": "name5" },
	])
	const [users, setUsers] = useState([
		{ "id": 1, "name": "name1", "surname": "surname1" },
		{ "id": 2, "name": "name2", "surname": "surname2" },
		{ "id": 3, "name": "name3", "surname": "surname3" },
		{ "id": 4, "name": "name4", "surname": "surname4" },
		{ "id": 5, "name": "name5", "surname": "surname5" },
		{ "id": 1, "name": "name1", "surname": "surname1" },
		{ "id": 2, "name": "name2", "surname": "surname2" },
		{ "id": 3, "name": "name3", "surname": "surname3" },
		{ "id": 4, "name": "name4", "surname": "surname4" },
		{ "id": 5, "name": "name5", "surname": "surname5" },
		{ "id": 1, "name": "name1", "surname": "surname1" },
		{ "id": 2, "name": "name2", "surname": "surname2" },
		{ "id": 3, "name": "name3", "surname": "surname3" },
		{ "id": 4, "name": "name4", "surname": "surname4" },
		{ "id": 5, "name": "name5", "surname": "surname5" },
	])
	
	const handleSubmit = (e) => {
		e.preventDefault();
		//console.log(dataToSend);
	};
	const addDepartment = (e) => {
		e.preventDefault();
		console.log(newDepartment);
	};

    function handleInputChange(inputId) {
        setCompanyEdit(prev => (
            {
                ...prev,
                [inputId]: document.getElementById(inputId).value
            }
        ))
    }

	const [addUser, setAddUser] = useState("")
	const [newDepartment, setNewDepartment] = useState("")
	const handleUserChange = (e) => setAddUser(e.target.value);

	function handleDeleteUser(element) {
		alert(`deleted user ${element}`)
	}
	function handleDeleteDepartment(element) {
		alert(`deleted department ${element}`)
	}

    const [sortingUser, setSortingUser] = useState(["id", 0])
	const [sortingDepartment, setSortingDepartment] = useState(["id", 0])
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
								{users.map(user => (
									<option key={user.id} value={user.id}>{user.name} {user.surname}</option>
								))}
							</select>
							<button type="submit" onClick={addDepartment}>Add</button>
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
								id="newDepartment"
								onChange={() => setNewDepartment(document.getElementById("newDepartment").value)}
								value={newDepartment}
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