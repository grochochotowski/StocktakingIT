import { useContext } from 'react';
import {Link} from 'react-router-dom'
import { GlobalStateContext } from '../GlobalState';

import '../styles/navbar.css'

import logo from '../assets/logo.png'

function NavBarEmployee() {

    const { state, setState } = useContext(GlobalStateContext);

    const handleLogOut = () => {
        localStorage.removeItem('auth');
            
        setState({
            ...state,
            "isLoggedIn": false,
            "level": null,
            "personId" : null
        });
    }

    return (
        <nav>
            <div className="logo-container">
                <img src={logo} alt="kropka-net-logo" />
            </div>
            <div className='options'>
                <Link to='/dashboard'>
                    <h2><i className="fa-solid fa-house"></i></h2>
                    <h4>Home</h4>
                </Link>
                <Link to='/orders'>
                    <h2><i className="fa-solid fa-basket-shopping"></i></h2>
                    <h4>Orders</h4>
                </Link>
                <Link to='/companies'>
                    <h2><i className="fa-solid fa-building"></i></h2>
                    <h4>Companies</h4>
                </Link>
            </div>
            <div className="bottom-row">
                <Link to='/account/1'>
                    <h6>Account</h6>
                    <h2><i className="fa-solid fa-user"></i></h2>
                </Link>
                <button onClick={handleLogOut}>
                    <h6>Log out</h6>
                    <h2><i className="fa-solid fa-right-from-bracket"></i></h2>
                </button>
            </div>
        </nav>
    )
}

export default NavBarEmployee