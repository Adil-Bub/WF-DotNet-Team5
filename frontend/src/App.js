import React from 'react';
import './App.css';
import { AppProvider } from './Context/App.context';
import ProfilePage from './Pages/ProfilePage';
import LoginwithToken from './Pages/LoginWithToken';
import RegistrationPage from './Pages/RegistrationPage';
import {createBrowserRouter} from 'react-router-dom';
import {RouterProvider} from 'react-router-dom';
import ProtectedRoute from './Component/ProtectedRoute.js';

const router = createBrowserRouter([
  {
    path:"/",
    element:<LoginwithToken/>,
  },
  {
    path:"/login",
    element:<LoginwithToken/>,
  },
  {
    path:"/register",
    element:<RegistrationPage/>,
  },
  {
    path:"/profile",
    element:<ProtectedRoute>
      <ProfilePage/>
    </ProtectedRoute>,
  }
]);
function App() {
  return (
    < AppProvider>
      <RouterProvider router={router}/>
    </AppProvider>

  );
}

export default App;
