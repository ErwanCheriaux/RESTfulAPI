import { useRef } from 'react'
import { Modal, Button } from 'react-bootstrap'
import BikeForm from './BikeForm'

export default function BikeEditModal({ bike, show, onClickClose, onClickSave }) {
    const formRef = useRef()

    return (
        <Modal show={show} onHide={onClickClose}>
            <Modal.Header closeButton>
                <Modal.Title>Edit Bike</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <BikeForm
                    formRef={formRef}
                    onSubmit={onClickSave}
                    defaultValue={bike}
                    hideSubmitButton={true} />
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={onClickClose}>Close</Button>
                <Button variant="primary" onClick={() => formRef.current.requestSubmit()}>Save</Button>
            </Modal.Footer>
        </Modal>
    )
}