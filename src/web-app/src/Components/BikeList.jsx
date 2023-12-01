import React, { useState, useEffect } from 'react';
import BikeForm from './BikeForm';

export default function BikeList() {
    const [bikes, setBikes] = useState([]);

    useEffect(() => {
        // Fetch data from your .NET API when the component mounts
        const fetchData = async () => {
            try {
                const response = await fetch('http://localhost:5075/bikes');
                if (response.ok) {
                    const data = await response.json();
                    setBikes(data);
                } else {
                    console.error('Failed to fetch data from API');
                }
            } catch (error) {
                console.error('Error:', error);
            }
        };

        fetchData();
    }, []); // The empty dependency array ensures this effect runs only once when the component mounts


    const handleFormSubmit = async (newBike) => {
        try {
            const response = await fetch('http://localhost:5075/bikes', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newBike),
            });

            if (response.ok) {
                const createdBike = await response.json();
                setBikes([...bikes, createdBike]);
            } else {
                console.error('Failed to create bike');
            }
        } catch (error) {
            console.error('Error:', error);
        }
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