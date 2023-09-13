import React, { useContext, useState } from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import { useNavigate } from 'react-router-dom';
//npm install react-router-dom 
const LoginWithToken = () => {
    const [loginobj, setLogin] = useState({ EmployeeId: '', Password: '' });
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(false);
    const { user, setUser } = useContext(AppContext);
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
            //.get('./data.json')
            setUser(response.data);
            console.log(response.data);
            if (response.data.designation === 'admin') {
                navigate('/profile');
            }

        }
        catch (error) {
            setError(error.Message);
        }
    }
    return (
        <div>
            <nav class="navbar navbar-light bg-light">
                <h4 class="p-2">Loan management system</h4>
            </nav>
            <div class="container d-flex justify-content-center align-items-center vh-100">
                <div class="card shadow p-4">
                    <form class="card-body" onSubmit={handleSubmit}>
                        <h3 class="text-center">Login</h3>
                        <div class="mb-3">
                            <label htmlFor="employeeId" class="form-label">Employee id</label>
                            <input
                                type="text"
                                class="form-control"
                                id="employeeId"
                                placeholder="Enter employee id"
                                value={username}
                                onChange={handleUsername}
                            />
                        </div>
                        <div class="mb-3">
                            <label htmlFor="password" class="form-label">Password</label>
                            <input
                                type="password"
                                class="form-control"
                                id="password"
                                placeholder="Enter password"
                                value={password}
                                onChange={handlepwd}
                            />
                        </div>
                        <div class="d-grid gap-2">
                            <button type="submit" class="btn btn-primary btn-light">Submit</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
}

export default LoginWithToken;
