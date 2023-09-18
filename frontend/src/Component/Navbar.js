import React from 'react';
import { useContext } from 'react';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { AppContext } from "../Context/App.context";
import { useNavigate, Link, NavLink } from 'react-router-dom';

export function NavBar(){
  const { user, setUser } = useContext(AppContext);

    return (
        <>
        <Navbar bg="dark" data-bs-theme="dark">
        <Container>
          <Navbar.Brand href='/dashboard/user/'>LUMA</Navbar.Brand>
          <Nav className="me-auto">
            <NavLink to='/dashboard/user/loans'>View</NavLink>
            <NavLink to='/dashboard/user/loans/apply'>Apply</NavLink>
            <NavLink to='/dashboard/user/items=purchased'>Orders</NavLink>
          </Nav>
        </Container>
      </Navbar>
        </>
    )
}