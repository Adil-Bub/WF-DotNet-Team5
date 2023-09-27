import React from "react";
import { useNavigate } from 'react-router-dom';
import { NavBar } from "../../Component/LAMANav";
import Card from 'react-bootstrap/Card';
import { FaUsersBetweenLines } from 'react-icons/fa6';
import { LuShoppingBasket } from 'react-icons/lu';
import {FaMoneyCheckAlt} from 'react-icons/fa';

const AdminDashboard = () => {

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;

    const navigate = useNavigate();

    const navigateToEmployeeData = () => {
        navigate('/dashboard/admin/employee-data');
    }

    const navigateToLoanCardManagement = () => {
        navigate('/dashboard/admin/loan-card');
    }

    const navigateToItemsMasterData = () => {
        navigate('/dashboard/admin/all-items');
    }

    return (
        <>
            <NavBar />
            <div className="text-center">
                <h2 style={{ marginTop: '20px' }}>
                    Admin Dashboard
                </h2>
                <h4>
                    Welcome <code>{user.employeeName} ({user.employeeId})</code>
                </h4>

                <div className="container p-5 text-center fs-5">
                    <div className="d-inline-flex flex-wrap gap-4">

                        <Card style={{ width: '15rem', fontSize: '18px'}}
                            border="dark"
                            role="button"
                            className="custom-card"
                            onClick={navigateToEmployeeData}>
                            <Card.Body className="d-flex flex-column align-items-center">
                                <FaUsersBetweenLines size={100} color="darkcyan" />
                                <Card.Text className="m-2">
                                    Employee Data
                                </Card.Text>
                            </Card.Body>
                        </Card>

                        <Card style={{ width: '15rem', fontSize: '18px'}}
                            border="dark"
                            role="button"
                            className="custom-card"
                            onClick={navigateToLoanCardManagement}>
                            <Card.Body className="d-flex flex-column align-items-center">
                                <FaMoneyCheckAlt size={100} color="darkcyan" />
                                <Card.Text className="m-2">
                                    Loan Card Data
                                </Card.Text>
                            </Card.Body>
                        </Card>

                        <Card style={{ width: '15rem', fontSize: '18px'}}
                            border="dark"
                            role="button"
                            className="custom-card"
                            onClick={navigateToItemsMasterData}>
                            <Card.Body className="d-flex flex-column align-items-center">
                                <LuShoppingBasket size={100} color="darkcyan" />
                                <Card.Text className="m-2">
                                    Item Data
                                </Card.Text>
                            </Card.Body>
                        </Card>
                    </div>
                </div>
            </div>
        </>
    );
}

export default AdminDashboard;
