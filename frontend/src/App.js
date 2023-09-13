import React from 'react';
import './App.css';
import { AppProvider } from './Context/App.context';
import Login from './Pages/LoginPage';
import ProfilePage from './Pages/ProfilePage';
import LoginwithToken from './Pages/LoginWithToken.js';
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
