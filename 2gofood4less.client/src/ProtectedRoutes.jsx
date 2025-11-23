import { useState, useEffect } from "react";
import { Outlet, Navigate } from "react-router-dom";

function ProtectedRoutes() {

    const [isLogged, setIsLogged] = useState(false);
    const [waiting, setWaiting] = useState(true);

    useEffect(() => {
        fetch('/api/manager/auth/me', { credentials: "include" })
            .then(response => {
                if (!response.ok) throw new Error("Not authenticated");
                return response.json();
            })
            .then(data => {
                // /me returns the user directly
                //  localStorage.setItem("user", data.email);
                localStorage.setItem("userId", data.id);
                setIsLogged(true);
            })
            .catch(err => {
                console.log("Error protected routes: ", err);
                localStorage.removeItem("user");
                localStorage.removeItem("userId");
                setIsLogged(false);
            })
            .finally(() => setWaiting(false));
    }, []);

    return waiting ? <div className="waiting-page">
        <div>Waiting...</div>
    </div> :
        isLogged ? <Outlet /> : <Navigate to="/login" />;
}

export default ProtectedRoutes;