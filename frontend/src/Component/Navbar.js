
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
export function NavBar(){
    return (
        <>
        <Navbar bg="dark" data-bs-theme="dark">
        <Container>
          <Navbar.Brand href='/dashboard/user/'>LUMA</Navbar.Brand>
          <Nav className="me-auto">
            <Nav.Link href='/dashboard/user/loans'>View</Nav.Link>
            <Nav.Link href='/dashboard/user/loans/apply'>Apply</Nav.Link>
            <Nav.Link href='/dashboard/user/items=purchased'>Orders</Nav.Link>
          </Nav>
        </Container>
      </Navbar>
        </>
    )
}