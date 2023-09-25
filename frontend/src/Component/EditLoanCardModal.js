import React, { useState, useEffect } from 'react';
import { Button, Modal, Form, Row, Col } from 'react-bootstrap';
import axios from 'axios';

//Todo validation errors
const EditLoanCardModal = ({ showModal, handleCloseModal, selectedLoanCard, setShowModal }) => {
    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;
    const [loanCard, setLoanCard] = useState(selectedLoanCard);
    const [errors, setErrors] = useState({});

    useEffect(() => {
        setLoanCard(selectedLoanCard);
    }, [selectedLoanCard]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setLoanCard({ ...loanCard, [name]: value });
    };

    function handleSubmit(event) {
        event.preventDefault();
        axios
            .put(`https://localhost:7189/api/LoanCard/${loanCard.loanId}`, loanCard, {
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
                <Modal.Title>Edit details of Loan Card</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmit}>
                    <Form.Group controlId='formType'>
                        <Form.Label>Loan Type</Form.Label>
                        <Form.Control
                            type='text'
                            name='loanType'
                            value={loanCard.loanType}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId='formDesignation'>
                        <Form.Label>durationInYears</Form.Label>
                        <Form.Control
                            type='text'
                            name='durationInYears'
                            value={loanCard.durationInYears}
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

export default EditLoanCardModal;