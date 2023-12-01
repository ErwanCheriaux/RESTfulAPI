import React, { useState } from 'react';
import BikeForm from './BikeForm';

const BikeList = () => {
    const [bikes, setBikes] = useState([]);

    const handleFormSubmit = (newBike) => {
        // Assuming you want to add the new bike to the list
        setBikes([...bikes, newBike]);
    };

    return (
        <div>
            <h1>My Bike List</h1>

            {/* Display existing bikes */}
            {bikes.map((bike, index) => (
                <div key={index}>
                    <p>ID: {bike.id}</p>
                    <p>Brand: {bike.brand}</p>
                    <p>Model: {bike.model}</p>
                    <p>Year: {bike.year}</p>
                    <p>Material: {bike.material}</p>
                    <p>Color: {bike.color}</p>
                    <p>Size: {bike.size}</p>
                    <p>Serial Number: {bike.serialNumber}</p>
                    <p>Creation Date: {bike.creationDate}</p>
                </div>
            ))}

            {/* BikeForm for adding a new bike */}
            <BikeForm onSubmit={handleFormSubmit} />
        </div>
    );
};

export default BikeList;
