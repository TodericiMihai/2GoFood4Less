import { useEffect } from 'react';
import api from '../../utils/axiosConfig';

function Login() {

    document.title = "Login";

    // dont ask an already logged in user to login over and over again
    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            document.location = "/";
        }
    }, []);

    async function loginHandler(e) {
        e.preventDefault();
        const form_ = e.target;
        const formData = new FormData(form_);
        const dataToSend = {};

        for (const [key, value] of formData) {
            dataToSend[key] = value;
        }

        if (dataToSend.Remember === "on") {
            dataToSend.Remember = true;
        }

        console.log("login data before send: ", dataToSend);

        try {
            const response = await api.post("/manager/auth/login", dataToSend);
            const data = response.data;

            if (data.token) {
                localStorage.setItem("token", data.token);
                localStorage.setItem("userId", data.user.id);
                document.location = "/";
            } else {
                throw new Error("No token received");
            }
        } catch (error) {
            const messageEl = document.querySelector(".message");
            if (error.response && error.response.data && error.response.data.message) {
                messageEl.innerHTML = error.response.data.message;
            } else {
                messageEl.innerHTML = "Something went wrong, please try again";
            }
            console.log("login error: ", error);
        }
    }

    return (
        <section className='page flex-center'>
            <div className='card' style={{ width: '100%', maxWidth: '400px' }}>
                <header style={{ textAlign: 'center', marginBottom: '2rem' }}>
                    <h1 style={{ fontSize: '2rem', color: 'var(--primary-color)' }}>Welcome Back</h1>
                    <p style={{ color: 'var(--text-secondary)' }}>Login to manage your restaurants</p>
                </header>
                <p className='message' style={{ color: 'var(--error-color)', textAlign: 'center', marginBottom: '1rem' }}></p>
                <div className='form-holder'>
                    <form action="#" className='login' onSubmit={loginHandler}>
                        <div className="form-group">
                            <label htmlFor="email">Email Address</label>
                            <input type="email" name='Email' id='email' placeholder="Enter your email" required />
                        </div>
                        <div className="form-group">
                            <label htmlFor="password">Password</label>
                            <input type="password" name='Password' id='password' placeholder="Enter your password" required />
                        </div>
                        <div className="form-group" style={{ display: 'flex', alignItems: 'center', gap: '0.5rem' }}>
                            <input type="checkbox" name='Remember' id='remember' style={{ width: 'auto' }} />
                            <label htmlFor="remember" style={{ margin: 0, cursor: 'pointer' }}>Remember me</label>
                        </div>

                        <input type="submit" value="Login" className='btn' style={{ width: '100%', marginTop: '1rem' }} />
                    </form>
                </div>
                <div style={{ marginTop: '2rem', textAlign: 'center', color: 'var(--text-secondary)' }}>
                    <span>Don't have an account? </span>
                    <a href="/register" style={{ fontWeight: '600' }}>Register here</a>
                </div>
            </div>
        </section>
    );
}

export default Login;