import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import AddMenuItemModal from './AddMenuItemModal';
import api from '../../utils/axiosConfig';

function MenuDetails() {
    const { id } = useParams();
    const [menuItems, setMenuItems] = useState([]);
    const [loading, setLoading] = useState(true);
    const [showItemModal, setShowItemModal] = useState(false);

    const fetchMenuItems = async () => {
        setLoading(true);
        try {
            const response = await api.get(`/menuitem/menu/${id}`);
            setMenuItems(response.data || []);
        } catch (error) {
            console.log("Error menu details page: ", error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchMenuItems();
    }, [id]);

    return (
        <section className='page'>
            <header className="flex-between" style={{ marginBottom: '2rem' }}>
                <div style={{ display: 'flex', alignItems: 'center', gap: '1rem' }}>
                    <Link to="/" className="btn secondary" style={{ padding: '0.5rem 1rem' }}>&larr; Home</Link>
                    <h1>Menu Details</h1>
                </div>
            </header>

            <div className="menu-items-section">
                <div className="flex-between" style={{ marginBottom: '1.5rem' }}>
                    <h2>Menu Items</h2>
                    <button className="btn" onClick={() => setShowItemModal(true)}>+ Add Item</button>
                </div>

                {loading ? (
                    <div className="flex-center" style={{ height: '100px' }}>Loading items...</div>
                ) : menuItems && menuItems.length > 0 ? (
                    <div className="card" style={{ padding: 0, overflow: 'hidden' }}>
                        <table>
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Description</th>
                                    <th style={{ textAlign: 'right' }}>Price</th>
                                </tr>
                            </thead>
                            <tbody>
                                {menuItems.map(item => (
                                    <tr key={item.id}>
                                        <td style={{ fontWeight: '600' }}>{item.name}</td>
                                        <td style={{ color: 'var(--text-secondary)' }}>{item.description}</td>
                                        <td style={{ textAlign: 'right', fontWeight: 'bold', color: 'var(--primary-color)' }}>
                                            ${item.price}
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                ) : (
                    <div className="card flex-center" style={{ padding: '3rem', color: 'var(--text-secondary)' }}>
                        <p>No items found in this menu.</p>
                    </div>
                )}
            </div>

            {showItemModal && (
                <AddMenuItemModal
                    menuId={id}
                    onClose={() => setShowItemModal(false)}
                    onSuccess={() => {
                        fetchMenuItems();
                        alert("Item added successfully!");
                    }}
                />
            )}
        </section>
    );
}

export default MenuDetails;
