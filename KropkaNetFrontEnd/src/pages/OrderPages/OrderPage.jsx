import React, { useState } from 'react'
import { Link } from 'react-router-dom'

import NavBar from '../../components/NavBar'

import '../../styles/order.css'

function OrderPage() {

    const [selected, setSelected] = useState(1);

    const [orders, setOrders] = useState([
        {
        "id" : 1,
        "dateOfOrderExecution" : "01/01/0001",
        "departmentName" : "Department 1"
        },
        {
        "id" : 2,
        "dateOfOrderExecution" : "02/02/0002",
        "departmentName" : "Department 2"
        },
        {
        "id" : 3,
        "dateOfOrderExecution" : "03/03/0003",
        "departmentName" : "Department 3"
        },
        {
        "id" : 4,
        "dateOfOrderExecution" : "04/04/0004",
        "departmentName" : "Department 4"
        },
        {
        "id" : 5,
        "dateOfOrderExecution" : "05/05/0005",
        "departmentName" : "Department 5"
        },
    ])

    


    return (
        <>
            <NavBar />
            <div className="order-container">
                <div className="list">
                    {generateHeader}
                    {generateBody}
                </div>
                <div className="options">
                    <Link to="/orders/list">List</Link>
                    <Link to={`/orders/details/${selected}`} className={selected ? "" : "disable"}>Details</Link>
                    <Link to={`/orders/edit/${selected}`} className={selected ? "" : "disable"}>Edit</Link>
                </div>
            </div>
        </>
    )
}

export default OrderPage