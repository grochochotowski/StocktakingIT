import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import { axiosInstance } from '../../api/axios'

import '../../styles/index.css'
import '../../styles/form.css'

function RegisterPage() {

    const navigate = useNavigate();

    

    const [messageBoxOpt, setMessageBoxOpt] = useState({
        "active": false,
        "header" : "",
        "message" : "",
        "type" : ""
    })

    const [newUser, setNewUser] = useState({
        "login": "",
        "password": "",
        "confirmPassword": "",
        "name": "",
        "surname": "",
        "email": "",
        "phoneNumber": ""
    })
    async function register(e) {
        e.preventDefault();
        try {
            const response = await axiosInstance.post('account/user/register', JSON.stringify(newUser));

            navigate("/login")

        } catch (error) {
            setMessageBoxOpt(
                {
                    "active": true,
                    "header" : error.code,
                    "message" : error.message,
                    "type" : "error"
                }
            )
            console.error(error);
        }
    }

    function handleInputChange(inputId) {
        setNewUser(prev => (
            {
                ...prev,
                [inputId]: document.getElementById(inputId).value
            }
        ))
    }

    return (
        <div className="main-container account">
            <div className="form-header">
                <h1>Create account</h1>
            </div>
            <form>
                <div className="layer row">
                    <div className="input-container">
                        <label htmlFor="name">Name:</label>
                        <input
                            type="text"
                            id="name"
                            onChange={() => handleInputChange("name")}
                            value={newUser.name}
                        />
                    </div> {/* name */}
                    <div className="input-container">
                        <label htmlFor="surname">Surname:</label>
                        <input
                            type="text"
                            id="surname"
                            onChange={() => handleInputChange("surname")}
                            value={newUser.surname}
                        />
                    </div> {/* surname */}
                </div>
                <div className="layer row">
                    <div className="input-container">
                        <label htmlFor="email">E-mail:</label>
                        <input
                            type="text"
                            id="email"
                            onChange={() => handleInputChange("email")}
                            value={newUser.email}
                        />
                    </div> {/* name */}
                    <div className="input-container">
                        <label htmlFor="phoneNumber">Phone Number:</label>
                        <input
                            type="text"
                            id="phoneNumber"
                            onChange={() => handleInputChange("phoneNumber")}
                            value={newUser.phoneNumber}
                        />
                    </div> {/* surname */}
                </div>
                <div className="layer">
                    <div className="input-container">
                        <label htmlFor="login">Login:</label>
                        <input
                            type="text"
                            id="login"
                            onChange={() => handleInputChange("login")}
                            value={newUser.login}
                        />
                    </div> {/* login */}
                    <div className="input-container">
                        <label htmlFor="password">Password:</label>
                        <input
                            type="text"
                            id="password"
                            onChange={() => handleInputChange("password")}
                            value={newUser.password}
                        />
                    </div> {/* password */}
                    <div className="input-container">
                        <label htmlFor="confirmPassword">Confirm Password:</label>
                        <input
                            type="text"
                            id="confirmPassword"
                            onChange={() => handleInputChange("confirmPassword")}
                            value={newUser.confirmPassword}
                        />
                    </div> {/* confirmPassword */}
                </div>
                <div className="finish">
                    <p>Already have an account? <Link to="/login">Log in</Link> instead.</p>
                    <button onClick={register}>Register</button>
                </div>
            </form>
        </div>
    )
}

export default RegisterPage