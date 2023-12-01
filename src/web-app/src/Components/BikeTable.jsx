import React, { useState, useEffect } from 'react';
import BikeForm from './BikeForm';

export default function BikeTable() {
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

    const handelBikeDelete = async (id) => {
        try {
            const response = await fetch('http://localhost:5075/bikes/' + id, {
                method: 'DELETE',
            });

            if (response.ok) {
                let bikesCopy = [...bikes];
                let bikesRemoved = bikesCopy.filter(bike => bike.id != id);
                setBikes(bikesRemoved);
            } else {
                console.error('Failed to delete bike')
            }
        } catch (error) {
            console.error('Error:', error);
        }
    };

    return (
        <div>
            <h1>My Bike List</h1>

            {/* Display existing bikes in a table */}
            <table>
                <thead>
                    <tr>
                        <th>Brand</th>
                        <th>Model</th>
                        <th>Year</th>
                        <th>Material</th>
                        <th>Color</th>
                        <th>Size</th>
                        <th>Serial number</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {bikes.map((bike, index) => (
                        <tr key={index}>
                            <td>{bike.brand}</td>
                            <td>{bike.model}</td>
                            <td>{bike.year}</td>
                            <td>{bike.material}</td>
                            <td>{bike.color}</td>
                            <td>{bike.size}</td>
                            <td>{bike.serialNumber}</td>
                            <td><button onClick={() => handelBikeDelete(bike.id)}>X</button></td>
                        </tr>
                    ))}
                </tbody>
            </table>

            {/* BikeForm for adding a new bike */}
            <BikeForm onSubmit={handleFormSubmit} />
        </div>
    );
};