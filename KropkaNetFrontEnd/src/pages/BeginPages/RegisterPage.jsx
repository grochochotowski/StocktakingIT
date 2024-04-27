import { Link } from 'react-router-dom'

import '../../styles/index.css'
import '../../styles/form.css'

function RegisterPage() {

    const register = () => {
        alert("Register")
    }

    return (
        <div className="main-container account">
            <div className="form-header">
                <h1>Create account</h1>
            </div>
            <form>
                <div className="layer row">
                    <div className="input-container">
                        <label htmlFor="firstName">Name:</label>
                        <input
                            type="text"
                            id="firstName"
                        />
                    </div> {/* name */}
                    <div className="input-container">
                        <label htmlFor="lastName">Surname:</label>
                        <input
                            type="text"
                            id="password"
                        />
                    </div> {/* surname */}
                </div>
                <div className="layer">
                    <div className="input-container">
                        <label htmlFor="login">Login:</label>
                        <input
                            type="text"
                            id="login"
                        />
                    </div> {/* login */}
                    <div className="input-container">
                        <label htmlFor="password">Password:</label>
                        <input
                            type="password"
                            id="password"
                        />
                    </div> {/* password */}
                    <div className="input-container">
                        <label htmlFor="confirmPassword">Confirm password:</label>
                        <input
                            type="password"
                            id="confirmPassword"
                        />
                    </div> {/* confirm password */}
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