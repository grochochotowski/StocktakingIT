import {Link} from 'react-router-dom'

import '../styles/navbar.css'

function NavBar() {
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
                <Link to='/settings'>
                    <h2><i className="fa-solid fa-gear"></i></h2>
                    <h4>Setting</h4>
                </Link>
            </div>
        </nav>
    )
}

export default NavBar