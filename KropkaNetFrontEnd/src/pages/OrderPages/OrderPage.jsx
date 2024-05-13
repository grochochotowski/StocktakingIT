import React, { useState } from 'react'
import { Link } from 'react-router-dom'

import NavBar from '../../components/NavBar'

import '../../styles/order.css'

function OrderPage() {


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