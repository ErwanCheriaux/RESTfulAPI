import React, { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

export default function BikeForm({ onSubmit }) {
    const [formData, setFormData] = useState({
        brand: '',
        model: '',
        year: 2010,
        material: '',
        color: '',
        size: 'M',
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
            size: 'M',
            serialNumber: ''
        });
    };

    return (
        <>
            <h1>Add new bike</h1>
            <Form onSubmit={handleSubmit}>
                <Row className='mb-3'>
                    <Form.Group as={Col}>
                        <Form.Label>Brand</Form.Label>
                        <Form.Control
                            autoFocus
                            required
                            type="text"
                            name="brand"
                            value={formData.brand}
                            placeholder='Transition...'
                            onChange={handleChange} />
                    </Form.Group>
                    <Form.Group as={Col}>
                        <Form.Label>Model</Form.Label>
                        <Form.Control
                            required
                            type="text"
                            name="model"
                            value={formData.model}
                            placeholder='Patrol...'
                            onChange={handleChange} />
                    </Form.Group>
                    <Form.Group as={Col}>
                        <Form.Label>Year</Form.Label>
                        <Form.Control
                            type="number"
                            name="year"
                            value={formData.year}
                            onChange={handleChange} />
                    </Form.Group>
                </Row>
                <Row className='mb-3'>
                    <Form.Group as={Col}>
                        <Form.Label>Material</Form.Label>
                        <Form.Control
                            type="text"
                            name="material"
                            value={formData.material}
                            placeholder='Carbon...'
                            onChange={handleChange} />
                    </Form.Group>
                    <Form.Group as={Col}>
                        <Form.Label>Color</Form.Label>
                        <Form.Control
                            type="text"
                            name="color"
                            value={formData.color}
                            placeholder='Blue...'
                            onChange={handleChange} />
                    </Form.Group>
                    <Form.Group as={Col}>
                        <Form.Label>Size</Form.Label>
                        <Form.Select name='size' value={formData.size} onChange={handleChange} >
                            {['XS', 'S', 'M', 'L', 'XL'].map((size, index) => (
                                <option key={index}>{size}</option>
                            ))}
                        </Form.Select>
                    </Form.Group>
                </Row>
                <Form.Group className="mb-3">
                    <Form.Label>Serial number</Form.Label>
                    <Form.Control
                        type="text"
                        name="serialNumber"
                        value={formData.serialNumber}
                        placeholder='1251-AD-664...'
                        onChange={handleChange} />
                </Form.Group>
                <Button type="submit">Submit</Button>
            </Form >
        </>
    );
};