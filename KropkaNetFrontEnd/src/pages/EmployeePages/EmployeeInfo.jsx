import React, { useState, useContext, useEffect } from 'react';
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';

function EmployeeInfo({selected}) {

    const { state, setState } = useContext(GlobalStateContext);

    const [employee, setEmployee] = useState({})
    const [position, setPosition] = useState("")

    async function fetchData() {
        const token = await refreshToken();
        getEmployee(token)
    }

    async function getEmployee(token) {
        let apiCall = `kropkaNet/employee/${selected}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setEmployee(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    useEffect(() => {
        fetchData();
    }, [])

    useEffect(() => {
        if (employee.position != null) {
            setPosition(employee.position.name)
        }
    }, [employee.position])
    

    return (
        <div className="outside-box">
			<div className="content">
				<h1>Employee {employee.id}</h1>
				<div className="info-box">
					<div className="info-line">
						<h4>Name:</h4>
						<p>{employee.name} {employee.surname}</p>
					</div>
					<div className="info-line">
						<h4>Position:</h4>
						<p>{position}</p>
					</div>
					<div className="info-line">
						<h4>Email:</h4>
						<p>{employee.email}</p>
					</div>
					<div className="info-line">
						<h4>Phone number:</h4>
						<p>{employee.phoneNumber}</p>
					</div>
					<div className="info-line">
						<h4>Notes:</h4>
                        <p>{employee.note}</p>
					</div>
				</div>
			</div>
		</div>
    )
}

export default EmployeeInfo