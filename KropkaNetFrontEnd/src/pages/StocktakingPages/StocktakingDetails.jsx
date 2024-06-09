import React, { useState, useEffect, useContext } from 'react'
import { useParams } from 'react-router-dom'
import { axiosInstance, refreshToken } from '../../api/axios';
import { GlobalStateContext } from '../../GlobalState';

import NavBar from '../../components/NavBar'
import NavBarEmployee from '../../components/NavBarEmployee'
import ProductBox from '../../components/ProductBox'

import '../../styles/index.css'
import '../../styles/details.css'
import '../../styles/list.css'
import AddProduct from '../ProductPages/AddProduct';

function StocktakingDetails() {

    const { state, setState } = useContext(GlobalStateContext);
    const params = useParams();
    
    const [box, setBox] = useState("");

    const [changeData, setChangedata] = useState({"note" : "", "expectedTimeHours" : 0})

    const [newEmployee, setNewEmployee] = useState("")
	const [notInEmployees, setNotInEmployees] = useState([])
	const [sortingEmployee, setSortingEmployee] = useState(["id", 0])
    
    const [stocktaking, setStocktaking] = useState({})
    const [warehouse, setWarehouse] = useState([]);
    const [users, setUsers] = useState([]);
    const [employees, setEmployees] = useState([])

    async function fetchData() {
        const token = await refreshToken();
        getStocktaking(token)
        getWarehouse(token)
        getUsers(token)
        getEmployees(token)
        getNotInEmployees(token)
    }
    async function getStocktaking(token) {
		let apiCall = `kropkaNet/stocktaking/${params.stocktakingId}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setStocktaking(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }
    async function getWarehouse(token) {
		let apiCall = `kropkaNet/warehouse/${params.warehouseId}/products`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setWarehouse(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }
    async function getUsers(token) {
		let apiCall = `kropkaNet/user/GetFromOrder/${params.orderId}`;
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
    async function getEmployees(token) {
		let apiCall = `kropkaNet/employee/GetFromStocktaking/${params.stocktakingId}?` +
            `sortBy=${sortingEmployee[0]}&` +
            `sortDirection=${sortingEmployee[1] == 0 ? "ASC" : "DESC"}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setEmployees(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }
    async function getNotInEmployees(token) {
        let apiCall = `kropkaNet/employee/get/notInStocktaking?stocktakingId=${params.stocktakingId}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setNotInEmployees(response.data);
			setNewEmployee(response.data[0].id)
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    useEffect(() => {
        fetchData();
    }, [sortingEmployee])
    useEffect(() => {
        setChangedata({
            "note" : stocktaking.note != null ? stocktaking.note : "",
            "expectedTimeHours" : stocktaking.expectedTimeHours
        })
    }, [stocktaking])

    const formatDateTime = (dateString) => {
		const date = new Date(dateString);
		const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit' };
		return date.toLocaleString(undefined, options);
	};

    async function exportData() {
        const token = await refreshToken();
		let apiCall = `kropkaNet/warehouse/${params.warehouseId}/export`;
        try {
            const response = await axiosInstance.get(apiCall, {
                responseType: 'blob',
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            const blob = new Blob([response.data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

            const link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = `Products_${new Date().toISOString().slice(0, 19).replace(/:/g, '-')}.xlsx`;
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    function handleChange(input) {
        setChangedata(prev => ({
            ...prev,
            [input]: input == "expectedTimeHours" ? parseInt(document.getElementById(input).value) : document.getElementById(input).value
        }))
    }
    async function saveData() {
        const token = await refreshToken();
        let apiCall = `kropkaNet/stocktaking/update/${params.stocktakingId}`;
        try {
            const response = await axiosInstance.put(apiCall, changeData, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            fetchData()
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }



    async function addEmployee(e) {
		e.preventDefault();
		
		const token = await refreshToken();
        const apiCall = `kropkaNet/stocktaking/${params.stocktakingId}/addEmployee/${newEmployee}`
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
	async function handleDeleteEmployee(element) {
		const token = await refreshToken();
        const apiCall = `kropkaNet/stocktaking/${params.stocktakingId}/removeEmployee/${element}`
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
	}
    const handleEmployeeChange = (e) => setNewEmployee(e.target.value);
	function sortTableEmployee(column) {
        setSortingEmployee(prev => {
            if (prev[0] === column && prev[1] === 0) return [column, 1]
            return [column, 0]
        })
    }
	function generateHeaderEmployee() {
        return (
            <thead>
                <tr>
                    <th className="u-thin" onClick={() => sortTableEmployee("id")}>
                        {
                            sortingEmployee[0] == "id" &&
                            (
                                sortingEmployee[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        ID
                    </th>
                    <th className="wide" onClick={() => sortTableEmployee("name")}>
                        {
                            sortingEmployee[0] == "name" &&
                            (
                                sortingEmployee[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Name
                    </th>
                    <th className="wide" onClick={() => sortTableEmployee("surname")}>
                        {
                            sortingEmployee[0] == "surname" &&
                            (
                                sortingEmployee[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Surname
                    </th>
                    <th className="u-thin no-select"></th>
                </tr>
            </thead>
        );
    }
    function generateBodyEmployee() {
        return (
            <tbody>
                {employees.map((employee) => (
                    <tr className="not-clickable" key={employee.id} id={employee.id}>
                        <td className="u-thin">{employee.id}</td>
                        <td>{employee.name}</td>
                        <td>{employee.surname}</td>
						<td className="u-thin delete-item" onClick={() => handleDeleteEmployee(employee.id)}>
							<i className="fa-solid fa-trash-can"></i>
						</td>
                    </tr>
                ))}
            </tbody>
        )
    }

    useEffect(() => {
        function handleClickOutside(event) {
          if (event.target.closest(".outside-box") && !event.target.closest(".content")) setBox("");
        }
    
        document.addEventListener("click", handleClickOutside);
    
        return () => {
          document.removeEventListener("click", handleClickOutside);
        };
    }, []);

    return (
        <>
            { state.level == "employee" ? <NavBarEmployee /> : <NavBar /> }
            <div className="container">
                <div className="details">
                    <div className="stocktaking-header">
                        <h1>Stocktaking {stocktaking.id}</h1>
                        {
                            state.level == "employee" && <button onClick={saveData}>Save data</button>
                        }
                        <button onClick={exportData}>Export data</button>
                    </div>
                    <hr />
{/*date&time*/}     <div className="info-element">
                        <h2>Date & time</h2>
                        <p>Date: {formatDateTime(stocktaking.dateOfOrderExecution)}</p>
                        <p>Expected execution time:&nbsp;
                            {
                                state.level == "employee"
                                ? <input id="expectedTimeHours" className="stocktaking-input" type="text" value={changeData.expectedTimeHours} onChange={() => handleChange("expectedTimeHours")}/>
                                : stocktaking.expectedTimeHours
                            }
                        &nbsp;hours</p>
                    </div>
{/*note*/}          <div className="info-element">
                        <h2>Notes</h2>
                        {
                            state.level == "employee"
                            ? <textarea id="note" className="stocktaking-textarea" value={changeData.note} onChange={() => handleChange("note")}/>
                            : <p>{stocktaking.note}</p>
                        } 
                    </div>
{/*order*/}         <div className="info-element">
                        <h2>Order</h2>
                        <p>Company: {stocktaking.companyName}</p>
                        <p>Department: {stocktaking.departmentName}</p>
                    </div>
{/*address*/}       <div className="info-element">
                        <h2>Address</h2>
                        {stocktaking.address && (
                            <>
                                <p>{stocktaking.address.country}, {stocktaking.address.city}, {stocktaking.address.zipCode}</p>
                                <p>{stocktaking.address.street} {stocktaking.address.building}
                                    {stocktaking.address.premises ? ` / ${stocktaking.address.premises}` : ''}
                                </p>
                            </>
                        )}
                    </div>
{/*user&employee*/} <div className="info-element-double">
                        <div className="users">
                            <h2>Users</h2>
                            <ul>
                                {
                                    users.map((user) => (
                                        <li>{user.name} {user.surname}</li>
                                    ))
                                }
                            </ul>
                        </div>
                        <div className="employees">
                            <h2>Employees</h2>
                            {
                                state.level == "user" ?
                                <ul>
                                    {
                                        employees.map((employee) => (
                                            <li>{employee.name} {employee.surname}</li>
                                        ))
                                    }
                                </ul> :
                                <>
                                    <div className="list rows-5">
                                        <table>
                                            { generateHeaderEmployee() }
                                            { generateBodyEmployee() }
                                        </table>
                                    </div>
                                    <div className="new-object">
                                        <select name="employee" id="employee" onChange={handleEmployeeChange}>
                                            {notInEmployees.map(employee => (
                                                <option key={employee.id} value={employee.id}>{employee.name} {employee.surname}</option>
                                            ))}
                                        </select>
                                        <button type="submit" onClick={addEmployee}>Add</button>
                                    </div>
                                </>
                            }
                        </div>
                    </div>
                </div>
                <div className="warehouse">
                    <h1>Products</h1>
                    <hr />
                    {
                        state.level == "employee" && 
                            <div className="new-product" onClick={() => setBox("addProduct")}>
                                <i className="fa-solid fa-plus"></i>
                            </div>
                    }
                    {
                        warehouse.map((product) => (
                            <ProductBox key={product.id} product={product} warehouseId={params.warehouseId}/>
                        ))
                    }
                </div>
            </div>
            { box && box == "addProduct" && <AddProduct hideBox={() => setBox("")} warehouseId={params.warehouseId} updateData={() => fetchData()}/> }
        </>
    )
}

export default StocktakingDetails