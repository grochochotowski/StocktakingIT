import {Link} from 'react-router-dom'

import '../styles/navbar.css'

function NavBar() {

    const handleSettings = () => {
        alert("Settings")
    }
    const handleLogOut = () => {
        alert("Log out")
    }

    return (
        <nav>
            <div className='options'>
                <Link to='/'>
                    <h2><i className="fa-solid fa-house"></i></h2>
                    <h4>Home</h4>
                </Link>
                <Link to='/orders'>
                    <h2><i className="fa-solid fa-basket-shopping"></i></h2>
                    <h4>Orders</h4>
                </Link>
                <Link to='/account'>
                    <h2><i className="fa-solid fa-user"></i></h2>
                    <h4>Account</h4>
                </Link>
            </div>
            <div className="bottom-row">
                <button onClick={handleSettings}>
                    <h6>Settings</h6>
                    <h2><i className="fa-solid fa-gear"></i></h2>
                </button>
                <button onClick={handleLogOut}>
                    <h6>Log out</h6>
                    <h2><i className="fa-solid fa-right-from-bracket"></i></h2>
                </button>
            </div>
        </nav>
    )
}

export default NavBar