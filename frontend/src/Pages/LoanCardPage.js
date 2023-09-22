import React, { useContext, useState,useEffect } from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import { useNavigate } from 'react-router-dom';
import { FaEdit, FaTrash } from 'react-icons/fa'
import { NavBar } from "../Component/LAMANav";


const LoanCardPage = () => {

    const { user, setUser } = useContext(AppContext);
    const navigate = useNavigate();
    const [loanCards, setLoanCards] = useState([]);





    useEffect(() => {
        axios
            .get('https://localhost:7189/api/LoanCard/all', {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                setLoanCards(response.data);
                console.log(response.data);
            })
            .catch((error) => {
                console.error('Error fetching data: ', error);
            });
        }, [user.token]);
   
    return (
       

                <div>
                    <NavBar/>
                    
                        <h4 className="pl-2">Loan management system: Admin</h4>
                 
                    <div className="container mt-5">
                        <div className="row justify-content-center">
                            <div className="table-responsive">
                                <table className="table table-hover table-bordered">
                                    <thead className="text-center align-items-center">
                                        <tr>
                                            <th className="align-middle">Loan_Id</th>
                                            <th className="align-middle">Loan_Type</th>
                                            <th className="align-middle">Duration</th>
                                            <th className="align-middle">Edit</th>
                                            <th className="align-middle">Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody className="table-group-divider text-center">
                                        {loanCards.map((item) => (
                                    <tr key={item.loanId}>
                                        <td>{item.loanId}</td>
                                        <td>{item.loanType}</td>
                                        <td>{item.durationInYears}</td>
                                        <td>
                                                    <FaEdit className="edit-icon" color="blue" onClick={() => {
                                                    
                                                    }}></FaEdit>
                                                </td>
                                                <td>
                                                    <FaTrash className="delete-icon" color="red"></FaTrash>
                                                </td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            );
        }
        
    


export default LoanCardPage;