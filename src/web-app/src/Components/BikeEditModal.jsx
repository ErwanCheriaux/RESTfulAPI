import React, { useRef } from 'react';
import { Modal, Button } from 'react-bootstrap';
import BikeForm from './BikeForm';

export default function BikeEditModal({ bike, show, onClickClose, onClickSave }) {
    const formRef = useRef();

    const handleSave = () => {
        formRef.current.submit();
    };

    return (
        <Modal show={show} onHide={onClickClose}>
            <Modal.Header closeButton>
                <Modal.Title>Edit Bike</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <BikeForm
                    ref={formRef}
                    onSubmit={onClickSave}
                    defaultValue={bike}
                    displaySubmitButton={false} />
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={onClickClose}>Close</Button>
                <Button variant="primary" onClick={handleSave}>Save</Button>
            </Modal.Footer>
        </Modal>
    );
};