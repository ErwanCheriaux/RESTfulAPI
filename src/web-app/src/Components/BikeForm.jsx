import React, { useState } from 'react';

export default function BikeForm({ onSubmit }) {
    const [formData, setFormData] = useState({
        brand: '',
        model: '',
        year: 2010,
        material: '',
        color: '',
        size: '',
        serialNumber: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        onSubmit(formData);
        // Reset the form after submission
        setFormData({
            brand: '',
            model: '',
            year: 2010,
            material: '',
            color: '',
            size: '',
            serialNumber: ''
        });
    };

    return (
        <form onSubmit={handleSubmit}>
            <label>
                Brand:
                <input autoFocus required type="text" name="brand" value={formData.brand} onChange={handleChange} />
            </label>
            <label>
                Model:
                <input required type="text" name="model" value={formData.model} onChange={handleChange} />
            </label>
            <label>
                Year:
                <input type="number" name="year" value={formData.year} onChange={handleChange} />
            </label>
            <label>
                Material:
                <input type="text" name="material" value={formData.material} onChange={handleChange} />
            </label>
            <label>
                Color:
                <input type="text" name="color" value={formData.color} onChange={handleChange} />
            </label>
            <label>
                Size:
                <input type="text" name="size" value={formData.size} onChange={handleChange} />
            </label>
            <label>
                Serial Number:
                <input type="text" name="serialNumber" value={formData.serialNumber} onChange={handleChange} />
            </label>

            <button type="submit">Submit</button>
        </form>
    );
};