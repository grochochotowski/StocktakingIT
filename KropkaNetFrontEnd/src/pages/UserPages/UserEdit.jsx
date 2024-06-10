import React, { useState, useContext, useEffect } from 'react';
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';

function UserEdit({ updateData, selected }) {

    const { state, setState } = useContext(GlobalStateContext);

	const [user, setUser] = useState({})
	const [userEdit, setUserEdit] = useState({})

    async function fetchData() {
        const token = await refreshToken();
        getUser(token)
    }

    async function getUser(token) {
        let apiCall = `kropkaNet/user/${selected}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setUser(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    useEffect(() => {
        fetchData();
    }, [])
	useEffect(() => {
        if (user) {
            updateInputs();
        }
    }, [user]);

    function updateInputs() {
		setUserEdit({
			"name": user.name,
            "surname": user.surname,
            "email": user.email,
            "phoneNumber": user.phoneNumber,
            "note": user.note ? user.note : "",
		})
	}
	
	async function handleSubmit(e) {
		e.preventDefault();
        const token = await refreshToken();
		let apiCall = `kropkaNet/user/update/${selected}`;
        try {
            const response = await axiosInstance.put(apiCall, userEdit,{
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setUser(response.data);
			fetchData();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
	};
    function handleInputChange(inputId) {
        setUserEdit(prev => (
            {
                ...prev,
                [inputId]: document.getElementById(inputId).value
            }
        ))
    }

    return (
        <div className="outside-box">
			<div className="content big scroll">
            <h1 className="title">User {user.id}</h1>
                <form>
                    <div className="layer row">
                        <div className="input-container">
                            <label htmlFor="name">Name:</label>
                            <input
                                type="text"
                                id="name"
                                onChange={() => handleInputChange("name")}
                                value={userEdit.name}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="surname">Surname:</label>
                            <input
                                type="text"
                                id="surname"
                                onChange={() => handleInputChange("surname")}
                                value={userEdit.surname}
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
                                value={userEdit.email}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="phoneNumber">Phone number:</label>
                            <input
                                type="text"
                                id="phoneNumber"
                                onChange={() => handleInputChange("phoneNumber")}
                                value={userEdit.phoneNumber}
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
                                value={userEdit.note}
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

export default UserEdit