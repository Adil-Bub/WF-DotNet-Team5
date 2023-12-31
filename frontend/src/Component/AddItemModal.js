import React, { useState, useEffect } from 'react';
import { Button, Modal, Form, Row, Col } from 'react-bootstrap';
import axios from 'axios';
import { showErrorToast, showSuccessfulToast } from '../Util/toast';

//Todo validation errors
const AddItemModal = ({ showAdd, handleAdd, setShowAdd }) => {
    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;
    
    const [itemMasterObj, setitemMasterObj] = useState({ itemDescription: '',issueStatus: '',itemMake: '',itemCategory: '', itemValuation: '' });
    const [itemDescription, setitemDescription] = useState('');
    const [issueStatus, setissueStatus] = useState('');
    const [itemMake, setitemMake] = useState('');
    const [itemCategory, setitemCategory] = useState('');
    const [itemValuation, setitemValuation] = useState('');

    
    const [errors, setErrors] = useState({});


    function handleSubmit(event) {
        event.preventDefault();
        itemMasterObj.itemDescription = itemDescription;
        itemMasterObj.issueStatus = issueStatus;
        itemMasterObj.itemMake = itemMake;
        itemMasterObj.itemCategory = itemCategory;
        itemMasterObj.itemValuation = itemValuation;

        axios
            .post(`https://localhost:7189/api/Items/add`, itemMasterObj, {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                showSuccessfulToast("Successfully added item!");
            }).catch((error) => {
                showErrorToast("Error adding item!")
            });
    }

    return (
        <Modal show={showAdd} onHide={handleAdd}>
            <Modal.Header closeButton>
                <Modal.Title>Edit details of Item</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmit}>
                    <Form.Group controlId='formDesc'>
                        <Form.Label>Description</Form.Label>
                        <Form.Control
                            type='text'
                            name='itemDescription'
                            onChange={(event) => {setitemDescription(event.target.value)}}
                        />
                    </Form.Group>
                    <Form.Group controlId='formitemMake'>
                        <Form.Label>Make</Form.Label>
                        <Form.Control
                            type='text'
                            name='itemMake'
                            onChange={(event) => {setitemMake(event.target.value)}}
                        />
                    </Form.Group>
                    <Form.Group controlId='formitemCategory'>
                        <Form.Label>Category</Form.Label>
                        <Form.Control
                            type='text'
                            name='itemCategory'
                            onChange={(event) => {setitemCategory(event.target.value)}}
                        />
                    </Form.Group>
                    <Form.Group controlId='formitemValuation'>
                        <Form.Label>Valuation</Form.Label>
                        <Form.Control
                            type='text'
                            name='itemValuation'
                            onChange={(event) => {setitemValuation(event.target.value)}}
                        />
                    </Form.Group>
                    <Button variant="primary mt-3" type="submit">
                        Submit
                    </Button>
                </Form>
            </Modal.Body>
        </Modal>
    )
};

export default AddItemModal;