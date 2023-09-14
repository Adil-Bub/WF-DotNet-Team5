import React, { useContext, useState } from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import { useNavigate } from 'react-router-dom';

const ApplyLoansPage = () => {

    const { user, setUser } = useContext(AppContext);
    const navigate = useNavigate();

   
    return (
        <div class="text-center">
            <h1>
                Loan Management Application  
            </h1>
            <h4>
                Apply Loans Page
            </h4>
            
        </div>
    );
}

export default ApplyLoansPage;