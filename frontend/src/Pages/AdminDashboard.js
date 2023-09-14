import React, { useContext, useState } from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import { useNavigate } from 'react-router-dom';

const AdminDashboard = () => {

    const { user, setUser } = useContext(AppContext);
    const navigate = useNavigate();

    const navigateToCustomerData = () => {
        navigate('/dashboard/admin/customer-data');
    }

    const navigateToLoanCardManagement = () => {
        navigate('/dashboard/admin/loan-card');
    }

    const navigateToItemsMasterData = () => {
        navigate('/dashboard/admin/all-items');
    }
    return (
        <div class="text-center">
            <h1>
            Loan Management Application  
            </h1>
            <h4>
                Admin Dashboard for {user.employeeId}
            </h4>

            <div class="container w-50 p-5 mb-4 text-center fs-5">
                <div class="d-inline-flex gap-3">
                    <button type="button" class="btn btn-outline-dark" onClick={navigateToCustomerData}>Customer Data Management</button>
                
               
                    <button type="button" class="btn btn-outline-dark" onClick={navigateToLoanCardManagement}>Loan Card Management</button>
                
                
                    <button type="button" class="btn btn-outline-dark" onClick={navigateToItemsMasterData}>Items Master Data</button>
                </div> 
            </div>
        </div>
    );
}

export default AdminDashboard;