import React, { useContext, useState, useEffect} from "react";
import axios from 'axios';
import { AppContext } from "../../Context/App.context";
import { useNavigate, Link } from 'react-router-dom';
import { NavBar } from "../../Component/LUMANav";

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
    
    let cardClass = `card w-25 shadow mb-5 ${isHovered && 'hovered-card'}`;
    
    if (myLoan.requestStatus == "Approved"){
      cardClass += ' text-dark bg-light' //white
    } else if (myLoan.requestStatus == "Pending Approval"){
      cardClass += ' text-white bg-secondary' //grey
    } else if (myLoan.requestStatus == "Rejected") {
      cardClass += ' text-white bg-dark' //black
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
          <p><strong>Item ID:</strong> {myLoan.itemId}</p>
          <p><strong>Make:</strong> {myLoan.itemMake}</p>
          <p><strong>Category:</strong> {myLoan.itemCategory}</p>
          <p><strong>Valuation:</strong> ₹ {myLoan.itemValuation}</p>
          <p><strong>Request ID:</strong> {myLoan.requestId}</p>
          <p><strong>Request Date:</strong> {myLoan.requestDate.substring(0,10)}</p>
          <p><strong>Request Status:</strong> {myLoan.requestStatus}</p>
          {isHovered && approvalDetails && (
            <div className="approval-details">
              <code>Approved Details</code>
              <ul className="list-unstyled">
              <li><strong>Card ID:</strong> {approvalDetails.cardId}</li>
              <li><strong>Card Issue Date:</strong> {approvalDetails.cardIssueDate.substring(0,10).substring(0,10)}</li>
              </ul>
            </div>
          )}
          <div className="card-footer">
            { (myLoan.returnDate!=null) ? (
                <p><code><strong>Return Date:</strong> {myLoan.returnDate.substring(0,10)}</code> </p>
            ) : (
                <p>Return Date TBD</p>
            )}
            <p><strong>Loan ID:</strong> {myLoan.loanId}</p>
            <p><strong>Duration:</strong> {myLoan.durationInYears} Years</p>
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
        <div className="text-center p-2">
          {(myLoanCards.length == 0) ? (
            <h4>No loans availed yet. </h4>
          ) : (

          <>           
           <h4>
                All Loan Cards Requested by <code>{user.employeeName} ({user.employeeId})</code>
            </h4>
            <div class="container text-center d-flex gap-3 mt-3">
                {myLoanCards.map((myLoan, index) => (
                    <Card key={index} myLoan={myLoan} />
                    
                ))}
            </div>
            </> 
            )}          

        </div>
        </>
    );
}

export default MyLoansPage;