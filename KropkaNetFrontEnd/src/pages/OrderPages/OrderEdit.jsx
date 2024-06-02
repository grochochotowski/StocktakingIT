import React, { useState } from 'react'

function OrderNew({ hideBox, updateData }) {

	const [orderDate, setOrderDate] = useState('');

	const handleDateChange = (e) => setOrderDate(e.target.value);
	
	const handleSubmit = (e) => {
		e.preventDefault();
		const dataToSend = {
			dateOfOrderExecution: orderDate
		};
		console.log(dataToSend);
		hideBox();
	};

	const handleDelete = () => {
		alert("delete")
	}

	return (
		<div className="outside-box">
			<div className="content">
				<h1>New order</h1>
				<form>
					<input 
						type="datetime-local" 
						id="order-date" 
						name="order-date" 
						value={orderDate} 
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