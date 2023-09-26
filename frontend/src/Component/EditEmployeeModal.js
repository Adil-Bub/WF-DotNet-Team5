import React, { useState, useEffect } from 'react';
import { Button, Modal, Form } from 'react-bootstrap';
import axios from 'axios';
import { useFormik } from 'formik';
import { editEmployeeSchema } from '../Util/validation';
import { showErrorToast, showSuccessfulToast } from '../Util/toast';

//Todo validation errors
const EditEmployeeModal = ({ showModal, handleCloseModal, selectedEmployee }) => {

    const baseUrl = 'https://localhost:7189/api';

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;

    const formik = useFormik({
        initialValues: {
            employeeId: selectedEmployee.employeeId,
            employeeName: selectedEmployee.employeeName,
            designation: selectedEmployee.designation,
            department: selectedEmployee.department,
            gender: selectedEmployee.gender,
            dateOfBirth: selectedEmployee.dateOfBirth,
            dateOfJoining: selectedEmployee.dateOfJoining
        },
        validationSchema: editEmployeeSchema,
        onSubmit: (values) => {
            console.log(values);
            axios
                .put(`${baseUrl}/Employee/${selectedEmployee.employeeId}`, values, {
                    headers: { 'Authorization': 'Bearer ' + user.token }
                })
                .then((response) => {
                    showSuccessfulToast("Successfully edited employee details!");
                }).catch((error) => {
                    showErrorToast("Error editing employee details. Please try again later!");
                });
        }
    })

    return (
        <Modal show={showModal} onHide={handleCloseModal}>
            <Modal.Header closeButton>
                <Modal.Title>Edit details of Employee</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={formik.handleSubmit}>
                    <Form.Group controlId='formName'>
                        <Form.Label>Employee Name</Form.Label>
                        <Form.Control
                            type='text'
                            className={`form-control ${formik.touched.employeeName && formik.errors.employeeName ? 'is-invalid' : ''
                                }`}
                            name='employeeName'
                            value={formik.values.employeeName}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                        />
                        {formik.touched.employeeName && formik.errors.employeeName ? (
                            <div className="invalid-feedback">{formik.errors.employeeName}</div>
                        ) : null}
                    </Form.Group>
                    <Form.Group controlId='formDesignation'>
                        <Form.Label>Designation</Form.Label>
                        <Form.Control
                            type='text'
                            name='designation'
                            className={`form-control ${formik.touched.designation && formik.errors.designation ? 'is-invalid' : ''
                                }`}
                            value={formik.values.designation}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                        />
                        {formik.touched.designation && formik.errors.designation ? (
                            <div className="invalid-feedback">{formik.errors.designation}</div>
                        ) : null}
                    </Form.Group>
                    <Form.Group controlId='formDepartment'>
                        <Form.Label>Department</Form.Label>
                        <Form.Control
                            type='text'
                            name='department'
                            className={`form-control ${formik.touched.department && formik.errors.department ? 'is-invalid' : ''
                                }`}
                            value={formik.values.department}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                        />
                        {formik.touched.department && formik.errors.department ? (
                            <div className="invalid-feedback">{formik.errors.department}</div>
                        ) : null}
                    </Form.Group>
                    <Form.Group controlId='formGender'>
                        <Form.Label>Gender</Form.Label>
                        <Form.Control
                            as="select"
                            name="gender"
                            className={`form-select ${formik.touched.gender && formik.errors.gender ? 'is-invalid' : ''}`}
                            value={formik.values.gender}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                        >
                            <option value={"M"}>Male</option>
                            <option value={"F"}>Female</option>
                        </Form.Control>
                        {formik.touched.gender && formik.errors.gender ? (
                            <div className="invalid-feedback">{formik.errors.gender}</div>
                        ) : null}
                    </Form.Group>

                    <Form.Group controlId='formDateOfBirth'>
                        <Form.Label>Date Of Birth</Form.Label>
                        <Form.Control
                            type="date"
                            name="dateOfBirth"
                            className={`form-control ${formik.touched.dateOfBirth && formik.errors.dateOfBirth ? 'is-invalid' : ''
                                }`}
                            value={formik.values.dateOfBirth.substring(0, 10)}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                        />
                        {formik.touched.dateOfBirth && formik.errors.dateOfBirth ? (
                            <div className="invalid-feedback">{formik.errors.dateOfBirth}</div>
                        ) : null}
                    </Form.Group>

                    <Form.Group controlId='formDateOfJoining'>
                        <Form.Label>Date of Joining</Form.Label>
                        <Form.Control
                            type="date"
                            name="dateOfJoining"
                            className={`form-control ${formik.touched.dateOfJoining && formik.errors.dateOfJoining ? 'is-invalid' : ''
                                }`}
                            value={formik.values.dateOfJoining.substring(0, 10)}
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                        />
                        {formik.touched.dateOfJoining && formik.errors.dateOfJoining ? (
                            <div className="invalid-feedback">{formik.errors.dateOfJoining}</div>
                        ) : null}
                    </Form.Group>
                    <Button variant="primary" type="submit" className='mt-3'>
                        Submit
                    </Button>
                </Form>
            </Modal.Body>
        </Modal>
    )
};

export default EditEmployeeModal;
