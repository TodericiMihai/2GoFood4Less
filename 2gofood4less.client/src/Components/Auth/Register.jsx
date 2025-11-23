import { useEffect } from 'react';

function Register() {

    document.title = "Register";

    // dont ask an already registered user to register over and over again
    useEffect(() => {
        const user = localStorage.getItem("user");
        if (user) {
            document.location = "/";
        }
    }, []);

    return (
        <section className='register-page-wrapper page'>
            <div className='register-page'>
                <header>
                    <h1>Register Page</h1>
                </header>
                <p className='message'></p>
                <div className='form-holder'>
                    <form action="#" className='register' onSubmit={registerHandler}>
                        <label htmlFor="name">Name</label>
                        <br />
                        <input type="text" name='Name' id='name' required />
                        <br />
                        <label htmlFor="email">Email</label>
                        <br />
                        <input type="email" name='Email' id='email' required />
                        <br />
                        <label htmlFor="password">Password</label>
                        <br />
                        <input type="password" name='Password' id='password' required />

                        <br />
                        <input type="submit" value="Register" className='register btn' />
                    </form>
                </div>
                <div className='my-5'>
                    <span>Or </span>
                    <a href="/login">Login</a>
                </div>
            </div>
        </section>
    );
    async function registerHandler(e) {
        e.preventDefault();
        const form_ = e.target, submitter = document.querySelector("input.login");

        // const formData = new FormData(form_, submitter); dataToSend = {};

        // for (const [key, value] of formData) {
        //     dataToSend[key] = value;
        // }

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

        console.log("login error: ", data);
    }
}

export default Register;