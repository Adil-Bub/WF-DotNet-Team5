import React, { useState, useEffect } from 'react';
import { Button, Modal, Form, Row, Col } from 'react-bootstrap';
import axios from 'axios';

//Todo validation errors
const AddLoanCardModal = ({ showAdd, handleAdd, setShowAdd }) => {
    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;
    
    const [loanCardObj, setLoanCardObj] = useState({ loanType: '', durationInYears: '' });
    const [loanType, setLoanType] = useState('');
    const [durationInYears, setDurationInYears] = useState('');
    
    const [errors, setErrors] = useState({});


    function handleSubmit(event) {
        event.preventDefault();
        loanCardObj.loanType = loanType;
        loanCardObj.durationInYears = durationInYears;

        axios
            .post(`https://localhost:7189/api/LoanCard/add`, loanCardObj, {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                alert('Successfully added loan-card details!');
            }).catch((error) => {
                alert('Error adding loan-card details! ', error);
            });
    }

    return (
        <Modal show={showAdd} onHide={handleAdd}>
            <Modal.Header closeButton>
                <Modal.Title>Edit details of Item</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmit}>
                    <Form.Group controlId='formCategory'>
                        <Form.Label>Loan Category</Form.Label>
                        <Form.Control
                            type='text'
                            name='loanType'
                            onChange={(event) => {setLoanType(event.target.value)}}
                        />
                    </Form.Group>
                    <Form.Group controlId='formDurationInYears'>
                        <Form.Label>Duration in Years</Form.Label>
                        <Form.Control
                            type='text'
                            name='durationInYears'
                            onChange={(event) => {setDurationInYears(event.target.value)}}
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

export default AddLoanCardModal;