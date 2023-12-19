import { useState } from 'react'
import { Table, Button } from 'react-bootstrap'
import BikeForm from './BikeForm'
import BikeEditModal from './BikeEditModal'
import {
    getBikesAsync,
    postBikeAsync,
    putBikeAsync,
    deleteBikeAsync,
} from '../utils/api'

export default function BikeTable({ bikesData }) {
    const [bikes, setBikes] = useState(bikesData)
    const [bikeEdit, setBikeEdit] = useState({})
    const [showModal, setShowModal] = useState(false)

    async function reloadData() {
        setBikes(await getBikesAsync())
    }

    async function handleFormSubmit(newBike) {
        await postBikeAsync(newBike)
        await reloadData()
    }

    function handleEdit(existingBike) {
        setBikeEdit(existingBike)
        setShowModal(true)
    }

    function handleCloseModal() {
        setShowModal(false)
    }

    async function handleSaveModal(updatedBike) {
        setShowModal(false)
        if (await putBikeAsync(updatedBike)) {
            await reloadData()
        }
    }

    async function handelDelete(id) {
        if (await deleteBikeAsync(id)) {
            await reloadData()
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
                            <td><Button variant="warning" onClick={() => handleEdit(bike)}>Edit</Button></td>
                            <td><Button variant="danger" onClick={() => handelDelete(bike.id)}>Delete</Button></td>
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