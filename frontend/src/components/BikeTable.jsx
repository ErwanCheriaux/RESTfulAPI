import { useState, useEffect } from 'react'
import { Table, Button } from 'react-bootstrap'
import BikeForm from './BikeForm'
import BikeEditModal from './BikeEditModal'

export default function BikeTable({ bikes, setBikes, getBikes }) {
    const [bikeEdit, setBikeEdit] = useState({})
    const [showModal, setShowModal] = useState(false)

    useEffect(() => {
        // Fetch data from your .NET API when the component mounts
        getBikes()
    }, [getBikes])

    const handleFormSubmit = async (newBike) => {
        try {
            const response = await fetch('http://localhost:5075/bikes', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newBike),
            })

            if (response.ok) {
                const createdBike = await response.json()
                setBikes([...bikes, createdBike])
            } else {
                console.error('Failed to create bike')
            }
        } catch (error) {
            console.error('Error:', error)
        }
    }

    const handelBikeEdit = (existingBike) => {
        setBikeEdit(existingBike)
        setShowModal(true)
    }

    const handleCloseModal = () => {
        setShowModal(false)
    }

    const handleSaveModal = async (updatedBike) => {
        setShowModal(false)

        try {
            const response = await fetch('http://localhost:5075/bikes/' + updatedBike.id, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedBike),
            })

            if (response.ok) {
                let bikesCopy = [...bikes]
                bikesCopy[bikesCopy.findIndex(bike => bike.id === updatedBike.id)] = updatedBike
                setBikes(bikesCopy)
            } else {
                console.error('Failed to update bike')
            }
        } catch (error) {
            console.error('Error:', error)
        }
    }

    const handelBikeDelete = async (id) => {
        try {
            const response = await fetch('http://localhost:5075/bikes/' + id, {
                method: 'DELETE',
            })

            if (response.ok) {
                let bikesCopy = [...bikes]
                let bikesRemoved = bikesCopy.filter(bike => bike.id !== id)
                setBikes(bikesRemoved)
            } else {
                console.error('Failed to delete bike')
            }
        } catch (error) {
            console.error('Error:', error)
        }
    }

    return (
        <>
            <h1>My bike list</h1>
            <Table hover size='sm'>
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
                            <td><Button variant="warning" onClick={() => handelBikeEdit(bike)}>Edit</Button></td>
                            <td><Button variant="danger" onClick={() => handelBikeDelete(bike.id)}>Delete</Button></td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <BikeEditModal
                bike={bikeEdit}
                show={showModal}
                onClickClose={handleCloseModal}
                onClickSave={handleSaveModal} />

            <h1>Add a new bike</h1>
            <BikeForm onSubmit={handleFormSubmit} />
        </>
    )
}