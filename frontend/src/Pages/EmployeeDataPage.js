import React, { useContext, useEffect, useState } from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import { useNavigate } from 'react-router-dom';
import { FaEdit, FaTrash } from 'react-icons/fa'

const EmployeeDataPage = () => {

    const { user, setUser } = useContext(AppContext);
    const [employees, setEmployees] = useState([]);
    const [editableEmployee, setEditableEmployee] = useState();
    const navigate = useNavigate();

    useEffect(() => {
        axios
            .get('https://localhost:7189/api/Employee/all', {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                setEmployees(response.data);
                console.log(response.data);
            }).catch((error) => {
                alert('Error fetching data ', error);
            });
    }, []);
    return (
        <div>
            <nav className="navbar" style={{ 'background': '#cce6ff' }}>
                <h4 className="pl-2">Loan management system: Employee details</h4>
            </nav>
            <div className="container mt-5">
                <div className="row justify-content-center">
                    <div className="table-responsive">
                        <table className="table table-hover table-bordered">
                            <thead className="text-center align-items-center">
                                <tr>
                                    <th className="align-middle">Employee Id</th>
                                    <th className="align-middle">Employee Name</th>
                                    <th className="align-middle">Designation</th>
                                    <th className="align-middle">Department</th>
                                    <th className="align-middle">Gender</th>
                                    <th className="align-middle">Date of Birth</th>
                                    <th className="align-middle">Date of Joining</th>
                                    <th className="align-middle">Edit</th>
                                    <th className="align-middle">Delete</th>
                                </tr>
                            </thead>
                            <tbody className="table-group-divider text-center">
                                {employees.map((item) => (
                                    <tr key={item.employeeId}>
                                        <td>{item.employeeId}</td>
                                        <td>{item.employeeName}</td>
                                        <td>{item.designation}</td>
                                        <td>{item.department}</td>
                                        <td>{item.gender}</td>
                                        <td>{item.dateOfBirth}</td>
                                        <td>{item.dateOfJoining}</td>
                                        <td>
                                            <FaEdit className="edit-icon" color="blue" onClick={() => {
                                                setEditableEmployee(...item)
                                            }}></FaEdit>
                                        </td>
                                        <td>
                                            <FaTrash className="delete-icon" color="red"></FaTrash>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default EmployeeDataPage;