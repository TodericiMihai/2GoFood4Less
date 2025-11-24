import { useState, useEffect } from "react";
import { Outlet, Navigate } from "react-router-dom";
import api from "./utils/axiosConfig";

function ProtectedRoutes() {

    const [isLogged, setIsLogged] = useState(false);
    const [waiting, setWaiting] = useState(true);

    useEffect(() => {
        const verifyAuth = async () => {
            const token = localStorage.getItem('token');

            if (!token) {
                setIsLogged(false);
                setWaiting(false);
                return;
            }

            try {
                const response = await api.get('/manager/auth/me');

                if (response.status === 200) {
                    localStorage.setItem("userId", response.data.id);
                    setIsLogged(true);
                } else {
                    setIsLogged(false);
                    localStorage.removeItem('token');
                    localStorage.removeItem('user');
                    localStorage.removeItem('userId');
                }
            } catch (error) {
                console.error('Error protected routes:', error);
                setIsLogged(false);
                localStorage.removeItem('token');
                localStorage.removeItem('user');
                localStorage.removeItem('userId');
            } finally {
                setWaiting(false);
            }
        };

        verifyAuth();
    }, []);

    if (waiting) {
        return (
            <div className="waiting-page">
                <div>Waiting...</div>
            </div>
        );
    }

    return isLogged ? <Outlet /> : <Navigate to="/login" replace />;
}

export default ProtectedRoutes;