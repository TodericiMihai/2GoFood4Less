import { useEffect } from 'react';

function Register() {

    document.title = "Register";

    // dont ask an already registered user to register over and over again
    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            document.location = "/";
        }
    }, []);

    async function registerHandler(e) {
        e.preventDefault();
        const form_ = e.target;

        const dataToSend = {
            Name: form_.name.value,
            Email: form_.email.value,
            UserName: form_.name.value.replace(/\s+/g, ''),
            password: form_.password.value,
            Password: form_.password.value
        };

        // create username
        const newUserName = dataToSend.Name.trim().split(" ");
        dataToSend.UserName = newUserName.join("");

        try {
            const response = await fetch("api/manager/auth/register", {
                method: "POST",
                credentials: "include",
                body: JSON.stringify(dataToSend),
                headers: {
                    "content-type": "Application/json",
                    "Accept": "application/json"
                }
            });

            const data = await response.json();

            if (response.ok) {
                document.location = "/login";
            }

            const messageEl = document.querySelector(".message");
            if (data.message) {
                messageEl.innerHTML = data.message;
            } else if (data.errors && Array.isArray(data.errors)) {
                let errorMessages = "<div>Attention please:</div><div class='normal'>";
                data.errors.forEach(error => {
                    errorMessages += error.description + " ";
                });

                errorMessages += "</div>";
                messageEl.innerHTML = errorMessages;
            } else {
                messageEl.innerHTML = "<div>An error occurred during registration. Please try again.</div>";
            }

            console.log("register response: ", data);
        } catch (error) {
            console.error("register error: ", error);
            const messageEl = document.querySelector(".message");
            messageEl.innerHTML = "<div>An error occurred during registration. Please try again.</div>";
        }
    }

    return (
        <section className='page flex-center'>
            <div className='card' style={{ width: '100%', maxWidth: '400px' }}>
                <header style={{ textAlign: 'center', marginBottom: '2rem' }}>
                    <h1 style={{ fontSize: '2rem', color: 'var(--primary-color)' }}>Create Account</h1>
                    <p style={{ color: 'var(--text-secondary)' }}>Join us to manage your food business</p>
                </header>
                <p className='message' style={{ color: 'var(--error-color)', textAlign: 'center', marginBottom: '1rem' }}></p>
                <div className='form-holder'>
                    <form action="#" className='register' onSubmit={registerHandler}>
                        <div className="form-group">
                            <label htmlFor="name">Full Name</label>
                            <input type="text" name='Name' id='name' placeholder="John Doe" required />
                        </div>
                        <div className="form-group">
                            <label htmlFor="email">Email Address</label>
                            <input type="email" name='Email' id='email' placeholder="john@example.com" required />
                        </div>
                        <div className="form-group">
                            <label htmlFor="password">Password</label>
                            <input type="password" name='Password' id='password' placeholder="Create a strong password" required />
                        </div>

                        <input type="submit" value="Register" className='btn' style={{ width: '100%', marginTop: '1rem' }} />
                    </form>
                </div>
                <div style={{ marginTop: '2rem', textAlign: 'center', color: 'var(--text-secondary)' }}>
                    <span>Already have an account? </span>
                    <a href="/login" style={{ fontWeight: '600' }}>Login here</a>
                </div>
            </div>
        </section>
    );
}

export default Register;