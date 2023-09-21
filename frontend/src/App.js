import React from 'react';
import './App.css';
import { AppProvider } from './Context/App.context';
import LoginwithToken from './Pages/LoginWithToken';
import RegistrationPage from './Pages/RegistrationPage';
import UserDashboard from "./Pages/UserDashboard";
import AdminDashboard from './Pages/AdminDashboard';
import HomePage from './Pages/HomePage';
import ItemsMasterDataPage from './Pages/ItemsMasterDataPage';
import EmployeeDataPage from './Pages/EmployeeDataPage';
import LoanCardPage from './Pages/LoanCardPage';
import ViewLoansPage from './Pages/ViewLoansPage';
import ApplyLoansPage from './Pages/ApplyLoansPage';
import ViewItemsPurchasedPage from './Pages/ViewItemsPurchasedPage';
import { createBrowserRouter } from 'react-router-dom';
import { RouterProvider } from 'react-router-dom';
import ProtectedRoute from './Component/ProtectedRoute.js';
import AdminRoute from './Component/AdminRoute';
const router = createBrowserRouter([
 
  {
    path:"/",
    element:<LoginwithToken/>,
  },
  {
    path: "/register",
    element: <RegistrationPage />,
  },
  // {
  //   path:"/profile",
  //   element:<ProtectedRoute>
  //     <ProfilePage/>
  //   </ProtectedRoute>,
  // }
  {
    path: "/dashboard/user",
    element: <ProtectedRoute>
      <UserDashboard />
    </ProtectedRoute>
  },
  {
    path: "/dashboard/admin",
    element: <AdminRoute>
      <AdminDashboard />
    </AdminRoute>
  },
  {
    path: "/dashboard/admin/employee-data",
    element:
      <AdminRoute>
        <EmployeeDataPage />
      </AdminRoute>
  },
  {
    path: "/dashboard/admin/loan-card",
    element: <AdminRoute>
      <LoanCardPage />
    </AdminRoute>
  },
  {
    path: "/dashboard/admin/all-items",
    element: <AdminRoute>
      <ItemsMasterDataPage />
    </AdminRoute>
  },
  {
    path: "/dashboard/user/loans",
    element: <ProtectedRoute>
      <ViewLoansPage />
    </ProtectedRoute>
  },
  {
    path: "/dashboard/user/loans/apply",
    element: <ProtectedRoute>
      <ApplyLoansPage />
    </ProtectedRoute>
  },
  {
    path: "/dashboard/user/items=purchased",
    element: <ProtectedRoute>
      <ViewItemsPurchasedPage />
    </ProtectedRoute>
  }

]);
function App() {
  return (
    <>
   
    < AppProvider>
      <RouterProvider router={router} />
    </AppProvider>

    </>
  );
}

export default App;
