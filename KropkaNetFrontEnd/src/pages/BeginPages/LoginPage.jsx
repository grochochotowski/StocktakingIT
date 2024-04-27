import { Link } from 'react-router-dom'

import '../../styles/index.css'
import '../../styles/form.css'

function LoginPage() {

    const login = () => {
        alert("Log in")
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
                        />
                    </div>
                    <div className="input-container">
                        <label htmlFor="password">Password:</label>
                        <input
                            type="password"
                            id="password"
                        />
                    </div>
                </div>
                <div className="finish">
                    <p>Do not have an account? <Link to="/register">Register</Link> for free.</p>
                    <button onClick={login}>Log in</button>
                </div>
            </form>
        </div>
    )
}

export default LoginPage