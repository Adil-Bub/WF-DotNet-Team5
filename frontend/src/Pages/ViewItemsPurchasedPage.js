import React, { useState } from "react";
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import { NavBar } from "../Component/LUMANav";
const ViewItemsPurchasedPage = () => {

    const storedUser = localStorage.getItem('user');
    const { user, setUser } = storedUser ? JSON.parse(storedUser) : null;
    const navigate = useNavigate();

   
    return (
        <>
        <NavBar/>
        <div className="text-center">
            <h1>
                Loan Management Application  
            </h1>
            <h4>
                View Items Purchased Page
            </h4>
            
        </div>
        </>
    );
}

export default ViewItemsPurchasedPage;