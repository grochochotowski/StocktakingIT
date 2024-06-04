import React, { useState } from 'react'

import NavBar from '../../components/NavBar'

import '../../styles/index.css'
import '../../styles/user.css'
import '../../styles/form.css'

function User() {

    const [user, setUser] = useState({
        "id": 3002,
        "name": "userName",
        "surname": "userSurname",
        "personalNumber": "userNumber",
        "email": "userEmail",
        "phoneNumber": "userPhone",
        "note": "userNote"
    })

    function handleInputChange(inputId) {
        setUser(prev => (
            {
                ...prev,
                [inputId]: document.getElementById(inputId).value
            }
        ))
    }

    function update() {
        alert("update")
    }

    return (
        <>
            <NavBar/>
            <div className="container">
                <h1 className="title">User {user.id}</h1>
                <form>
                    <div className="layer row">
                        <div className="input-container">
                            <label htmlFor="name">Name:</label>
                            <input
                                type="text"
                                id="name"
                                onChange={() => handleInputChange("name")}
                                value={user.name}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="surname">Surname:</label>
                            <input
                                type="text"
                                id="surname"
                                onChange={() => handleInputChange("surname")}
                                value={user.surname}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="personalNumber">Personal Number:</label>
                            <input
                                type="text"
                                id="personalNumber"
                                onChange={() => handleInputChange("personalNumber")}
                                value={user.personalNumber}
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
                                value={user.email}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="phoneNumber">Phone number:</label>
                            <input
                                type="text"
                                id="phoneNumber"
                                onChange={() => handleInputChange("phoneNumber")}
                                value={user.phoneNumber}
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
                                value={user.note}
                                placeholder="Notes"
                            />
                        </div>
                    </div>
                    <div className="finish">
                        <button onClick={update}>Save</button>
                    </div>
                </form>
            </div>
        </>
    )
}

export default User