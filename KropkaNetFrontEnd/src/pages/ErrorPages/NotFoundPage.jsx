import { Link } from 'react-router-dom'

import '../../styles/error.css'

function NotFoundPage() {
    return (
        <div className="error-container">
            <h1>404</h1>
            <h2>Not found</h2>
            <p>The page you are looking for does not exist</p>
            <Link to="/" className="button">HOME</Link>
        </div>
    )
}

export default NotFoundPage