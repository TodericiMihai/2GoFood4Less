import { useState } from 'react';

function AddRestaurantModal({ onClose, onSuccess }) {
    const [formData, setFormData] = useState({
        Name: '',
        Description: '',
        FoodType: ''
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

        const managerId = localStorage.getItem("userId");
        if (!managerId) {
            setError("User ID not found. Please log in again.");
            return;
        }

        const dataToSend = {
            ...formData,
            ManagerId: managerId
        };

        try {
            const response = await fetch('api/restaurant/add', {
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
                setError(data.message || 'Failed to add restaurant');
            }
        } catch (err) {
            setError('An error occurred. Please try again.');
            console.error(err);
        }
    };

    return (
        <div className="modal-overlay">
            <div className="modal-content">
                <h2>Add Restaurant</h2>
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
                    <label>
                        Food Type:
                        <input type="text" name="FoodType" value={formData.FoodType} onChange={handleChange} required />
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

export default AddRestaurantModal;
