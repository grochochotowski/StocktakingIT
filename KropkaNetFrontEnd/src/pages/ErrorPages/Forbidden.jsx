import '../../styles/error.css'

function Forbidden() {
    return (
        <div className="error-container">
            <h1>403</h1>
            <h2>Forbidden</h2>
            <p>You are not allowed to be on this page</p>
            <button>HOME</button>
        </div>
    )
}

export default Forbidden