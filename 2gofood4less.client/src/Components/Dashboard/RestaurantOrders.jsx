import { useEffect, useState } from 'react';
import { useParams, Link } from 'react-router-dom';
import api from '../../utils/axiosConfig';

function RestaurantOrders() {
    const { id } = useParams();
    const [orders, setOrders] = useState([]);
    const [loading, setLoading] = useState(true);
    const [activeTab, setActiveTab] = useState('current'); // 'current' or 'past'
    const [restaurant, setRestaurant] = useState(null);
    const [processingOrders, setProcessingOrders] = useState(new Set());

    const fetchOrders = async () => {
        setLoading(true);
        try {
            // Fetch restaurant details
            const restResponse = await api.get(`/restaurant/${id}`);
            setRestaurant(restResponse.data);

            // Fetch orders
            const ordersResponse = await api.get(`/order/restaurant/${id}`);
            setOrders(ordersResponse.data || []);
            console.log("Orders: ", ordersResponse.data);
        } catch (error) {
            console.log("Error fetching orders: ", error);
        } finally {
            setLoading(false);
        }
    };

    const handleProgressOrder = async (orderId, currentStatus) => {
        // Add to processing set
        setProcessingOrders(prev => new Set([...prev, orderId]));

        try {
            let endpoint = '';

            // Determine which endpoint to call based on current status
            if (currentStatus === 1) { // Confirmed -> Processing
                endpoint = `/order/process/${orderId}`;
            } else if (currentStatus === 2) { // Processing -> Finished
                endpoint = `/order/finish/${orderId}`;
            }

            if (endpoint) {
                await api.post(endpoint);
                // Refresh orders after successful update
                await fetchOrders();
            }
        } catch (error) {
            console.error("Error updating order status: ", error);
            alert("Failed to update order status. Please try again.");
        } finally {
            // Remove from processing set
            setProcessingOrders(prev => {
                const newSet = new Set(prev);
                newSet.delete(orderId);
                return newSet;
            });
        }
    };

    useEffect(() => {
        fetchOrders();
    }, [id]);

    // Filter orders based on status
    const currentOrders = orders.filter(order => order.status !== 3);
    const pastOrders = orders.filter(order => order.status === 3);

    const displayedOrders = activeTab === 'current' ? currentOrders : pastOrders;

    const getStatusLabel = (status) => {
        switch (status) {
            case 0: return 'Pending';
            case 1: return 'Confirmed';
            case 2: return 'In Progress';
            case 3: return 'Completed';
            case 4: return 'Cancelled';
            default: return 'Unknown';
        }
    };

    const getStatusColor = (status) => {
        switch (status) {
            case 0: return '#ffa500'; // Orange
            case 1: return '#4CAF50'; // Green
            case 2: return '#2196F3'; // Blue
            case 3: return '#9E9E9E'; // Gray
            case 4: return '#f44336'; // Red
            default: return '#757575';
        }
    };

    const getProgressButtonText = (status) => {
        if (status === 1) return 'Start Processing';
        if (status === 2) return 'Mark as Finished';
        return null;
    };

    if (loading) return <div className="page flex-center">Loading orders...</div>;

    return (
        <section className='page'>
            <header className="flex-between" style={{ marginBottom: '2rem' }}>
                <div style={{ display: 'flex', alignItems: 'center', gap: '1rem' }}>
                    <Link to={`/restaurant/${id}`} className="btn secondary" style={{ padding: '0.5rem 1rem' }}>&larr; Back</Link>
                    <div>
                        <h1>Orders</h1>
                        {restaurant && <p style={{ color: 'var(--text-secondary)' }}>{restaurant.name}</p>}
                    </div>
                </div>
            </header>

            {/* Tab Buttons */}
            <div style={{ display: 'flex', gap: '1rem', marginBottom: '2rem' }}>
                <button
                    className={`btn ${activeTab === 'current' ? '' : 'secondary'}`}
                    onClick={() => setActiveTab('current')}
                    style={{ flex: 1 }}
                >
                    Current Orders ({currentOrders.length})
                </button>
                <button
                    className={`btn ${activeTab === 'past' ? '' : 'secondary'}`}
                    onClick={() => setActiveTab('past')}
                    style={{ flex: 1 }}
                >
                    Past Orders ({pastOrders.length})
                </button>
            </div>

            {/* Orders Display */}
            {displayedOrders.length > 0 ? (
                <div style={{ display: 'flex', flexDirection: 'column', gap: '1.5rem' }}>
                    {displayedOrders.map(order => (
                        <div key={order.id} className="card">
                            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'start', marginBottom: '1rem' }}>
                                <div>
                                    <h3 style={{ marginBottom: '0.25rem' }}>Order #{order.id.substring(0, 8)}</h3>
                                    <p style={{ color: 'var(--text-secondary)', fontSize: '0.9rem' }}>
                                        Client ID: {order.clientId}
                                    </p>
                                </div>
                                <span
                                    style={{
                                        padding: '0.5rem 1rem',
                                        borderRadius: '20px',
                                        backgroundColor: getStatusColor(order.status) + '20',
                                        color: getStatusColor(order.status),
                                        fontWeight: '600',
                                        fontSize: '0.9rem'
                                    }}
                                >
                                    {getStatusLabel(order.status)}
                                </span>
                            </div>

                            <div style={{ marginBottom: '1rem', fontSize: '0.9rem', color: 'var(--text-secondary)' }}>
                                <p><strong>Location:</strong> {order.location}</p>
                                <p><strong>Payment:</strong> {order.paymentMethod === 0 ? 'Cash' : 'Card'}</p>
                                <p><strong>Total:</strong> ${order.value?.toFixed(2)}</p>
                            </div>

                            <div style={{ borderTop: '1px solid var(--border)', paddingTop: '1rem', marginBottom: '1rem' }}>
                                <h4 style={{ marginBottom: '0.75rem', fontSize: '1rem' }}>Items:</h4>
                                <div style={{ display: 'flex', flexDirection: 'column', gap: '0.5rem' }}>
                                    {order.orderItems && order.orderItems.map((item, index) => (
                                        <div
                                            key={index}
                                            style={{
                                                display: 'flex',
                                                justifyContent: 'space-between',
                                                padding: '0.5rem',
                                                backgroundColor: 'var(--bg-secondary)',
                                                borderRadius: '8px'
                                            }}
                                        >
                                            <span>{item.name}</span>
                                            <span style={{ color: 'var(--text-secondary)' }}>x{item.quantity}</span>
                                        </div>
                                    ))}
                                </div>
                            </div>

                            {/* Progress Button - Only show for Confirmed (1) and Processing (2) statuses */}
                            {(order.status === 1 || order.status === 2) && (
                                <button
                                    className="btn"
                                    onClick={() => handleProgressOrder(order.id, order.status)}
                                    disabled={processingOrders.has(order.id)}
                                    style={{
                                        width: '100%',
                                        opacity: processingOrders.has(order.id) ? 0.6 : 1,
                                        cursor: processingOrders.has(order.id) ? 'not-allowed' : 'pointer'
                                    }}
                                >
                                    {processingOrders.has(order.id) ? 'Updating...' : getProgressButtonText(order.status)}
                                </button>
                            )}
                        </div>
                    ))}
                </div>
            ) : (
                <div className="card flex-center" style={{ padding: '3rem', color: 'var(--text-secondary)', textAlign: 'center' }}>
                    <p>No {activeTab} orders found.</p>
                </div>
            )}
        </section>
    );
}

export default RestaurantOrders;
