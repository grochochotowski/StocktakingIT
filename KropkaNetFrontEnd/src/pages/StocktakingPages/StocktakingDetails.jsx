import React, { useState, useEffect } from 'react'
import { useParams } from 'react-router-dom'
import { axiosInstance, refreshToken } from '../../api/axios';

import NavBar from '../../components/NavBar'
import ProductBox from '../../components/ProductBox'

import '../../styles/index.css'
import '../../styles/details.css'

function StocktakingDetails() {

    const params = useParams();
    
    const [stocktaking, setStocktaking] = useState({})
    const [warehouse, setWarehouse] = useState([]);
    const [users, setUsers] = useState([]);

    async function fetchData() {
        getStocktaking()
        getWarehouse()
        getUsers()
    }
    async function getStocktaking() {
        const token = await refreshToken();
		let apiCall = `kropkaNet/stocktaking/${params.orderId}`;
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
    async function getWarehouse() {
        const token = await refreshToken();
		let apiCall = `kropkaNet/warehouse/${stocktaking.warehouseId}/products`;
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
    async function getUsers() {
        const token = await refreshToken();
		let apiCall = `kropkaNet/user/${params.id}/getAll`;
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
    async function getEmployees() {

    }

    useEffect(() => {
        fetchData();
      }, [])



    const [employees, setEmployees] = useState([
        { "name": "name1", "surname": "surname1" },
        { "name": "name2", "surname": "surname2" },
        { "name": "name3", "surname": "surname3" },
        { "name": "name4", "surname": "surname4" }
    ])

    const formatDateTime = (dateString) => {
		const date = new Date(dateString);
		const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit' };
		return date.toLocaleString(undefined, options);
	};

    return (
        <>
            <NavBar />
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