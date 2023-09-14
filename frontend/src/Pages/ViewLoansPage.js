import React, { useContext, useState } from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import { useNavigate } from 'react-router-dom';

const ViewLoansPage = () => {

    const { user, setUser } = useContext(AppContext);
    const navigate = useNavigate();

   
    return (
        <div className="text-center">
            <h1>
                Loan Management Application  
            </h1>
            <h4>
                View Loans Page
            </h4>
            
        </div>
    );
}

export default ViewLoansPage;