import { useState } from 'react';

function AddMenuModal({ onClose, onSuccess, restaurantId }) {
    const [formData, setFormData] = useState({
        Name: '',
        Description: ''
    });
    const [error, setError] = useState('');

    const handleChange = (e) => {
        setFormData({
            ...formData,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');

        if (!restaurantId) {
            setError("Restaurant ID is missing.");
            return;
        }

        const dataToSend = {
            ...formData,
            RestaurantId: restaurantId
        };

        try {
            const response = await fetch('/api/menu/add', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(dataToSend)
            });

            if (response.ok) {
                onSuccess();
                onClose();
            } else {
                const data = await response.json();
                setError(data.message || 'Failed to add menu');
            }
        } catch (err) {
            setError('An error occurred. Please try again.');
            console.error(err);
        }
    };

    return (
        <div className="modal-overlay">
            <div className="modal-content">
                <h2>Add Menu</h2>
                {error && <div className="error-message">{error}</div>}
                <form onSubmit={handleSubmit}>
                    <label>
                        Name:
                        <input type="text" name="Name" value={formData.Name} onChange={handleChange} required />
                    </label>
                    <label>
                        Description:
                        <textarea name="Description" value={formData.Description} onChange={handleChange} required />
                    </label>
                    <div className="modal-actions">
                        <button type="submit" className="btn">Add</button>
                        <button type="button" className="btn cancel" onClick={onClose}>Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default AddMenuModal;
