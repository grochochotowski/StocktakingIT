import {Link} from 'react-router-dom'

import logo from '../../assets/logo.png'

import '../../styles/index.css'

function WelcomePage() {
    return (
        <div className="main-container">
            <main className="welcome">
                <div className="logo">
                    <img src={logo} alt="kropka-net-logo" />
                </div>
                <div className="controlls">
                    <Link to="/login" className="button">Log in</Link>
                    <Link to="/register" className="button">Register</Link>
                </div>
                <div className="bg"></div>
            </main>
        </div>
    )
}

export default WelcomePage