import { useState, useEffect } from 'react'
import { Form } from 'react-bootstrap'
import configData from "../config.json"

export default function BikeSelect({ riderId, bikes, getBikes }) {
    const [riderBikes, setRiderBikes] = useState([])

    useEffect(() => {
        // Fetch data from your .NET API when the component mounts
        getRiderBikes(riderId)
    }, [riderId])

    const handleChange = async (event) => {
        const { value } = event.target

        try {
            const response = await fetch(configData.SERVER_URL + '/riders/' + riderId + '/bikes?bike_id=' + value, {
                method: 'PATCH',
                headers: {
                    'Content-Type': 'application/json',
                }
            })

            if (response.ok) {
                console.log('Not implemented yet')
                // reload bikes
                getBikes()
                // reload rider bikes
                getRiderBikes(riderId)
            } else {
                console.error('Failed to update rider')
            }
        } catch (error) {
            console.error('Error:', error)
        }
    }

    async function getRiderBikes(id) {
        try {
            const response = await fetch(configData.SERVER_URL + '/riders/' + id + '/bikes')
            if (response.ok) {
                const data = await response.json()
                setRiderBikes(data)
            } else {
                console.error('Failed to fetch data from API')
            }
        } catch (error) {
            console.error('Error:', error)
        }
    }

    function getSelectedBikeId() {
        return riderBikes.length > 0 ? riderBikes[0].id : '';
    }

    function isBikeAvailable(bike) {
        return bike.riderId === null || bike.riderId === riderId
    }

    return (
        <Form.Select value={getSelectedBikeId()} onChange={handleChange}>
            <option value={''} >Select a bike</option>
            {
                bikes.map((bike, index) => (
                    isBikeAvailable(bike) && <option key={index} value={bike.id}>{bike.year + ' ' + bike.brand + ' ' + bike.model}</option>
                ))
            }
        </Form.Select >
    )
}