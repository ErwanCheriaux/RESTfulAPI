import { useState, useEffect } from 'react'
import { Table, Button } from 'react-bootstrap'
import BikeForm from './BikeForm'
import BikeEditModal from './BikeEditModal'
import { deleteBikeAsync, postBikeAsync, putBikeAsync } from '../api'

export default function BikeTable({ bikes, setBikes, loadBikes }) {
    const [bikeEdit, setBikeEdit] = useState({})
    const [showModal, setShowModal] = useState(false)

    useEffect(() => {
        loadBikes()
    }, [loadBikes])

    async function handleFormSubmit(newBike) {
        const createdBike = await postBikeAsync(newBike)
        setBikes([...bikes, createdBike])
    }

    function handelBikeEdit(existingBike) {
        setBikeEdit(existingBike)
        setShowModal(true)
    }

    function handleCloseModal() {
        setShowModal(false)
    }

    async function handleSaveModal(updatedBike) {
        setShowModal(false)
        await putBikeAsync(updatedBike)
        await loadBikes()
    }

    async function handelBikeDelete(id) {
        await deleteBikeAsync(id)
        await loadBikes()
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