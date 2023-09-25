import React, { useState } from "react";
import { NavBar } from "../Component/LUMANav";

const ViewItemsPurchasedPage = () => {

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;

   
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