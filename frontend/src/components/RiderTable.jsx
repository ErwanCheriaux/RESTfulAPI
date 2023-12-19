import { useState } from 'react'
import { Table, Button } from 'react-bootstrap'
import RiderForm from './RiderForm'
import RiderEditModal from './RiderEditModal'
import BikeSelect from './BikeSelect'
import {
    getRidersAsync,
    postRiderAsync,
    putRiderAsync,
    deleteRiderAsync,
    getBikesAsync,
} from '../api'

export default function RiderTable({ ridersData, bikesData }) {
    const [riders, setRiders] = useState(ridersData)
    const [bikes, setBikes] = useState(bikesData)
    const [riderEdit, setRiderEdit] = useState({})
    const [showModal, setShowModal] = useState(false)

    async function reloadRidersData() {
        setRiders(await getRidersAsync())
    }

    async function reloadBikesData() {
        setBikes(await getBikesAsync())
    }

    async function handleFormSubmit(newRider) {
        await postRiderAsync(newRider)
        await reloadRidersData()
    }

    function handleEdit(existingRider) {
        setRiderEdit(existingRider)
        setShowModal(true)
    }

    function handleCloseModal() {
        setShowModal(false)
    }

    async function handleSaveModal(updatedRider) {
        setShowModal(false)
        if (await putRiderAsync(updatedRider)) {
            await reloadRidersData()
        }
    }

    async function handleDelete(id) {
        if (await deleteRiderAsync(id)) {
            await reloadRidersData()
        }
    }

    // d: yyyy-mm-dd
    function dateToAge(d) {
        let date = new Date(d + 'T00:00:00')
        let today = new Date()
        let age = today.getFullYear() - date.getFullYear()
        if (date.setFullYear(date.getFullYear() + age) > today) {
            age--
        }
        return age
    }

    return (
        <>
            <h1>My rider list</h1>
            <Table hover size='sm'>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Age</th>
                        <th>Country</th>
                        <th>Bike</th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {riders.map((rider, index) => (
                        <tr key={index}>
                            <td>{rider.name}</td>
                            <td>{dateToAge(rider.birthdate)}</td>
                            <td>{rider.country}</td>
                            <td><BikeSelect riderId={rider.id} bikes={bikes} reloadData={reloadBikesData} /></td>
                            <td><Button variant="warning" onClick={() => handleEdit(rider)}>Edit</Button></td>
                            <td><Button variant="danger" onClick={() => handleDelete(rider.id)}>Delete</Button></td>
                        </tr>
                    ))}
                </tbody>
            </Table>

            <RiderEditModal
                rider={riderEdit}
                show={showModal}
                onClickClose={handleCloseModal}
                onClickSave={handleSaveModal} />

            <h1>Add a new rider</h1>
            <RiderForm onSubmit={handleFormSubmit} />
        </>
    )
}