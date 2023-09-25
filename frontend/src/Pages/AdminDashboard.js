import React from "react";
import { useNavigate } from 'react-router-dom';
import { NavBar } from "../Component/LAMANav";

const AdminDashboard = () => {

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;

    const navigate = useNavigate();

    const navigateToEmployeeData = () => {
        navigate('/dashboard/admin/employee-data');
    }

    const navigateToLoanCardManagement = () => {
        navigate('/dashboard/admin/loan-card');
    }

    const navigateToItemsMasterData = () => {
        navigate('/dashboard/admin/all-items');
    }

    return (
        <>
            <NavBar />
            <div className="text-center">
                <h2 style={{marginTop: '20px'}}>
                    Admin Dashboard
                </h2>
                <h4>
                    Welcome {user.employeeName}!
                </h4>

                <div className="container w-50 p-5 mb-4 text-center fs-5">
                    <div className="d-inline-flex gap-3">
                        <button type="button" className="btn btn-outline-dark" onClick={navigateToEmployeeData}>Employee Data Management</button>


                        <button type="button" className="btn btn-outline-dark" onClick={navigateToLoanCardManagement}>Loan Card Management</button>


                        <button type="button" className="btn btn-outline-dark" onClick={navigateToItemsMasterData}>Items Master Data</button>
                    </div>
                </div>
            </div>
        </>
    );
}

export default AdminDashboard;
