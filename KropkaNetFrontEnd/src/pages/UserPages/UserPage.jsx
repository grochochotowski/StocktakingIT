import React, { useState, useEffect, useContext } from 'react'
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';

import NavBar from '../../components/NavBar'

import '../../styles/index.css'
import '../../styles/user.css'
import '../../styles/form.css'

function UserPage() {

    const { state, setState } = useContext(GlobalStateContext);

    const [user, setUser] = useState({})
    const [updateUser, setUpdateUser] = useState({})

    async function fetchData() {
        const token = await refreshToken();
        let apiCall = `kropkaNet/user/${state.personId}`
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
        setUpdateUser({
            "name": user.name,
            "surname": user.surname,
            "email": user.email,
            "phoneNumber": user.phoneNumber,
            "note": user.note ? user.note : "",
        })
    }, [user])

    function handleInputChange(inputId) {
        setUser(prev => (
            {
                ...prev,
                [inputId]: document.getElementById(inputId).value
            }
        ))
    }

    async function updateDate(e) {
        e.preventDefault();
        const token = await refreshToken();
        let apiCall = `kropkaNet/user/update/${state.personId}`
        try {
            const response = await axiosInstance.put(apiCall, updateUser, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            fetchData()
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    return (
        <>
            <NavBar/>
            <div className="container user">
                <h1 className="title">User {user.id}</h1>
                <form>
                    <div className="layer row">
                        <div className="input-container">
                            <label htmlFor="name">Name:</label>
                            <input
                                type="text"
                                id="name"
                                onChange={() => handleInputChange("name")}
                                value={updateUser.name}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="surname">Surname:</label>
                            <input
                                type="text"
                                id="surname"
                                onChange={() => handleInputChange("surname")}
                                value={updateUser.surname}
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
                                value={updateUser.email}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="phoneNumber">Phone number:</label>
                            <input
                                type="text"
                                id="phoneNumber"
                                onChange={() => handleInputChange("phoneNumber")}
                                value={updateUser.phoneNumber}
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
                                value={updateUser.note}
                                placeholder="Notes"
                            />
                        </div>
                    </div>
                    <div className="finish">
                        <button onClick={updateDate}>Save</button>
                    </div>
                </form>
            </div>
        </>
    )
}

export default UserPage