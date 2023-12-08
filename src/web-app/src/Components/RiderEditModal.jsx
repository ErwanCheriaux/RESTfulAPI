import { useRef } from 'react'
import { Modal, Button } from 'react-bootstrap'
import RiderForm from './RiderForm'

export default function RiderEditModal({ rider, show, onClickClose, onClickSave }) {
    const formRef = useRef()

    return (
        <Modal show={show} onHide={onClickClose}>
            <Modal.Header closeButton>
                <Modal.Title>Edit Rider</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <RiderForm
                    formRef={formRef}
                    onSubmit={onClickSave}
                    defaultValue={rider}
                    hideSubmitButton={true} />
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={onClickClose}>Close</Button>
                <Button variant="primary" onClick={() => formRef.current.requestSubmit()}>Save</Button>
            </Modal.Footer>
        </Modal>
    )
}