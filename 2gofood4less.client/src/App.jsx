import { Route, RouterProvider, createBrowserRouter, createRoutesFromElements } from 'react-router-dom';
import ProtectedRoutes from './ProtectedRoutes';
import './App.css'
import Home from './Components/Dashboard/Home'
import RestaurantDetails from './Components/Dashboard/RestaurantDetails'
import RestaurantOrders from './Components/Dashboard/RestaurantOrders'
import MenuDetails from './Components/Dashboard/MenuDetails'
import Login from './Components/Auth/Login'
import Register from './Components/Auth/Register'
import api from './utils/axiosConfig';


const router = createBrowserRouter(
    createRoutesFromElements(
        <Route path='/'>
            <Route element={<ProtectedRoutes />}>
                <Route path='/' element={<Home />} />
                <Route path='/restaurant/:id' element={<RestaurantDetails />} />
                <Route path='/restaurant/:id/orders' element={<RestaurantOrders />} />
                <Route path='/menu/:id' element={<MenuDetails />} />
            </Route>
            <Route path='/login' element={<Login />} />
            <Route path='/register' element={<Register />} />
            <Route path='*' element={
                <div>
                    <header>
                        <h1>Not Found</h1>
                    </header>
                    <p>
                        <a href="/">Back to Home</a>
                    </p>
                </div>
            } />
        </Route>
    )
);
function App() {
    const isLogged = localStorage.getItem("token");
    const logout = async () => {
        try {
            await api.get("/manager/auth/logout");
        } catch (error) {
            console.error('Logout error:', error);
        } finally {
            localStorage.removeItem("token");
            localStorage.removeItem("user");
            localStorage.removeItem("userId");
            document.location = "/login";
        }
    };

    return (
        <section>
            <div className='top-nav'>
                {
                    isLogged ?
                        <span className='item-holder'>
                            <a href="/">Home</a>
                            <span onClick={logout}>Log Out</span>
                        </span> :
                        <span className='item-holder'>
                            <a href="/login">Login</a>
                            <a href="/register">Register</a>
                        </span>
                }
            </div>

            <RouterProvider router={router} />
        </section>
    );
}

export default App
