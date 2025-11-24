import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import AddRestaurantModal from './AddRestaurantModal';
import api from '../../utils/axiosConfig';

function Home() {

    document.title = "Welcome";
    const [restaurants, setRestaurants] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [loading, setLoading] = useState(true);

    const fetchRestaurants = async () => {
        setLoading(true);
        const userId = localStorage.getItem("userId");
        try {
            const response = await api.get("restaurant/manager/" + userId);
            setRestaurants(response.data);
            console.log("restaurants: ", response.data);
        } catch (error) {
            console.log("Error home page: ", error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        console.log("Home component loaded. Links should be active.");
        fetchRestaurants();
    }, []);

    return (
        <section className='page'>
            <header className="flex-between" style={{ marginBottom: '2rem' }}>
                <div>
                    <h1>My Restaurants</h1>
                    <p style={{ color: 'var(--text-secondary)' }}>Manage your restaurant locations and menus</p>
                </div>
                <button className="btn" onClick={() => setShowModal(true)}>
                    + Add Restaurant
                </button>
            </header>

            {loading ? (
                <div className="flex-center" style={{ height: '200px' }}>
                    <p>Loading restaurants...</p>
                </div>
            ) : restaurants && restaurants.length > 0 ? (
                <div className="grid-responsive">
                    {restaurants.map(restaurant => (
                        <div key={restaurant.id} className="card" style={{ display: 'flex', flexDirection: 'column', justifyContent: 'space-between' }}>
                            <div>
                                <h3 style={{ marginBottom: '0.5rem' }}>{restaurant.name}</h3>
                                <p style={{ color: 'var(--text-secondary)', marginBottom: '1.5rem', display: '-webkit-box', WebkitLineClamp: 3, WebkitBoxOrient: 'vertical', overflow: 'hidden' }}>
                                    {restaurant.description}
                                </p>
                            </div>
                            <Link to={`/restaurant/${restaurant.id}`} className="btn secondary" style={{ textAlign: 'center' }}>
                                Manage Restaurant
                            </Link>
                        </div>
                    ))}
                </div>
            ) : (
                <div className='card flex-center' style={{ flexDirection: 'column', padding: '4rem 2rem', textAlign: 'center' }}>
                    <h3 style={{ marginBottom: '1rem' }}>No Restaurants Found</h3>
                    <p style={{ color: 'var(--text-secondary)', marginBottom: '2rem' }}>Get started by adding your first restaurant.</p>
                    <button className="btn" onClick={() => setShowModal(true)}>Add Restaurant</button>
                </div>
            )}

            {showModal && (
                <AddRestaurantModal
                    onClose={() => setShowModal(false)}
                    onSuccess={() => {
                        fetchRestaurants();
                        alert("Restaurant added successfully!");
                    }}
                />
            )}
        </section>
    );
}

export default Home;