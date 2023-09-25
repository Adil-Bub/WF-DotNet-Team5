import React from 'react';
import { Navigate } from 'react-router-dom';


const ProtectedRoute = ({ children })=> {

  const storedUser = localStorage.getItem('user');
  const user = storedUser ? JSON.parse(storedUser) : null;
    if(user == null || user.designation!=="admin")
    {
        return <Navigate to="/" replace />
  }
  return children
}
export default ProtectedRoute;