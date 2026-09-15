import React, { useState, useContext, useEffect } from 'react';
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';

function UserNew({ hideBox, updateData }) {

    const { state, setState } = useContext(GlobalStateContext);

	const [newUserData, setNewUserData] = useState({
        "login": "",
        "password": "",
        "confirmPassword": "",
        "name": "",
        "surname": "",
        "email": "",
        "phoneNumber": "",
        "note": "",
	})
	
	async function handleSubmit(e) {
		e.preventDefault();
		
		const token = await refreshToken();
        const apiCall = `account/user/register`;
        try {
            const response = await axiosInstance.post(apiCall, newUserData, {
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

    function handleInputChange(inputId) {
        const value = document.getElementById(inputId).value;
        setNewUserData(prev => {
            if (inputId === "password") {
                return {
                    ...prev,
                    password: value,
                    confirmPassword: value,
                };
            }
            return {
                ...prev,
                [inputId]: value,
            };
        });
    }

    return (
        <div className="outside-box">
			<div className="content big scroll">
            <h1 className="title">New user</h1>
                <form>
                    <div className="layer row">
                        <div className="input-container">
                            <label htmlFor="login">Login:</label>
                            <input
                                type="text"
                                id="login"
                                onChange={() => handleInputChange("login")}
                                value={newUserData.login}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="password">Password:</label>
                            <input
                                type="text"
                                id="password"
                                onChange={() => handleInputChange("password")}
                                value={newUserData.password}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="name">Name:</label>
                            <input
                                type="text"
                                id="name"
                                onChange={() => handleInputChange("name")}
                                value={newUserData.name}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="surname">Surname:</label>
                            <input
                                type="text"
                                id="surname"
                                onChange={() => handleInputChange("surname")}
                                value={newUserData.surname}
                            />
                        </div>
                    </div>
                    <div className="layer row">
                        <div className="input-container">
                            <label htmlFor="email">E-mail:</label>
                            <input
                                type="text"
                                id="email"
                                onChange={() => handleInputChange("email")}
                                value={newUserData.email}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="phoneNumber">Phone number:</label>
                            <input
                                type="text"
                                id="phoneNumber"
                                onChange={() => handleInputChange("phoneNumber")}
                                value={newUserData.phoneNumber}
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
                                value={newUserData.note}
                                placeholder="Notes"
                            />
                        </div>
                    </div>
                    <div className="finish">
                        <button onClick={handleSubmit}>Create</button>
                    </div>
                </form>
			</div>
		</div>
    )
}

export default UserNew
