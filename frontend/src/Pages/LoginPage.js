import React, { useState } from "react";
import axios from 'axios';
import { useNavigate, Link } from 'react-router-dom';
import { NavBar } from "../Component/Navbar";

import { showErrorToast, showSuccessfulToast } from '../Util/toast';
import Spinner from 'react-bootstrap/Spinner';

import { useFormik } from 'formik';
import { loginObjSchema } from "../Util/validation";

const LoginWithToken = () => {

    const baseUrl = 'https://localhost:7189/api';

    const initialValues = { 
        employeeId: '', 
        password: '' 
    };

    const navigate = useNavigate();

    const formik = useFormik({
        initialValues,
        validationSchema: loginObjSchema,
        onSubmit: async (values) => {
            try {
                const response = await axios
                    .post(`${baseUrl}/Authorization/login`, values);
                localStorage.setItem('user', JSON.stringify(response.data));
                showSuccessfulToast("Login successful!")
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
                <div className="container d-flex justify-content-center align-items-center vh-100">
                    <div className="card shadow p-4">
                        <form className="card-body" onSubmit={formik.handleSubmit}>
                            <h3 className="text-center">Login</h3>
                            <div className="mb-3">
                                <label htmlFor="employeeId" className="form-label">Employee ID</label>
                                <input
                                    type="text"
                                    className={`form-control ${formik.touched.employeeId && formik.errors.employeeId ? 'is-invalid' : ''
                                        }`}
                                    id="employeeId"
                                    name="employeeId"
                                    placeholder="Enter Employee ID"
                                    value={formik.values.employeeId}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                />
                                {formik.touched.employeeId && formik.errors.employeeId ? (
                                    <div className="invalid-feedback">{formik.errors.employeeId}</div>
                                ) : null}
                            </div>
                            <div className="mb-3">
                                <label htmlFor="password" className="form-label">Password</label>
                                <input
                                    type="password"
                                    className={`form-control ${formik.touched.password && formik.errors.password ? 'is-invalid' : ''
                                        }`}
                                    id="password"
                                    name="password"
                                    placeholder="Enter Password"
                                    value={formik.values.password}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                />
                                {formik.touched.password && formik.errors.password ? (
                                    <div className="invalid-feedback">{formik.errors.password}</div>
                                ) : null}
                            </div>
                            <div className="d-grid mt-4">
                                <button type="submit" className="btn btn-dark">Login</button>
                            </div>
                            <p className="text-center mt-2">
                                Don't have an account? <Link to="/register">Sign Up</Link>
                            </p>
                        </form>
                    </div>
                </div>
            )}
        </div>
    )
}

export default LoginWithToken;
