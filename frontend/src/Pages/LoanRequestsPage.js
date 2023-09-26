import React, {useContext, useEffect, useState} from "react";
import { NavBar } from "../Component/LAMANav";
import axios from 'axios';
import { Card, Form, Row, Col, Container, Button } from 'react-bootstrap';

const LoanRequestsPage = () => {

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;
    const url ='https://localhost:7189/api/EmployeeRequest/requests';
    const [ requests, setRequests ] = useState([]); 
    const [ date, setDate ] = useState(new Date().toISOString())
    const [ status, setStatus ] = useState("Pending Approval");

    useEffect(() => {
        axios
            .get(url, {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                setRequests(response.data);
                console.log(response.data);
            }).catch((error) => {
                alert('Error fetching data ', error);
            });
    }, []);


    const handleStatusChange = (status) => {
      setStatus(status);
    }

    const handleSubmit = (requestId) => {
      if(status == null || date == null){
        alert('Please enter valid details!')
      }else{
        var payload = {
          'requestStatus' : status,
          'returnDate': date,
          'requestId':requestId
        }
        console.log(payload);
        axios
            .put(`https://localhost:7189/api/EmployeeRequest/${requestId}`, payload, {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                console.log(response.data);
                alert('Successfully changed request status!');
            }).catch((error) => {
                alert('Error editing details! ', error);
            });
      }
    }

    return (
        <div>
        <NavBar/>
        <div className="d-flex">
        {requests.filter((item)=>item.requestStatus == 'Pending Approval').map((request) => (
            <Container  key={request.requestId}>
            
                  <Card className="m-3 text-center shadow">
                    <Card.Body>
                      <Card.Header className="mb-3">Request ID: {request.requestId}</Card.Header>
                      <Card.Title>{request.itemDescription}</Card.Title>
                      
                        <Card.Text><strong>Make: </strong>{request.itemMake}</Card.Text>
                        <Card.Text><strong>Category: </strong>{request.itemCategory}</Card.Text>
                        <Card.Text><strong>Value: </strong>₹ {request.itemValuation}</Card.Text>
                        <Card.Text><strong>Request Date: </strong>{request.requestDate.substring(0,10)}</Card.Text>
                        <Card.Text><strong>Repayment Tenure: </strong>{request.durationInYears} Years</Card.Text>
                      
                        
                        <Form>
                        <Form.Group className="d-inline-flex align-items-center">
                          <Form.Label><strong>Status: </strong></Form.Label>
                          <Form.Control className="mx-2"
                            as="select"
                            value={status}
                            onChange={(e) => handleStatusChange(e.target.value)}
                          >
                            <option value="Pending Approval">Pending Approval</option>
                            <option value="Approved">Approved</option>
                            <option value="Rejected">Rejected</option>
                          </Form.Control>                       
                        </Form.Group>
                        <Button className="mt-3 w-75" variant="dark" size="" onClick={() => handleSubmit(request.requestId)}>Submit</Button>
                        </Form>
                        
                        
                    </Card.Body>
                  </Card>
                
          </Container>
        ))}
        </div>
        
        </div>
    )
}

export default LoanRequestsPage;