import React, { useState, useContext, useEffect } from 'react';
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';

function OrderNew({ hideBox, updateData }) {

    const { state, setState } = useContext(GlobalStateContext);

	const [departments, setDepartments] = useState([])
	const [selectedDepartment, setSelectedDepartment] = useState('');
	const [orderDate, setOrderDate] = useState('');

	async function fetchData() {
        const token = await refreshToken();
        const apiCall = `kropkaNet/department/user/${state.personId}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setDepartments(response.data);
			setSelectedDepartment(response.data[0].id)
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    useEffect(() => {
      fetchData();
    }, [])

	const handleDepartmentChange = (e) => setSelectedDepartment(e.target.value);
	const handleDateChange = (e) => setOrderDate(e.target.value);
	
	async function handleSubmit(e) {
		e.preventDefault();
		const dataToSend = {
			dateOfOrderExecution: orderDate + ":00.000Z",
			departmentId: parseInt(selectedDepartment)
		};
		
		const token = await refreshToken();
        var apiCall = `kropkaNet/order/create`
		if (state.level == "user") {
			apiCall += `?userId=${state.personId}`;
		}
        try {
            const response = await axiosInstance.post(apiCall, dataToSend, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
			updateData();
			hideBox();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
	};

	return (
		<div className="outside-box">
			<div className="content">
				<h1>New order</h1>
				<form>
					<select name="department" id="department" onChange={handleDepartmentChange}>
						{departments.map(department => (
							<option key={department.id} value={department.id}>{department.companyName} - {department.departmentName}</option>
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