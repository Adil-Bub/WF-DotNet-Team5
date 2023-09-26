import React, { useState, useEffect } from 'react';
import { Button, Modal, Form, Row, Col } from 'react-bootstrap';
import axios from 'axios';

//Todo validation errors
const EditEmployeeModal = ({ showModal, handleCloseModal, selectedEmployee, setShowModal }) => {
    
    const baseUrl = 'https://localhost:7189/api';

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;

    const [employee, setEmployee] = useState(selectedEmployee);

    useEffect(() => {
        setEmployee(selectedEmployee);
    }, [selectedEmployee]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setEmployee({ ...employee, [name]: value });
    };

    function handleSubmit(event) {
        event.preventDefault();
        axios
            .put(`${baseUrl}/Employee/${employee.employeeId}`, employee, {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                alert('Successfully edited employee details!');
                //setShowModal(false);
            }).catch((error) => {
                alert('Error editing employee details! ', error);
            });
    }

    return (
        <Modal show={showModal} onHide={handleCloseModal}>
            <Modal.Header closeButton>
                <Modal.Title>Edit details of Employee</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmit}>
                    <Form.Group controlId='formName'>
                        <Form.Label>Employee Name</Form.Label>
                        <Form.Control
                            type='text'
                            name='employeeName'
                            value={employee.employeeName}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId='formDesignation'>
                        <Form.Label>Designation</Form.Label>
                        <Form.Control
                            type='text'
                            name='designation'
                            value={employee.designation}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId='formDepartment'>
                        <Form.Label>Department</Form.Label>
                        <Form.Control
                            type='text'
                            name='department'
                            value={employee.department}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId='formGender'>
                        <Form.Label>Gender</Form.Label>
                        <Form.Control
                            as="select"
                            name="gender"
                            value={employee.gender == 'F' || employee.gender == 'f'  ? 'F': 'M'}
                            onChange={handleChange}
                        >
                            <option value={"M"}>Male</option>
                            <option value={"F"}>Female</option>
                        </Form.Control>
                    </Form.Group>

                    <Form.Group controlId='formDateOfBirth'>
                        <Form.Label>Date Of Birth</Form.Label>
                        <Form.Control
                            type="date"
                            name="dateOfBirth"
                            value={(employee.dateOfBirth).substring(0, 10)}
                            onChange={handleChange}
                        />
                    </Form.Group>

                    <Form.Group controlId='formDateOfJoining'>
                        <Form.Label>Date of Joining</Form.Label>
                        <Form.Control
                            type="date"
                            name="dateOfJoining"
                            value={(employee.dateOfJoining).substring(0, 10)}
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

export default EditEmployeeModal;