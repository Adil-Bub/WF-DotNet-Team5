import React, { useState } from "react";
import axios from 'axios';
import { useNavigate, Link } from 'react-router-dom';
import { NavBar } from "../Component/Navbar";

const LoginWithToken = () => {
    const [loginobj, setLogin] = useState({ EmployeeId: '', Password: '' });
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(false);

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

            const response = await axios
                .post('https://localhost:7189/api/Authorization/login', loginobj)
            // setUser(response.data);
            localStorage.setItem('user', JSON.stringify(response.data));
            console.log(response.data);
            if (response.data.designation === 'admin') {
                navigate('/dashboard/admin');
            }
            else {
                navigate('/dashboard/user');
            }

        }
        catch (error) {
            setError(true);
        }
    }
    return (
        <div>
            <NavBar/>
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
            {error && (
                <div className="alert alert-danger mt-3" role="alert">
                    Login failed. Please check your Credentials.
                </div>
            )}
            <p className="text-center mt-3">
              Don't have an account? <Link to="/register">Sign Up</Link>
            </p>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default LoginWithToken;
