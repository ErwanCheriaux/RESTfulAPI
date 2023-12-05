import { Form } from 'react-bootstrap'

export default function BikeSelect({ bikes }) {

    return (
        <Form.Select>
            <option>Select a bike</option>
            {bikes.map((bike, index) => (
                <option key={index}>{bike.year + ' ' + bike.brand + ' ' + bike.model}</option>
            ))}
        </Form.Select>
    )
}