import React, { useState } from "react";
import axios from 'axios';

import { NavBar } from "../Component/LAMANav";
const ItemsMasterDataPage = () => {

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
                Items Master Data
            </h4>
            
        </div>
        </>
    );
}

export default ItemsMasterDataPage;