import React from 'react';
import './App.css';
import { AppProvider } from './Context/App.context';
import LoginwithToken from './Pages/LoginPage';
import RegistrationPage from './Pages/RegistrationPage';
import UserDashboard from "./Pages/LUMA/UserDashboard";
import AdminDashboard from './Pages/LAMA/AdminDashboard';
import ItemsMasterDataPage from './Pages/LAMA/ItemsMasterDataPage';
import EmployeeDataPage from './Pages/LAMA/EmployeeDataPage';
import LoanCardPage from './Pages/LAMA/LoanCardPage';
import ViewLoansPage from './Pages/LUMA/ViewLoansPage';
import ApplyLoansPage from './Pages/LUMA/ApplyLoansPage';
import ViewItemsPurchasedPage from './Pages/LUMA/ItemsPurchasedPage';
import { createBrowserRouter } from 'react-router-dom';
import { RouterProvider } from 'react-router-dom';
import ProtectedRoute from './Component/ProtectedRoute.js';
import AdminRoute from './Component/AdminRoute';
import MyLoansPage from './Pages/LUMA/MyLoansPage';
import LoanRequestsPage from './Pages/LAMA/LoanRequestsPage';

import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css'

const router = createBrowserRouter([

  {
    path: "/",
    element: <LoginwithToken />,
  },
  {
    path: "/register",
    element: <RegistrationPage />,
  },
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
    path: '/dashboard/user/loans/apply/:category',
    element:<ProtectedRoute>
    <ApplyLoansPage />
  </ProtectedRoute>
  },
  {
    path: "/dashboard/user/items-purchased",
    element: <ProtectedRoute>
      <ViewItemsPurchasedPage />
    </ProtectedRoute>
  },
  {
    path: "/dashboard/user/my-loans",
    element: <ProtectedRoute>
      <MyLoansPage />
    </ProtectedRoute>
  },
  {
    path: "/dashboard/admin/loan-requests",
    element: <AdminRoute>
      <LoanRequestsPage />
    </AdminRoute>
  }
]);

function App() {
  return (
    <>
      <AppProvider>
        <RouterProvider router={router} />
        <ToastContainer/>
      </AppProvider>
    </>
  );
}

export default App;
