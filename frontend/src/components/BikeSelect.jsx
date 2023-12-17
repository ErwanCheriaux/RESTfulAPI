import { useState, useEffect } from 'react'
import { Form } from 'react-bootstrap'
import { getRiderBikesAsync, patchRiderBikeAsync } from '../api'

export default function BikeSelect({ riderId, bikes, loadBikes }) {
    const [riderBikes, setRiderBikes] = useState([])

    useEffect(() => {
        loadRiderBikes()
    }, [])

    async function loadRiderBikes() {
        const data = await getRiderBikesAsync(riderId)
        setRiderBikes(data)
    }

    async function handleChange(event) {
        const { value } = event.target
        await patchRiderBikeAsync(riderId, value)
        await loadRiderBikes()
        await loadBikes()
    }

    function getSelectedBikeId() {
        return riderBikes.length > 0 ? riderBikes[0].id : ''
    }

    function isBikeAvailable(bike) {
        return bike.riderId === null || bike.riderId === riderId
    }

    return (
        <Form.Select value={getSelectedBikeId()} onChange={handleChange}>
            <option value={''} >Select a bike</option>
            {
                bikes.map((bike, index) => (
                    isBikeAvailable(bike) && <option key={index} value={bike.id}>{`${bike.year} ${bike.brand} ${bike.model}`}</option>
                ))
            }
        </Form.Select >
    )
}