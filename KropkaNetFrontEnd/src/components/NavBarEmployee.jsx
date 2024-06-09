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
                <Link to='/orders'>
                    <h2><i className="fa-solid fa-basket-shopping"></i></h2>
                    <h4>Orders</h4>
                </Link>
                <Link to='/companies'>
                    <h2><i className="fa-solid fa-building"></i></h2>
                    <h4>Companies</h4>
                </Link>
                <Link to='/employees'>
                    <h2><i className="fa-solid fa-building-user"></i></h2>
                    <h4>Employees</h4>
                </Link>
                <Link to='/users'>
                    <h2><i className="fa-solid fa-users"></i></h2>
                    <h4>Users</h4>
                </Link>
                <Link to='/products'>
                    <h2><i className="fa-solid fa-box"></i></h2>
                    <h4>Products</h4>
                </Link>
            </div>
            <div className="bottom-row">
                <Link to={`/account/${state.personId}`}>
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