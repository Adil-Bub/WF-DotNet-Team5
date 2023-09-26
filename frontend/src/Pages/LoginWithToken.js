import React, { useState } from "react";
import axios from 'axios';
import { useNavigate, Link } from 'react-router-dom';
import { NavBar } from "../Component/Navbar";

import { showErrorToast, showSuccessfulToast } from '../Util/toast';
import Spinner from 'react-bootstrap/Spinner';

const LoginWithToken = () => {

    const baseUrl = 'https://localhost:7189/api';

    const [loginobj, setLogin] = useState({ EmployeeId: '', Password: '' });
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [isLoading, setIsLoading] = useState(false);

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;

    const navigate = useNavigate();

    const handleUsername = (event) => {
        setUsername(event.target.value);
    }
    const handlepwd = (event) => {
        setPassword(event.target.value);
    }

    const handleSubmit = async (event) => {
        loginobj.EmployeeId = username;
        loginobj.Password = password;
        event.preventDefault();
        try {
            setIsLoading(true);
            const response = await axios
                .post(`${baseUrl}/Authorization/login`, loginobj);
            localStorage.setItem('user', JSON.stringify(response.data));
            setIsLoading(false);
            showSuccessfulToast("Login successful!")
            if (response.data.designation === 'admin') {
                navigate('/dashboard/admin');
            }
            else {
                navigate('/dashboard/user');
            }
        }
        catch (error) {
            if (error == null || error.response == null) {
                showErrorToast(error.message);
            } else {
                showErrorToast(error.response.data);
            }
            setIsLoading(false);
        }
    }

    return (
        <div>
            <NavBar />
            {isLoading ? (
                <div className="position-absolute top-50 start-50">
                    <Spinner animation="border" role="status"/>
                </div>
            ) : (
                <div className="container d-flex justify-content-center align-items-center vh-100">
                    <div className="card shadow p-4">
                        <form className="card-body" onSubmit={handleSubmit}>
                            <h3 className="text-center">Login</h3>
                            <div className="mb-3">
                                <label htmlFor="employeeId" className="form-label">Employee ID</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    id="employeeId"
                                    placeholder="Enter Employee ID"
                                    value={username}
                                    onChange={handleUsername}
                                />
                            </div>
                            <div className="mb-3">
                                <label htmlFor="password" className="form-label">Password</label>
                                <input
                                    type="password"
                                    className="form-control"
                                    id="password"
                                    placeholder="Enter Password"
                                    value={password}
                                    onChange={handlepwd}
                                />
                            </div>
                            <div className="d-grid gap-2">
                                <button type="submit" className="btn btn-dark">Submit</button>
                            </div>
                            <p className="text-center mt-3">
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
