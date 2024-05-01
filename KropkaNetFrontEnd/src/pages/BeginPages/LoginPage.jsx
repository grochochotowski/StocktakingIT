import { useState } from 'react'
import { Link, useNavigate } from 'react-router-dom'
import useAuth from '../../hooks/useAuth'
import instance from '../../api/axios'

import '../../styles/index.css'
import '../../styles/form.css'
import MessageBox from '../../components/MessageBox'

function LoginPage() {

    const [messageBoxOpt, setMessageBoxOpt] = useState({
        "active": false,
        "header" : "",
        "message" : "",
        "type" : ""
    })
    const { setAuth } = useAuth();
    const navigate = useNavigate();
    
    const [inputs, setInputs] = useState({
        "login" : "",
        "password" : ""
    })

    function handleInputChange(inputId) {
        setInputs(prev => (
            {
                ...prev,
                [inputId]: document.getElementById(inputId).value
            }
        ))
    }

    async function login(event) {
        event.preventDefault();
        try {
            const response = await instance().post('/account/login', JSON.stringify(inputs), {
                headers: {'Content-Type': 'application/json'}
            });
            const token = response?.data?.token
            setAuth({inputs, token})
            navigate("/dashboard")
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

    return (
        <div className="main-container account">
            <div className="form-header">
                <h1>Log in to your account</h1>
            </div>
            <form>
                <div className="layer">
                    <div className="input-container">
                        <label htmlFor="login">Login:</label>
                        <input
                            type="text"
                            id="login"
                            onChange={() => handleInputChange("login")}
                            value={inputs.login}
                        />
                    </div> {/* login */}
                    <div className="input-container">
                        <label htmlFor="password">Password:</label>
                        <input
                            type="password"
                            id="password"
                            onChange={() => handleInputChange("password")}
                            value={inputs.password}
                        />
                    </div> {/* password */}
                </div>
                <div className="finish">
                    <p>Do not have an account? <Link to="/register">Register</Link> for free.</p>
                    <button onClick={login}>Log in</button>
                </div>
            </form>
            { messageBoxOpt.active && <MessageBox header={messageBoxOpt.header} message={messageBoxOpt.message} type={messageBoxOpt.type}/> }
        </div>
    )
}

export default LoginPage