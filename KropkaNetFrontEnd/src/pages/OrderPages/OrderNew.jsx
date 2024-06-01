import React, { useState } from 'react'

function OrderNew({ hideNew, updateData }) {

	const [departments, setDepartments] = useState([
		{
			"id": 1,
			"departmentName": "name1"
		},
		{
			"id": 2,
			"departmentName": "name2"
		},
		{
			"id": 3,
			"departmentName": "name3"
		},
		{
			"id": 4,
			"departmentName": "name4"
		},
		{
			"id": 5,
			"departmentName": "name5"
		},
	])
	const [selectedDepartment, setSelectedDepartment] = useState('');
	const [orderDate, setOrderDate] = useState('');

	const handleDepartmentChange = (e) => setSelectedDepartment(e.target.value);

	const handleDateChange = (e) => setOrderDate(e.target.value);
	
	const handleSubmit = (e) => {
		e.preventDefault();
		const dataToSend = {
			dateOfOrderExecution: orderDate,
			departmentId: selectedDepartment
		};
		console.log(dataToSend);
		hideNew();
	};

	return (
		<div className="outside-box">
			<div className="content">
				<h1>New order</h1>
				<form>
					<select name="department" id="department" onChange={handleDepartmentChange}>
						{departments.map(department => (
							<option key={department.id} value={department.id}>{department.departmentName}</option>
						))}
					</select>
					<input 
						type="datetime-local" 
						id="order-date" 
						name="order-date" 
						value={orderDate} 
						onChange={handleDateChange}
					/>
					<button type="submit" onClick={handleSubmit}>Create</button>
				</form>
			</div>
		</div>
	)
}

export default OrderNew