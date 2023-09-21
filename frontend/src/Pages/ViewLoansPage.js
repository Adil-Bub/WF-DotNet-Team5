import React, { useContext, useState, useEffect} from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import { useNavigate, Link } from 'react-router-dom';
import { NavBar } from "../Component/Navbar";


const imgObj = {
    "Furniture" : "https://i.imgur.com/EryUuGM.jpg",
    "Crockery" : "https://i.imgur.com/XrFaJqg.jpg",
    "Stationary" : "https://i.imgur.com/aYVbZ1I.jpg"
};


const ViewLoansPage = () => {

    const { user, setUser } = useContext(AppContext);
    const [ loanCards, setLoanCards ] = useState([]);
    const navigate = useNavigate();

    const fetchData = async () => {
        const response = await axios.get('https://localhost:7189/api/LoanCard/all');
        await setLoanCards(response.data);
        
    }

    useEffect(() => {
        fetchData();
    }, [])
    
    return (
        <>
        <NavBar/>
        <div className="text-center">
            <h1>
                Loan Management Application  
            </h1>
            <div class="container text-center d-flex gap-3 mt-5">
                {loanCards.map(loanCard => (
                    <div class="card w-25 mx-auto fs-5" key={loanCard.LoanId}>
                        <img src = {imgObj[loanCard.loanType]} class="card-img-top" alt={loanCard.loanType} />
                        <div class="card-body">
                            <h3 class="card-title">{loanCard.loanType}</h3>
                            <p class="card-text"><strong>Duration:</strong> {loanCard.durationInYears} Years</p>
                            <Link to="apply" class="btn btn-lg btn-primary">Apply for Loan</Link>
                        </div>
                    </div>
                ))}
            </div>           
        </div>
        </>
    );
}

export default ViewLoansPage;