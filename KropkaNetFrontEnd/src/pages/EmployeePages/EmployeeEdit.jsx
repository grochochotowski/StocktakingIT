import React, { useState, useContext, useEffect } from 'react';
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';

function EmployeeEdit({ updateData, selected }) {

    const { state, setState } = useContext(GlobalStateContext);

	const [employee, setEmployee] = useState({})
	const [employeeEdit, setEmployeeEdit] = useState({})
	const [newPos, setNewPos] = useState({})

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
            console.log(response.data)
            setEmployee(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    useEffect(() => {
        fetchData();
    }, [])
	useEffect(() => {
        if (employee) {
            updateInputs();
        }
        if (employee.position != null) {
            setNewPos(employee.positionId)
        }
    }, [employee]);

    function updateInputs() {
		setEmployeeEdit({
			"name": employee.name,
            "surname": employee.surname,
            "email": employee.email,
            "phoneNumber": employee.phoneNumber,
            "note": employee.note ? employee.note : "",
		})
	}
	
	async function handleSubmit(e) {
		e.preventDefault();
        const token = await refreshToken();
		let apiCall = `kropkaNet/employee/update/${selected}`;
        try {
            const response = await axiosInstance.put(apiCall, employeeEdit,{
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setEmployee(response.data);
			fetchData();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
	};
    function handleInputChange(inputId) {
        setEmployeeEdit(prev => (
            {
                ...prev,
                [inputId]: document.getElementById(inputId).value
            }
        ))
    }
    
    function handleSelectChange(e) {
        setNewPos(e.target.value)
    }
    useEffect(() => {
        async function updatePosition() {
            const token = await refreshToken();
            let apiCall = `kropkaNet/employee/changeposition/${selected}?positionId=${newPos}`;
            try {
                const response = await axiosInstance.patch(apiCall, null, {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                });
                updateData();
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        }
        updatePosition()
    }, [newPos])
    

    return (
        <div className="outside-box">
			<div className="content big scroll">
            <h1 className="title">Employee {employee.id}</h1>
                <form>
                    <div className="layer row">
                        <div className="input-container">
                            <label htmlFor="name">Name:</label>
                            <input
                                type="text"
                                id="name"
                                onChange={() => handleInputChange("name")}
                                value={employeeEdit.name}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="surname">Surname:</label>
                            <input
                                type="text"
                                id="surname"
                                onChange={() => handleInputChange("surname")}
                                value={employeeEdit.surname}
                            />
                        </div>
                        {
                            state.position == "Admin" &&
                            <div className="input-container">
                                <label htmlFor="pos">Position:</label>
                                <select name="pos" id="pos" value={newPos} onChange={handleSelectChange}>
                                    <option value="4">Employee</option>
                                    <option value="5">Moderator</option>
                                    <option value="6">Admin</option>
                                </select>
                            </div>
                        }
                    </div>
                    <div className="layer row">
                        <div className="input-container">
                            <label htmlFor="email">E-mail:</label>
                            <input
                                type="text"
                                id="email"
                                onChange={() => handleInputChange("email")}
                                value={employeeEdit.email}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="phoneNumber">Phone number:</label>
                            <input
                                type="text"
                                id="phoneNumber"
                                onChange={() => handleInputChange("phoneNumber")}
                                value={employeeEdit.phoneNumber}
                            />
                        </div>
                    </div>
                    <div className="layer">
                        <label htmlFor="note">Notes:</label>
                        <div className="input-container">
                            <textarea
                                type="text"
                                id="note"
                                onChange={() => handleInputChange("note")}
                                value={employeeEdit.note}
                                placeholder="Notes"
                            />
                        </div>
                    </div>
                    <div className="finish">
                        <button onClick={handleSubmit}>Save</button>
                    </div>
                </form>
			</div>
		</div>
    )
}

export default EmployeeEdit