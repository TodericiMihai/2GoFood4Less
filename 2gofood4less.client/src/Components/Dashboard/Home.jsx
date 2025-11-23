import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import AddRestaurantModal from './AddRestaurantModal';

function Home() {

    document.title = "Welcome";
    const [restaurants, setRestaurants] = useState([]);
    const [showModal, setShowModal] = useState(false);

    const fetchRestaurants = () => {
        const userId = localStorage.getItem("userId");
        fetch("api/restaurant/manager/" + userId, {
            method: "GET",
            credentials: "include"
        }).then(response => response.json()).then(data => {
            setRestaurants(data);
            console.log("restaurants: ", data);
        }).catch(error => {
            console.log("Error home page: ", error);
        });
    };

    useEffect(() => {
        console.log("Home component loaded. Links should be active.");
        fetchRestaurants();
    }, []);
    return (
        <section className='page'>
            <header>
                <h1>Welcome to your page</h1>
                <button className="btn" onClick={() => setShowModal(true)}>Add Restaurant</button>
            </header>
            {
                restaurants && restaurants.length > 0 ?
                    <div>
                        <table>
                            <thead>
                                <tr>
                                    <th>Name</th>
                                    <th>Description</th>
                                </tr>
                            </thead>
                            <tbody>
                                {restaurants.map(restaurant => (
                                    <tr key={restaurant.id}>
                                        <td>
                                            <Link to={`/restaurant/${restaurant.id}`} style={{ color: 'blue', textDecoration: 'underline', fontWeight: 'bold', cursor: 'pointer' }}>
                                                {restaurant.name}
                                            </Link>
                                        </td>
                                        <td>{restaurant.description}</td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div> :
                    <div className='warning'>
                        <div>No restaurants found. Add one!</div>
                    </div>
            }
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