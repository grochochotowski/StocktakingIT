import '../../styles/error.css'

function NotFoundPage() {
    return (
        <div className="error-container">
            <h1>404</h1>
            <h2>Not found</h2>
            <p>The page you are looking for does not exist</p>
            <button>HOME</button>
        </div>
    )
}

export default NotFoundPage