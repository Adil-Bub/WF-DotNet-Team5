import React from 'react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

export function NavBar(){
    return (
        <>
        <Navbar bg="dark" data-bs-theme="dark">
        <Container>
          <Navbar.Brand href='/dashboard/admin/'>LAMA</Navbar.Brand>
          <Nav className="me-auto">
            <Nav.Link href='/dashboard/admin/employee-data'>Employees</Nav.Link>
            <Nav.Link href='/dashboard/admin/loan-card'>Loans</Nav.Link>
            <Nav.Link href='/dashboard/admin/all-items'>Items</Nav.Link>
            <Nav.Link href='/dashboard/admin/loan-requests'>Requests</Nav.Link>
          </Nav>
        </Container>
      </Navbar>
        </>
    )
}