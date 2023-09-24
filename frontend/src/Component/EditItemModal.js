import React, { useState, useEffect } from 'react';
import { Button, Modal, Form, Row, Col } from 'react-bootstrap';
import axios from 'axios';

//Todo validation errors
const EditItemModal = ({ showModal, handleCloseModal, selectedItem, setShowModal }) => {
    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;
    const [item, setItem] = useState(selectedItem);
    const [errors, setErrors] = useState({});


    useEffect(() => {
        setItem(selectedItem);
    }, [selectedItem]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setItem({ ...item, [name]: value });
    };

    function handleSubmit(event) {
        event.preventDefault();
        axios
            .put(`https://localhost:7189/api/Items/`, item, {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                alert('Successfully edited loan-card details!');
            }).catch((error) => {
                alert('Error editing loan-card details! ', error);
            });
    }

    return (
        <Modal show={showModal} onHide={handleCloseModal}>
            <Modal.Header closeButton>
                <Modal.Title>Edit details of Item</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmit}>
                    <Form.Group controlId='formCategory'>
                        <Form.Label>itemCategory</Form.Label>
                        <Form.Control
                            type='text'
                            name='itemCategory'
                            value={item.itemCategory}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId='formDescription'>
                        <Form.Label>itemDescription</Form.Label>
                        <Form.Control
                            type='text'
                            name='itemDescription'
                            value={item.itemDescription}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId='formMake'>
                        <Form.Label>itemMake</Form.Label>
                        <Form.Control
                            type='text'
                            name='itemMake'
                            value={item.itemMake}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId='formValuation'>
                        <Form.Label>itemValuation</Form.Label>
                        <Form.Control
                            type='text'
                            name='itemValuation'
                            value={item.itemValuation}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Button variant="primary" type="submit">
                        Submit
                    </Button>
                </Form>
            </Modal.Body>
        </Modal>
    )
};

export default EditItemModal;