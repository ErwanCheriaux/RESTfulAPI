import React, { useState } from 'react'
import { Button, Form, Row, Col } from 'react-bootstrap'

export default function RiderForm({ formRef, onSubmit, defaultValue, hideSubmitButton }) {
    const getDefaultFormData = () => {
        return {
            name: '',
            birthdate: '',
            country: ''
        }
    }

    const [validated, setValidated] = useState(false)
    const [formData, setFormData] = useState(defaultValue || getDefaultFormData())

    const handleChange = (event) => {
        const { name, value } = event.target
        setFormData({
            ...formData,
            [name]: value
        })
    }

    const handleSubmit = (event) => {
        event.preventDefault()

        // Show validation
        setValidated(true)
        const form = event.currentTarget
        if (form.checkValidity() === false) {
            return
        }

        // Process formData
        onSubmit(formData)

        // Reset the form after submission
        setFormData(getDefaultFormData())

        // Hide validation
        setValidated(false)
    }

    return (
        <Form noValidate validated={validated} ref={formRef} onSubmit={handleSubmit}>
            <Row className='mb-3'>
                <Form.Group as={Col}>
                    <Form.Label>Name</Form.Label>
                    <Form.Control
                        autoFocus
                        required
                        type="text"
                        name="name"
                        value={formData.name}
                        placeholder='Erwan...'
                        onChange={handleChange} />
                    <Form.Control.Feedback type="invalid">
                        Please provide a name.
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group as={Col}>
                    <Form.Label>Birth date</Form.Label>
                    <Form.Control
                        required
                        type="text"
                        name="birthdate"
                        value={formData.birthdate}
                        placeholder='yyyy-mm-dd'
                        onChange={handleChange} />
                    <Form.Control.Feedback type="invalid">
                        Please provide a date of birth.
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group as={Col}>
                    <Form.Label>Country</Form.Label>
                    <Form.Control
                        type="text"
                        name="country"
                        value={formData.country}
                        onChange={handleChange} />
                </Form.Group>
            </Row>
            <SubmitButton display={!hideSubmitButton} />
        </Form >
    )
}

const SubmitButton = ({ display }) => {
    return (display ? <Button type="submit">Submit</Button> : null)
}