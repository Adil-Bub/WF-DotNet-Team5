import React from 'react';
import { useContext } from 'react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavLink from 'react-bootstrap/esm/NavLink';
import { AppContext } from "../Context/App.context";
export function NavBar(){
  const { user, setUser } = useContext(AppContext);

    return (
        <>
        <Navbar bg="dark" data-bs-theme="dark">
        <Container>
          <Navbar.Brand href='/dashboard/admin/'>LAMA</Navbar.Brand>
          <Nav className="me-auto">
            <NavLink to='/dashboard/admin/employee-data'>Employees</NavLink>
            <Nav.Link href='/dashboard/admin/loan-card'>Loans</Nav.Link>
            <Nav.Link href='/dashboard/admin/all-items'>Items</Nav.Link>
          </Nav>
        </Container>
      </Navbar>
        </>
    )
}