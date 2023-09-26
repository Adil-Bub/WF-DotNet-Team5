import React from 'react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { useNavigate } from 'react-router-dom';
import { showInfoToast } from '../Util/toast';

export function NavBar(){
  const navigate = useNavigate();
  const handleLogout = () => {
    localStorage.removeItem('user');
    showInfoToast("Logged out!");
    navigate('/');
  }
    return (
        <>
        <Navbar bg="dark" data-bs-theme="dark">
        <Container>
          <Navbar.Brand href='/dashboard/user/'>LUMA</Navbar.Brand>
          <Nav className="me-auto">
            <Nav.Link href='/dashboard/user/loans'>View</Nav.Link>
            <Nav.Link href='/dashboard/user/items-purchased'>Orders</Nav.Link>
            <Nav.Link href='/dashboard/user/my-loans'>My Loans</Nav.Link>
          </Nav>
          <button className="btn btn-outline-light" onClick={handleLogout}>Logout</button>
        </Container>
      </Navbar>
        </>
    )
}