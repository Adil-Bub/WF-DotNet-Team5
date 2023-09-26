import React, { useState } from "react";
import axios from 'axios';
import { useNavigate, Link } from 'react-router-dom';
import { NavBar } from "../Component/Navbar";

import { showErrorToast, showSuccessfulToast } from '../Util/toast';
import Spinner from 'react-bootstrap/Spinner';

import { useFormik } from 'formik';
import { registerSchema } from "../Util/validation";

const RegistrationPage = () => {

    const baseUrl = 'https://localhost:7189/api';

    const initialValues = {
        password: '',
        confirmPassword: '',
        employeeName: '',
        designation: '',
        department: '',
        gender: '',
        dateOfBirth: '',
    };

    const navigate = useNavigate();

    const formik = useFormik({
        initialValues,
        validationSchema: registerSchema,
        onSubmit: async (values) => {
            try {
                const response = await axios
                    .post(`${baseUrl}/Authorization/register`, values);
                localStorage.setItem('user', JSON.stringify(response.data));
                showSuccessfulToast("You have succesfully registered!")
                if (response.data.designation === 'admin') {
                    navigate('/dashboard/admin');
                }
                else {
                    navigate('/dashboard/user');
                }
            } catch (error) {
                if (error == null || error.response == null) {
                    showErrorToast(error.message);
                } else {
                    showErrorToast(error.response.data);
                }
            }
        }
    })

    return (
        <div>
            <NavBar />
            {formik.isSubmitting ? (
                <div className="position-absolute top-50 start-50">
                    <Spinner animation="border" role="status" />
                </div>
            ) : (
                <div className="container d-flex justify-content-center align-items-center mt-3">
                    <div className="card shadow p-4 align-items-center">
                        <h4>
                            👋 Register here!
                        </h4>
                        <h6 className="mb-2">
                            Please Enter Your Details:
                        </h6>
                        <form className="card-body" onSubmit={formik.handleSubmit}>
                            <div className="container">
                                <div className="row">
                                    <div className="mb-2 col">
                                        <label htmlFor="userName" className="form-label">Employee Name</label>
                                        <input
                                            type="text"
                                            className={`form-control ${formik.touched.employeeName && formik.errors.employeeName ? 'is-invalid' : ''
                                                }`}
                                            id="employeeName"
                                            name="employeeName"
                                            placeholder="Enter Employee Name"
                                            value={formik.values.employeeName}
                                            onChange={formik.handleChange}
                                            onBlur={formik.handleBlur}
                                        />
                                        {formik.touched.employeeName && formik.errors.employeeName ? (
                                            <div className="invalid-feedback">{formik.errors.employeeName}</div>
                                        ) : null}
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="mb-2 col">
                                        <label htmlFor="password" className="form-label">Password</label>
                                        <input
                                            type="password"
                                            className={`form-control ${formik.touched.password && formik.errors.password ? 'is-invalid' : ''
                                                }`}
                                            id="password"
                                            name="password"
                                            placeholder="Set password"
                                            value={formik.values.password}
                                            onChange={formik.handleChange}
                                            onBlur={formik.handleBlur}
                                        />
                                        {formik.touched.password && formik.errors.password ? (
                                            <div className="invalid-feedback">{formik.errors.password}</div>
                                        ) : null}
                                    </div>
                                    <div className="mb-2 col">
                                        <label htmlFor="confirmPassword" className="form-label">Confirm Password</label>
                                        <input
                                            type="password"
                                            className={`form-control ${formik.touched.confirmPassword && formik.errors.confirmPassword ? 'is-invalid' : ''
                                                }`}
                                            id="confirmPassword"
                                            name="confirmPassword"
                                            placeholder="Confirm your password"
                                            value={formik.values.confirmPassword}
                                            onChange={formik.handleChange}
                                            onBlur={formik.handleBlur}
                                        />
                                        {formik.touched.confirmPassword && formik.errors.confirmPassword ? (
                                            <div className="invalid-feedback">{formik.errors.confirmPassword}</div>
                                        ) : null}
                                    </div>
                                </div>

                                <div className="row">
                                    <div className="mb-2 col">
                                        <label htmlFor="designation" className="form-label">Designation</label>
                                        <input
                                            type="text"
                                            className={`form-control ${formik.touched.designation && formik.errors.designation ? 'is-invalid' : ''
                                                }`}
                                            id="designation"
                                            name="designation"
                                            placeholder="Enter your Designation"
                                            value={formik.values.designation}
                                            onChange={formik.handleChange}
                                            onBlur={formik.handleBlur}
                                        />
                                        {formik.touched.designation && formik.errors.designation ? (
                                            <div className="invalid-feedback">{formik.errors.designation}</div>
                                        ) : null}
                                    </div>
                                    <div className="mb-2 col">
                                        <label htmlFor="department" className="form-label">Department</label>
                                        <input
                                            type="text"
                                            className={`form-control ${formik.touched.department && formik.errors.department ? 'is-invalid' : ''
                                                }`}
                                            id="department"
                                            name="department"
                                            placeholder="Enter your department"
                                            value={formik.values.department}
                                            onChange={formik.handleChange}
                                            onBlur={formik.handleBlur}
                                        />
                                        {formik.touched.department && formik.errors.department ? (
                                            <div className="invalid-feedback">{formik.errors.department}</div>
                                        ) : null}
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="mb-2 col">
                                        <label htmlFor="gender" className="form-label">
                                            Gender
                                        </label>
                                        <select
                                            className={`form-select ${formik.touched.gender && formik.errors.gender ? 'is-invalid' : ''}`}
                                            id="gender"
                                            name="gender"
                                            value={formik.values.gender}
                                            onChange={formik.handleChange}
                                            onBlur={formik.handleBlur}
                                        >
                                            <option value="" disabled>
                                                Select your gender
                                            </option>
                                            <option value="M">Male</option>
                                            <option value="F">Female</option>
                                        </select>
                                        {formik.touched.gender && formik.errors.gender ? (
                                            <div className="invalid-feedback">{formik.errors.gender}</div>
                                        ) : null}
                                    </div>
                                    <div className="mb-2 col">
                                    <label htmlFor="dateOfBirth" className="form-label">Date of Birth</label>
                                        <input
                                            type="date"
                                            className={`form-control ${formik.touched.dateOfBirth && formik.errors.dateOfBirth ? 'is-invalid' : ''
                                                }`}
                                            id="dateOfBirth"
                                            name="dateOfBirth"
                                            placeholder="Enter your birth date"
                                            value={formik.values.dateOfBirth}
                                            onChange={formik.handleChange}
                                            onBlur={formik.handleBlur}
                                        />
                                        {formik.touched.dateOfBirth && formik.errors.dateOfBirth ? (
                                            <div className="invalid-feedback">{formik.errors.dateOfBirth}</div>
                                        ) : null}
                                    </div>
                                </div>
                                <div className="d-grid mt-3">
                                    <button type="submit" className="btn btn-dark">Register</button>
                                </div>
                                <p className="text-center mt-2">
                                    Already have an account? <Link to="/">Login</Link>
                                </p>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
}

export default RegistrationPage;
