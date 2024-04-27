import '../../styles/index.css'
import '../../styles/form.css'

function LoginPage() {

    const login = () => {
        alert("Log in")
    }
    return (
        <div className="main-container">
            <form>
                <div className="layer">
                    <div className="input-container">
                        <label htmlFor="login">Login:</label>
                        <input
                            type="text"
                            id="login"
                        />
                    </div>
                    <div className="input-container">
                        <label htmlFor="password">Password:</label>
                        <input
                            type="text"
                            id="password"
                        />
                    </div>
                </div>
                <button onClick={login}>Log in</button>
            </form>
        </div>
    )
}

export default LoginPage