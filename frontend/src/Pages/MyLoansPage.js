import React, { useContext, useState, useEffect} from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import { useNavigate, Link } from 'react-router-dom';
import { NavBar } from "../Component/LUMANav";

const Card = ({ myLoan }) => {
    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;
    const [isHovered, setIsHovered] = useState(false);
    const [approvalDetails, setApprovalDetails] = useState(null);

    const handleMouseEnter = async () => {
      if (myLoan.requestStatus === 'Approved') {
        // Fetch approval details from your API
        try {
          const response = await axios.get(`https://localhost:7189/approved/${myLoan.employeeId}`, {
            headers: { 'Authorization': 'Bearer ' + user.token }
        });
          if (response.status === 200) {
            const data = response.data[0];
            console.log("responsedata", response.data[0]);
            setApprovalDetails(data);
          }
        } catch (error) {
          console.error('Error fetching approval details:', error);
        }
      }
      setIsHovered(true);
    };
  
    const handleMouseLeave = () => {
      setIsHovered(false);
      console.log(approvalDetails);
      setApprovalDetails(null);
      
    };
    
    let cardClass = `card mb-5 ${isHovered && 'hovered-card'}`;
    if (myLoan.requestStatus == "Approved"){
      cardClass += ' bg-dark text-white'
    }
      
    
  
    return (
      <div
        className={cardClass}
        onMouseEnter={handleMouseEnter}
        onMouseLeave={handleMouseLeave}
      >
        <div className="card-header">
          <h3>{myLoan.itemDescription}</h3>
        </div>
        <div className="card-body">
          <p>Item ID: {myLoan.itemId}</p>
          <p>Make: {myLoan.itemMake}</p>
          <p>Category: {myLoan.itemCategory}</p>
          <p>Valuation: ₹ {myLoan.itemValuation}</p>
          <p>Request ID: {myLoan.requestId}</p>
          <p>Request Date: {myLoan.requestDate.substring(0,10)}</p>
          <p>Request Status: {myLoan.requestStatus}</p>
          {isHovered && approvalDetails && (
            <div className="approval-details">
              <p>Approved Loan Card Details:</p>
              <ul className="list-unstyled">
              <li>Card ID: {approvalDetails.cardId}</li>
              <li>Card Issue Date: {approvalDetails.cardIssueDate.substring(0,10).substring(0,10)}</li>
              </ul>
            </div>
          )}
          <div className="card-footer">
            { (myLoan.returnDate!=null) ? (
                <p>Return Date: {myLoan.returnDate} </p>
            ) : (
                <p>Return Date TBD by Admin</p>
            )}
            <p>Loan ID: {myLoan.loanId}</p>
            <p>Duration: {myLoan.durationInYears} Years</p>
          </div>
        </div>
      </div>
    );
  }
const MyLoansPage = () => {

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;

    const [ myLoanCards, setMyLoansCards ] = useState([]);
    const navigate = useNavigate();

 

    useEffect(() => {
        axios
            .get('https://localhost:7189/my-loans/' + user.employeeId, {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                setMyLoansCards(response.data);
                console.log(response.data);
            })
            .catch((error) => {
                console.error('Error fetching data: ', error);
            });
        }, [user.token]);
    
    return (
        <>
        <NavBar/>
        <div className="text-center">
            <h1>
                Loan Management Application  
            </h1>
            <h4>
                My Loans for {user.employeeId}
            </h4>
            <div class="container text-center d-flex gap-3 mt-5">
                {myLoanCards.map((myLoan, index) => (
                    <Card key={index} myLoan={myLoan} />
                    
                ))}
            </div>           
        </div>
        </>
    );
}

export default MyLoansPage;