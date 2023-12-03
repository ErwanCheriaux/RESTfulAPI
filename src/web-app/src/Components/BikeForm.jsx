import React, { useState } from 'react';
import { Button, Form, Row, Col } from 'react-bootstrap';

// export default function BikeForm({ onSubmit, ref, defaultValue = null, displaySubmitButton = true }) {
const BikeForm = ({ formRef, onSubmit, defaultValue = null, displaySubmitButton = true }) => {
    const currentYear = new Date().getFullYear();
    const getDefaultFormData = () => {
        return {
            brand: '',
            model: '',
            year: currentYear,
            material: '',
            color: '',
            size: 'M',
            serialNumber: '',
        };
    };

    const [validated, setValidated] = useState(false);
    const [formData, setFormData] = useState(defaultValue || getDefaultFormData());

    const handleChange = (event) => {
        const { name, value } = event.target;
        setFormData({
            ...formData,
            [name]: value
        });
    };

    const handleSubmit = (event) => {
        event.preventDefault();

        // Show validation
        setValidated(true);
        const form = event.currentTarget;
        if (form.checkValidity() === false) {
            return;
        }

        // Process formData
        onSubmit(formData);

        // Reset the form after submission
        setFormData(getDefaultFormData());

        // Hide validation
        setValidated(false);
    };

    return (
        <Form noValidate validated={validated} ref={formRef} onSubmit={handleSubmit}>
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
                    <Form.Control.Feedback type="invalid">
                        Please provide a brand.
                    </Form.Control.Feedback>
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
                    <Form.Control.Feedback type="invalid">
                        Please provide a model.
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group as={Col}>
                    <Form.Label>Year</Form.Label>
                    <Form.Control
                        required
                        min="1900"
                        max={currentYear}
                        isInvalid={validated && (formData.year < 1900 || formData.year > currentYear)}
                        type="number"
                        name="year"
                        value={formData.year}
                        onChange={handleChange} />
                    <Form.Control.Feedback type="invalid">
                        Please provide a year between 1900 and {currentYear}.
                    </Form.Control.Feedback>
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
            <SubmitButton display={displaySubmitButton} />
        </Form >
    );
};

const SubmitButton = ({ display }) => {
    return (display ? <Button type="submit">Submit</Button> : null);
};

export default BikeForm;