import React, { useState } from "react";
import { useNavigate } from 'react-router-dom';
import { NavBar } from "../../Component/LUMANav";
import Card from 'react-bootstrap/Card';
import { BsFillFileEarmarkCheckFill } from 'react-icons/bs';
import { BsFillEyeFill } from 'react-icons/bs';
import { ImProfile } from 'react-icons/im';

const UserDashboard = () => {

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;
    const navigate = useNavigate();

    const navigateToViewLoans = () => {
        navigate('/dashboard/user/loans');
    }

    const navigateToMyLoan = () => {
        navigate('/dashboard/user/my-loans');
    }

    const navigateToItemsPurchased = () => {
        navigate('/dashboard/user/items-purchased');
    }
    return (
        <>
            <NavBar />
            <div className="text-center">
                <h2 style={{ marginTop: '20px' }}>
                    User Dashboard
                </h2>
                <h4>
                    Welcome <code>{user.employeeName} ({user.employeeId})</code>
                </h4>

                <div className="container p-5 text-center fs-5">
                    <div className="d-inline-flex flex-wrap gap-4">

                        <Card style={{ width: '15rem', fontSize: '18px' }}
                            border="dark"
                            role="button"
                            className="custom-card"
                            onClick={navigateToViewLoans}>
                            <Card.Body className="d-flex flex-column align-items-center">
                                <BsFillEyeFill size={100} color="darkcyan" />
                                <Card.Text className="m-2">
                                    View Loans
                                </Card.Text>
                            </Card.Body>
                        </Card>

                        <Card style={{ width: '15rem', fontSize: '18px' }}
                            border="dark"
                            role="button"
                            className="custom-card"
                            onClick={navigateToItemsPurchased}>
                            <Card.Body className="d-flex flex-column align-items-center">
                                <BsFillFileEarmarkCheckFill size={100} color="darkcyan" />
                                <Card.Text className="m-2">
                                    Orders
                                </Card.Text>
                            </Card.Body>
                        </Card>
                        <Card style={{ width: '15rem', fontSize: '18px' }}
                            border="dark"
                            role="button"
                            className="custom-card"
                            onClick={navigateToMyLoan}>
                            <Card.Body className="d-flex flex-column align-items-center">
                                <ImProfile size={100} color="darkcyan" />
                                <Card.Text className="m-2">
                                    My Loans
                                </Card.Text>
                            </Card.Body>
                        </Card>
                    </div>
                </div>
            </div>
        </>
    );
}

export default UserDashboard;
