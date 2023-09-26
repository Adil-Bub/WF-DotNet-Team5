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

    const handleRequestDateChange = (date) => {
      setDate(date);
    }

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
        {requests.filter((item)=>item.requestStatus == 'Pending Approval').map((request) => (
            <Container key={request.requestId}>
            
                  <Card className="m-3 p-2">
                    <Card.Body>
                      <Card.Header className="mb-3">Request ID: {request.requestId}</Card.Header>
                      <Card.Title>{request.itemDescription}</Card.Title>
                      <div className="row mt-3">
                        <Card.Text className="col">Make: {request.itemMake}</Card.Text>
                        <Card.Text className="col">Category: {request.itemCategory}</Card.Text>
                        <Card.Text className="col">Value: ₹{request.itemValuation}</Card.Text>
                      </div>
                      <div className="row">
                        <Card.Text className="col">Request Date: {request.requestDate.substring(0,10)}</Card.Text>
                        <Card.Text className="col">Repayment Tenure: {request.durationInYears} Years</Card.Text>
                        <Card.Text className="col">Return Date: {request.returnDate}</Card.Text>
                      </div>
                      
                      <Form>
                      <Form.Group>
                        <Form.Label>Status:</Form.Label>
                        <Form.Control
                          as="select"
                          value={status}
                          onChange={(e) => handleStatusChange(e.target.value)}
                        >
                          <option value="Pending Approval">Pending Approval</option>
                          <option value="Approved">Approved</option>
                          <option value="Rejected">Rejected</option>
                        </Form.Control>                       
                      </Form.Group>
                      <Button className="my-2" variant="dark" size="sm" onClick={() => handleSubmit(request.requestId)}>Submit</Button>
                      </Form>
                    </Card.Body>
                  </Card>
                
          </Container>
        ))}
        </div>
    )
}

export default LoanRequestsPage;