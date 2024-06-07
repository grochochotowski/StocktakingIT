import React, { useState, useEffect, useContext } from 'react'
import { useParams } from 'react-router-dom'
import { axiosInstance, refreshToken } from '../../api/axios';
import { GlobalStateContext } from '../../GlobalState';

import NavBar from '../../components/NavBar'
import NavBarEmployee from '../../components/NavBarEmployee'
import ProductBox from '../../components/ProductBox'

import '../../styles/index.css'
import '../../styles/details.css'

function StocktakingDetails() {

    const { state, setState } = useContext(GlobalStateContext);
    const params = useParams();
    
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
		let apiCall = `company/employee/${params.stocktakingId}/GetFromStocktaking`;
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

    useEffect(() => {
        fetchData();
    }, [])

    const formatDateTime = (dateString) => {
		const date = new Date(dateString);
		const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit' };
		return date.toLocaleString(undefined, options);
	};

    return (
        <>
            { state.level == "employee" ? <NavBarEmployee /> : <NavBar /> }
            <div className="container">
                <div className="details">
                    <h1>Stocktaking {stocktaking.id}</h1>
                    <hr />
                    <div className="info-element">
                        <h2>Date & time</h2>
                        <p>Date: {formatDateTime(stocktaking.dateOfOrderExecution)}</p>
                        <p>Expected execution time: {stocktaking.expectedTimeHours} hours</p>
                    </div>
                    <div className="info-element">
                        <h2>Notes</h2>
                        <p>{stocktaking.note}</p>
                    </div>
                    <div className="info-element">
                        <h2>Order</h2>
                        <p>Company: {stocktaking.companyName}</p>
                        <p>Department: {stocktaking.departmentName}</p>
                    </div>
                    <div className="info-element">
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
                    <div className="info-element-double">
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
                            <ul>
                                {
                                    employees.map((employee) => (
                                        <li>{employee.name} {employee.surname}</li>
                                    ))
                                }
                            </ul>
                        </div>
                    </div>
                </div>
                <div className="warehouse">
                    <h1>Products</h1>
                    <hr />
                    {
                        warehouse.map((product) => (
                            <ProductBox key={product.id} product={product} />
                        ))
                    }
                </div>
            </div>
        </>
    )
}

export default StocktakingDetails