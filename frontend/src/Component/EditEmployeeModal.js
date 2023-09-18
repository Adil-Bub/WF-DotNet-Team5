import React, { useState, useContext, useEffect } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';
import axios from 'axios';
import { AppContext } from '../Context/App.context';

//Todo validation errors
const EditEmployeeModal = ({ showModal, handleCloseModal, selectedEmployee }) => {
    const { user, setUser } = useContext(AppContext);
    const [employee, setEmployee] = useState(selectedEmployee);
    const [errors, setErrors] = useState({});

    useEffect(() => {
        setEmployee(selectedEmployee);
    }, [selectedEmployee]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setEmployee({ ...employee, [name]: value });
    };

    function handleSubmit() {
        axios
            .put('https://localhost:7189/api/Employee/${employee.employeeId}', employee, {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                console.log(response.data);
                alert('Successfully edited employee details!');
            }).catch((error) => {
                alert('Error editing employee details! ', error);
            });
    }

    return (
        <Modal show={showModal} onHide={handleCloseModal}>
            <Modal.Header closeButton>
                <Modal.Title>Edit details of Employee {employee.employeeName}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmit}>
                    <Form.Group controlId='formName'>
                        <Form.Label>Employee Name</Form.Label>
                        <Form.Control
                            type='text'
                            name='name'
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
                            value={employee.gender}
                            onChange={handleChange}
                        />
                        <option>M</option>
                        <option>F</option>
                    </Form.Group>
                    <Form.Group controlId='formDateOfBirth'>
                        <Form.Control
                            type="date"
                            name="dateOfBirth"
                            value={employee.dateOfBirth}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Form.Group controlId='formDateOfJoining'>
                        <Form.Control
                            type="date"
                            name="dateOfJoining"
                            value={employee.dateOfJoining}
                            onChange={handleChange}
                        />
                    </Form.Group>
                    <Button variant="primary" type="submit">
                        Submit
                    </Button>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleCloseModal}>
                    Close
                </Button>
            </Modal.Footer>
        </Modal>
    )
};

export default EditEmployeeModal;