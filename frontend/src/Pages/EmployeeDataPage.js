import React, { useEffect, useState } from "react";
import axios from 'axios';
import { FaEdit, FaTrash } from 'react-icons/fa';
import { NavBar } from "../Component/LAMANav";
import EditEmployeeModal  from "../Component/EditEmployeeModal";
import { showErrorToast, showInfoToast } from "../Util/toast";
import { useNavigate } from 'react-router-dom';

const EmployeeDataPage = () => {

    const baseUrl = 'https://localhost:7189/api';

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;

    const navigate = useNavigate();
    
    const [employees, setEmployees] = useState([]);
    const [showModal, setShowModal] = useState(false);
    const [selectedEmployee, setSelectedEmployee] = useState({});

    const handleCloseModal = () => {
        setSelectedEmployee(null);
        setShowModal(false);
    };

    useEffect(() => {
        axios
            .get(`${baseUrl}/Employee/all`, {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                setEmployees(response.data);
            }).catch((error) => {
                showErrorToast("Session expired! Please login!");
                navigate('/');
            });
    }, [selectedEmployee]);

    return (
        <div>
            <NavBar />
            <div className="container mt-5">
                <div className="row justify-content-center">
                    <div className="table-responsive">
                        <table className="table table-hover table-bordered" style={{borderRadius:'10px', overflow: 'hidden', border: '2px solid #ccc'}}>
                            <thead className="text-center align-items-center">
                                <tr>
                                    <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Employee Id</th>
                                    <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Employee Name</th>
                                    <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Designation</th>
                                    <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Department</th>
                                    <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Gender</th>
                                    <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Date of Birth</th>
                                    <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Date of Joining</th>
                                    <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Edit</th>
                                    <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Delete</th>
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
                                        <td>{item.dateOfBirth.substring(0,10)}</td>
                                        <td>{item.dateOfJoining.substring(0,10)}</td>
                                        <td>
                                            <FaEdit className="edit-icon" color="blue" onClick={() => {
                                                if(item.employeeId != user.employeeId){
                                                    setSelectedEmployee(item);
                                                    setShowModal(true);
                                                }else{
                                                    showErrorToast("Cannot change self roles!");
                                                }
                                            }}></FaEdit>
                                        </td>
                                        <td>
                                            <FaTrash className="delete-icon" color="red" onClick={() => {
                                                if(item.employeeId != user.employeeId){
                                                    setSelectedEmployee(item);
                                                    axios
                                                    .delete(`${baseUrl}/Employee/${item.employeeId}`, {
                                                            headers: { 'Authorization': 'Bearer ' + user.token }
                                                    }).then((response) => {
                                                        showInfoToast("Deleted employee");
                                                        setSelectedEmployee({});
                                                    }).catch((error) => {
                                                        showErrorToast(error.message);
                                                        setSelectedEmployee({});
                                                    });
                                                }else{
                                                    showErrorToast("Cannot delete self!");
                                                }
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
                        >
                        </EditEmployeeModal>}
                    </div>
                </div>
            </div>
        </div>
    );
}

export default EmployeeDataPage;