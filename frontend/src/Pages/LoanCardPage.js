import React, { useState,useEffect } from "react";
import axios from 'axios';
import { FaEdit, FaTrash } from 'react-icons/fa'
import { NavBar } from "../Component/LAMANav";
import EditLoanCardModal  from "../Component/EditLoanCardModal";


const LoanCardPage = () => {

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;
    
    const [showModal, setShowModal] = useState(false);
    const [selectedLoanCard, setSelectedLoanCard] = useState({});
    const [loanCards, setLoanCards] = useState([]);

    const handleCloseModal = () => {
        setSelectedLoanCard(null);
        setShowModal(false);
    };



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
        }, [selectedLoanCard]);
   
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
                                                    setSelectedLoanCard(item);
                                                    setShowModal(true);
                                                    }}></FaEdit>
                                                </td>
                                                <td>
                                                    <FaTrash className="delete-icon" color="red" onClick={() => {
                                                        axios
                                                        .delete(`https://localhost:7189/api/LoanCard/${item.loanId}`, {
                                                            headers: { 'Authorization': 'Bearer ' + user.token }
                                                        })
                                                        setLoanCards(loanCards.filter(loanCard => loanCard.loanId != item.loanId))
                                                        
                                                    }}></FaTrash>
                                                </td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                                {showModal && <EditLoanCardModal
                            showModal={showModal}
                            handleCloseModal={handleCloseModal}
                            selectedLoanCard={selectedLoanCard}
                            setShowModal={setShowModal}
                        >
                        </EditLoanCardModal>}
                            </div>
                        </div>
                    </div>
                </div>
            );
        }
        
    


export default LoanCardPage;