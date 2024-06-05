import React, { useState } from 'react'

function OrderNew({ hideBox, updateData }) {

	const [orders, setOrders] = useState({
		"id": 3,
		"dateOfOrderExecution": "2024-05-27T20:48:41.712",
		"state": 1,
		"departmentName": "string2003",
		"stocktakingId": null
	})
	const [orderDate, setOrderDate] = useState({
		"dateOfOrderExecution": orders.dateOfOrderExecution
	});

	const handleDateChange = (e) => setOrderDate({"dateOfOrderExecution": e.target.value});
	
	const handleSubmit = (e) => {
		e.preventDefault();
		console.log(orderDate);
		hideBox();
	};

	const handleDelete = () => {
		alert("delete")
	}

	return (
		<div className="outside-box">
			<div className="content">
				<h1>Update order</h1>
				<form>
					<input 
						type="datetime-local" 
						id="order-date" 
						name="order-date" 
						value={orderDate.dateOfOrderExecution} 
						onChange={handleDateChange}
					/>
					<button type="submit" onClick={handleSubmit}>Update</button>
					<button className="warning" onClick={handleDelete}>Delete</button>
				</form>
			</div>
		</div>
	)
}

export default OrderNew