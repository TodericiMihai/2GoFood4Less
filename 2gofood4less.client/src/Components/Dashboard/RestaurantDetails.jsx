import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import AddMenuModal from './AddMenuModal';
import api from '../../utils/axiosConfig';

function RestaurantDetails() {
    const { id } = useParams();
    const [restaurant, setRestaurant] = useState(null);
    const [menus, setMenus] = useState([]);
    const [loading, setLoading] = useState(true);
    const [showMenuModal, setShowMenuModal] = useState(false);

    const fetchRestaurantData = async () => {
        setLoading(true);
        try {
            // Fetch restaurant details
            const restResponse = await api.get("/restaurant/" + id);
            setRestaurant(restResponse.data);

            // Fetch menus
            const menuResponse = await api.get("/menu/restaurant/" + id);
            setMenus(menuResponse.data || []);
        } catch (error) {
            console.log("Error details page: ", error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchRestaurantData();
    }, [id]);

    if (loading) return <div className="page flex-center">Loading...</div>;
    if (!restaurant) return <div className="page flex-center">Restaurant not found. <Link to="/">Go Home</Link></div>;

    return (
        <section className='page'>
            <header className="flex-between" style={{ marginBottom: '2rem' }}>
                <div style={{ display: 'flex', alignItems: 'center', gap: '1rem' }}>
                    <Link to="/" className="btn secondary" style={{ padding: '0.5rem 1rem' }}>&larr; Back</Link>
                    <div>
                        <h1>{restaurant.name}</h1>
                        <p style={{ color: 'var(--text-secondary)' }}>{restaurant.foodType}</p>
                    </div>
                </div>
                <Link to={`/restaurant/${id}/orders`} className="btn">
                    View Orders
                </Link>
            </header>

            <div className="card" style={{ marginBottom: '2rem' }}>
                <h3 style={{ marginBottom: '1rem' }}>About</h3>
                <p style={{ color: 'var(--text-secondary)', lineHeight: '1.8' }}>{restaurant.description}</p>
            </div>

            <div className="menu-section">
                <div className="flex-between" style={{ marginBottom: '1.5rem' }}>
                    <h2>Menus</h2>
                    <button className="btn" onClick={() => setShowMenuModal(true)}>+ Add Menu</button>
                </div>

                {menus && menus.length > 0 ? (
                    <div className="grid-responsive">
                        {menus.map(menu => (
                            <div key={menu.id} className="card" style={{ display: 'flex', flexDirection: 'column', justifyContent: 'space-between' }}>
                                <div>
                                    <h3 style={{ marginBottom: '0.5rem' }}>{menu.name}</h3>
                                    <p style={{ color: 'var(--text-secondary)', marginBottom: '1.5rem' }}>{menu.description}</p>
                                </div>
                                <Link to={`/menu/${menu.id}`} className="btn secondary" style={{ textAlign: 'center' }}>
                                    View Items
                                </Link>
                            </div>
                        ))}
                    </div>
                ) : (
                    <div className="card flex-center" style={{ padding: '3rem', color: 'var(--text-secondary)' }}>
                        <p>No menus found. Create your first menu to get started.</p>
                    </div>
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
