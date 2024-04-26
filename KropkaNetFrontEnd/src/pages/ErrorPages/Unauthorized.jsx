import '../../styles/error.css'

function Unauthorized() {
    return (
        <div className="error-container">
            <h1>401</h1>
            <h2>Unauthorized</h2>
            <p>Please log in before continuing</p>
            <button>HOME</button>
        </div>
    )
}

export default Unauthorized