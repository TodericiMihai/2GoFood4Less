import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import AddMenuItemModal from './AddMenuItemModal';

function MenuDetails() {
    const { id } = useParams();
    const [menuItems, setMenuItems] = useState([]);
    const [loading, setLoading] = useState(true);
    const [showItemModal, setShowItemModal] = useState(false);

    const fetchMenuItems = () => {
        setLoading(true);
        fetch(`/api/menuitem/menu/${id}`, {
            method: "GET",
            credentials: "include"
        }).then(response => {
            if (response.ok) {
                return response.json();
            }
            return [];
        }).then(data => {
            setMenuItems(data);
            setLoading(false);
        }).catch(error => {
            console.log("Error menu details page: ", error);
            setLoading(false);
        });
    };

    useEffect(() => {
        fetchMenuItems();
    }, [id]);

    return (
        <section className='page'>
            <header>
                <h1>Menu Details</h1>
                <Link to="/" className="btn">Back to Home</Link>
            </header>

            <div className="menu-items-section">
                <div className="section-header">
                    <h2>Menu Items</h2>
                    <button className="btn" onClick={() => setShowItemModal(true)}>Add Menu Item</button>
                </div>
                {loading ? (
                    <div>Loading items...</div>
                ) : menuItems && menuItems.length > 0 ? (
                    <table>
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Description</th>
                                <th>Price</th>
                            </tr>
                        </thead>
                        <tbody>
                            {menuItems.map(item => (
                                <tr key={item.id}>
                                    <td>{item.name}</td>
                                    <td>{item.description}</td>
                                    <td>${item.price}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                ) : (
                    <p>No items found in this menu.</p>
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
