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
          <Navbar.Brand href='/dashboard/user/'>LUMA</Navbar.Brand>
          <Nav className="me-auto">
            <NavLink to='/dashboard/user/loans'>View</NavLink>
            <Nav.Link href='/dashboard/user/loans/apply'>Apply</Nav.Link>
            <Nav.Link href='/dashboard/user/items=purchased'>Orders</Nav.Link>
          </Nav>
        </Container>
      </Navbar>
        </>
    )
}