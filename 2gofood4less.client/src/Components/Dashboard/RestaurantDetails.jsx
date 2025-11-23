import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import AddMenuModal from './AddMenuModal';

function RestaurantDetails() {
    const { id } = useParams();
    const [restaurant, setRestaurant] = useState(null);
    const [menus, setMenus] = useState([]);
    const [loading, setLoading] = useState(true);
    const [showMenuModal, setShowMenuModal] = useState(false);

    const fetchRestaurantData = () => {
        setLoading(true);
        // Fetch restaurant details
        fetch("/api/restaurant/" + id, {
            method: "GET",
            credentials: "include"
        }).then(response => response.json()).then(data => {
            setRestaurant(data);
            // Fetch menus after getting restaurant
            return fetch("/api/menu/restaurant/" + id, {
                method: "GET",
                credentials: "include"
            });
        }).then(response => {
            if (response.ok) {
                return response.json();
            }
            return [];
        }).then(data => {
            setMenus(data);
            setLoading(false);
        }).catch(error => {
            console.log("Error details page: ", error);
            setLoading(false);
        });
    };

    useEffect(() => {
        fetchRestaurantData();
    }, [id]);

    if (loading) return <div>Loading...</div>;
    if (!restaurant) return <div>Restaurant not found. <Link to="/">Go Home</Link></div>;

    return (
        <section className='page'>
            <header>
                <h1>{restaurant.name}</h1>
                <Link to="/" className="btn">Back to Home</Link>
            </header>
            <div className="details-content">
                <p><strong>Description:</strong> {restaurant.description}</p>
                <p><strong>Food Type:</strong> {restaurant.foodType}</p>
            </div>

            <div className="menu-section">
                <div className="section-header">
                    <h2>Menus</h2>
                    <button className="btn" onClick={() => setShowMenuModal(true)}>Add Menu</button>
                </div>
                {menus && menus.length > 0 ? (
                    <table>
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Description</th>
                            </tr>
                        </thead>
                        <tbody>
                            {menus.map(menu => (
                                <tr key={menu.id}>
                                    <td>
                                        <Link to={`/menu/${menu.id}`} style={{ color: 'blue', textDecoration: 'underline', fontWeight: 'bold', cursor: 'pointer' }}>
                                            {menu.name}
                                        </Link>
                                    </td>
                                    <td>{menu.description}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                ) : (
                    <p>No menus found.</p>
                )}
            </div>

            {showMenuModal && (
                <AddMenuModal
                    restaurantId={id}
                    onClose={() => setShowMenuModal(false)}
                    onSuccess={() => {
                        fetchRestaurantData();
                        alert("Menu added successfully!");
                    }}
                />
            )}
        </section>
    );
}

export default RestaurantDetails;
