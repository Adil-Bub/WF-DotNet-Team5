import React, { useContext, useState } from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import { useNavigate } from 'react-router-dom';

const ItemsMasterDataPage = () => {

    const { user, setUser } = useContext(AppContext);
    const navigate = useNavigate();

   
    return (
        <div class="text-center">
            <h1>
                Loan Management Application  
            </h1>
            <h4>
                Items Master Data
            </h4>
            
        </div>
    );
}

export default ItemsMasterDataPage;