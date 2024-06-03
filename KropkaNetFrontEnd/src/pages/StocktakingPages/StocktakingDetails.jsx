import React, { useState } from 'react'

import NavBar from '../../components/NavBar'
import ProductBox from '../../components/ProductBox'

import '../../styles/details.css'

function StocktakingDetails() {

    const [stocktaking, setStocktaking] = useState({
        "id" : 1,
        "expectedTimeHours": 6,
        "note" : "Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, labore odit asperiores minima impedit dolorum officia similique temporibus nulla neque, commodi nemo ab nostrum quaerat in libero assumenda est id.",
        "dateOfOrderExecution": "2024-05-27T20:48:41.712",
        "companyName": "Company name",
        "departmentName": "Department name",
        "address": {
            "country" : "Address element",
            "city" : "Address element",
            "zipCode" : "Address element",
            "street" : "Address element",
            "building" : "Address element",
            "premises" : "Address element",
        }
    })

    const [warehouse, setWarehouse] = useState([
        { "id": 1, "category": "PC-category-1", "name": "PC-name-1", "quantity": 1 },
        { "id": 2, "category": "PC-category-2", "name": "PC-name-2", "quantity": 2 },
        { "id": 3, "category": "PC-category-3", "name": "PC-name-3", "quantity": 3 },
        { "id": 4, "category": "PC-category-4", "name": "PC-name-4", "quantity": 4 },
        { "id": 5, "category": "PC-category-5", "name": "PC-name-5", "quantity": 5 }
    ]);

    const [users, setUsers] = useState([
        { "name": "name1", "surname": "surname1" },
        { "name": "name2", "surname": "surname2" },
        { "name": "name3", "surname": "surname3" },
        { "name": "name4", "surname": "surname4" }
    ]);

    const [employees, setEmployees] = useState([
        { "name": "name1", "surname": "surname1" },
        { "name": "name2", "surname": "surname2" },
        { "name": "name3", "surname": "surname3" },
        { "name": "name4", "surname": "surname4" }
    ])

    const formatDateTime = (dateString) => {
		const date = new Date(dateString);
		const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit', second: '2-digit' };
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
                        <p>{stocktaking.address.country}, {stocktaking.address.city}, {stocktaking.address.zipCode}</p>
                        <p>{stocktaking.address.street} {stocktaking.address.building}
                        {stocktaking.address.premises != null ? " / " + stocktaking.address.premises : ""}</p>
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