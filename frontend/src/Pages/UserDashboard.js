import React, { useContext, useState } from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import { useNavigate } from 'react-router-dom';

const UserDashboard = () => {

    const { user, setUser } = useContext(AppContext);
    const navigate = useNavigate();

    const navigateToViewLoans= () => {
        navigate('/dashboard/user/loans');
    }

    const navigateToApplyLoan = () => {
        navigate('/dashboard/user/loans/apply');
    }

    const navigateToItemsPurchased = () => {
        navigate('/dashboard/user/items=purchased');
    }
    return (
        <div className="text-center">
            <h1>
                Loan Management Application  
            </h1>
            <h4>
                Welcome {user.employeeId} to your Dashboard. 
            </h4>
            <div className="container w-50 p-5 mb-4 text-center fs-5">
                <div className="d-inline-flex gap-3">
                    <button type="button" className="btn btn-outline-dark" onClick={navigateToViewLoans}>View Loans</button>
                
               
                    <button type="button" className="btn btn-outline-dark" onClick={navigateToApplyLoan}>Apply Loans</button>
                
                
                    <button type="button" className="btn btn-outline-dark" onClick={navigateToItemsPurchased}>Items Purchased</button>
                </div> 
            </div>
        </div>
    );
}

export default UserDashboard;