import React, { useContext, useEffect, useState } from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import { useNavigate } from 'react-router-dom';
import { FaEdit, FaTrash } from 'react-icons/fa';
import EditEmployeeModal from "../Component/EditEmployeeModal";

const EmployeeDataPage = () => {

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;
    const [employees, setEmployees] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [selectedEmployee, setSelectedEmployee] = useState({});
    const navigate = useNavigate();

    const handleCloseModal = () => {
        setSelectedEmployee(null);
        setShowModal(false);
    };

    function deleteEmployee(){
        axios.delete(`https://localhost:7189/api/Employee/${selectedEmployee.employeeId}`, {
            headers: { 'Authorization': 'Bearer ' + user.token }
        })
        .then((response) => {
            alert('Successfully deleting employee details!');
            setSelectedEmployee({});
        }).catch((error) => {
            alert('Error deleting employee details! ', error);
        });
    };

    useEffect(() => {
        axios
            .get('https://localhost:7189/api/Employee/all', {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                setEmployees(response.data);
                //console.log(response.data);
            }).catch((error) => {
                alert('Error fetching data ', error);
            });
    }, [selectedEmployee]);

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
                                                setSelectedEmployee(item);
                                                setShowModal(true);
                                            }}></FaEdit>
                                        </td>
                                        <td>
                                            <FaTrash className="delete-icon" color="red" onClick={() => {
                                                setSelectedEmployee(item);
                                                deleteEmployee();
                                            }}></FaTrash>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                        {showModal && <EditEmployeeModal
                            showModal={showModal}
                            handleCloseModal={handleCloseModal}
                            selectedEmployee={selectedEmployee}
                            setShowModal={setShowModal}
                        >
                        </EditEmployeeModal>}
                    </div>
                </div>
            </div>
        </div>
    );
}

export default EmployeeDataPage;