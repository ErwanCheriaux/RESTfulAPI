import { Form } from 'react-bootstrap'
import { patchRiderBikeAsync } from '../utils/api'

export default function BikeSelect({ riderId, bikes, reloadData }) {
    async function handleChange(event) {
        const { value } = event.target
        if (await patchRiderBikeAsync(riderId, value)) {
            await reloadData()
        }
    }

    function getSelectedBikeId() {
        const riderBike = bikes.find((bike) => bike.riderId === riderId)
        return riderBike ? riderBike.id : ''
    }

    function isBikeAvailable(bike) {
        return bike.riderId === null || bike.riderId === riderId
    }

    return (
        <Form.Select value={getSelectedBikeId()} onChange={handleChange}>
            <option value={''} >Select a bike</option>
            {
                bikes.map((bike, index) => (
                    isBikeAvailable(bike) &&
                    <option key={index} value={bike.id}>
                        {`${bike.year} ${bike.brand} ${bike.model}`}
                    </option>
                ))
            }
        </Form.Select >
    )
}