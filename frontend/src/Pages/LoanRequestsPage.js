import React, {useContext, useEffect, useState} from "react";
import { NavBar } from "../Component/LAMANav";
import axios from 'axios';
import { Card, Form, Row, Col, Container, Button } from 'react-bootstrap';

const LoanRequestsPage = () => {

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;
    const url ='https://localhost:7189/api/EmployeeRequest/requests';
    const [ requests, setRequests ] = useState([]); 
    const [ date, setDate ] = useState(new Date().toISOString().substr(0, 10))
    const [ status, setStatus ] = useState("Pending Approval");

    useEffect(() => {
        axios
            .get(url, {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                setRequests(response.data);
                //console.log(response.data);
            }).catch((error) => {
                alert('Error fetching data ', error);
            });
    });

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
            .put(`https://localhost:7189/api/EmployeeRequestDetails/${requestId}`, payload, {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                //console.log(response.data);
                alert('Successfully changed request status!');
            }).catch((error) => {
                alert('Error editing details! ', error);
            });
      }
    }

    return (
        <div>
        <NavBar/>
        {requests.map((request) => (
            <Container fluid>
            <Row>
                <Col xs={12} sm={10} md={8} lg={10} className="mx-auto" key={request.requestId}>
                  <Card className="m-3">
                    <Card.Body>
                      <Card.Title>Request ID: {request.requestId}</Card.Title>
                      <Card.Text>Item Value: {request.itemValuation}</Card.Text>
                      <Card.Text>Item: {request.itemDescription}</Card.Text>
                      <Card.Text>Item Make: {request.itemMake}</Card.Text>
                      <Card.Text>Item Category: {request.itemCategory}</Card.Text>
                      <Card.Text>Request Date: {request.requestDate}</Card.Text>
                      <Card.Text>Request Status: {request.requestStatus}</Card.Text>
                      <Card.Text>Repayment Tenure: {request.durationInYears}</Card.Text>
                      {request.returnDate == null ? <></> : 
                      <Card.Text>Return Date: {request.returnDate}</Card.Text>}
                      <Form>
                      <Form.Group>
                        <Form.Label>Status:</Form.Label>
                        <Form.Control
                          as="select"
                          value={request.requestStatus}
                          onChange={(e) => handleStatusChange(e.target.value)}
                        >
                          <option value="Pending Approval">Pending Approval</option>
                          <option value="Approved">Approved</option>
                          <option value="Rejected">Rejected</option>
                        </Form.Control>
                        {request.returnDate == null ? <>
                        <Form.Label>Return Date:</Form.Label>
                        <Form.Control
                          type="date"
                          defaultValue={date}
                          onBlur={(e) => handleRequestDateChange(e.target.value)}
                        />
                          </> : <></>}
                      </Form.Group>
                      <Button variant="dark" size="sm" onClick={() => handleSubmit(request.requestId)}>Submit</Button>
                      </Form>
                    </Card.Body>
                  </Card>
                </Col>
            </Row>
          </Container>
        ))}
        </div>
    )
}

export default LoanRequestsPage;