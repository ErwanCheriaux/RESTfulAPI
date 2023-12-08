import { useState, useEffect } from 'react'
import { Table, Button } from 'react-bootstrap'
import RiderForm from './RiderForm'
import RiderEditModal from './RiderEditModal'
import BikeSelect from './BikeSelect'

export default function RiderTable({ bikes, getBikes }) {
    const [riders, setRiders] = useState([])
    const [riderEdit, setRiderEdit] = useState({})
    const [showModal, setShowModal] = useState(false)

    useEffect(() => {
        // Fetch data from your .NET API when the component mounts
        const fetchData = async () => {
            try {
                const response = await fetch('http://localhost:5075/riders')
                if (response.ok) {
                    const data = await response.json()
                    setRiders(data)
                } else {
                    console.error('Failed to fetch data from API')
                }
            } catch (error) {
                console.error('Error:', error)
            }
        }

        fetchData()
    }, []) // The empty dependency array ensures this effect runs only once when the component mounts

    const handleFormSubmit = async (newRider) => {
        try {
            const response = await fetch('http://localhost:5075/riders', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(newRider),
            })

            if (response.ok) {
                const createdRider = await response.json()
                setRiders([...riders, createdRider])
            } else {
                console.error('Failed to create rider')
            }
        } catch (error) {
            console.error('Error:', error)
        }
    }

    const handelRiderEdit = (existingRider) => {
        setRiderEdit(existingRider)
        setShowModal(true)
    }

    const handleCloseModal = () => {
        setShowModal(false)
    }

    const handleSaveModal = async (updatedRider) => {
        setShowModal(false)

        try {
            const response = await fetch('http://localhost:5075/riders/' + updatedRider.id, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedRider),
            })

            if (response.ok) {
                let ridersCopy = [...riders]
                ridersCopy[ridersCopy.findIndex(rider => rider.id === updatedRider.id)] = updatedRider
                setRiders(ridersCopy)
            } else {
                console.error('Failed to update rider')
            }
        } catch (error) {
            console.error('Error:', error)
        }
    }

    const handelRiderDelete = async (id) => {
        try {
            const response = await fetch('http://localhost:5075/riders/' + id, {
                method: 'DELETE',
            })

            if (response.ok) {
                let ridersCopy = [...riders]
                let ridersRemoved = ridersCopy.filter(rider => rider.id !== id)
                setRiders(ridersRemoved)
                getBikes()
            } else {
                console.error('Failed to delete rider')
            }
        } catch (error) {
            console.error('Error:', error)
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
                            <td><BikeSelect riderId={rider.id} bikes={bikes} getBikes={getBikes} /></td>
                            <td><Button variant="warning" onClick={() => handelRiderEdit(rider)}>Edit</Button></td>
                            <td><Button variant="danger" onClick={() => handelRiderDelete(rider.id)}>Delete</Button></td>
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