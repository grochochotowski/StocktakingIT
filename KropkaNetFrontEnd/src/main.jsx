/* eslint-disable react-refresh/only-export-components */
import React, { createContext, useContext, useState, Suspense, lazy } from 'react'
import ReactDOM from 'react-dom/client'
import { createBrowserRouter, RouterProvider, Navigate } from 'react-router-dom'
import { GlobalStateProvider, GlobalStateContext } from './GlobalState';


const WelcomePage = lazy(() => import('./pages/BeginPages/WelcomePage'))
const DashboardPage = lazy(() => import('./pages/BeginPages/DashboardPage'))
const LoginPage = lazy(() => import('./pages/BeginPages/LoginPage'))
const RegisterPage = lazy(() => import('./pages/BeginPages/RegisterPage'))

const OrderPage = lazy(() => import('./pages/OrderPages/OrderPage'))

const StocktakingDetails = lazy(() => import('./pages/StocktakingPages/StocktakingDetails'))

const CompanyPage = lazy(() => import('./pages/CompanyPages/CompanyPage'))

const UserPage = lazy(() => import('./pages/UserPages/UserPage'))

const Unauthorized = lazy(() => import('./pages/ErrorPages/Unauthorized'))
const Forbidden = lazy(() => import('./pages/ErrorPages/Forbidden'))
const NotFoundPage = lazy(() => import('./pages/ErrorPages/NotFoundPage'))
const Fallback = lazy(() => import('./pages/ErrorPages/Fallback'))

import './styles/index.css'

const PrivateRoute = ({ children }) => {
    const { state } = useContext(GlobalStateContext);
    return state.isLoggedIn ? children : <Navigate to="/login" />;
};

const router = createBrowserRouter([
    { path: '/', element: <WelcomePage />, errorElement: <NotFoundPage /> },

    { path: '/login', element: <LoginPage /> },
    { path: '/register', element: <RegisterPage /> },

    { path: '/dashboard',
    element: <PrivateRoute><DashboardPage /></PrivateRoute> },

    { path: '/orders',
    element: <PrivateRoute><OrderPage /></PrivateRoute> },
    { path: '/orders/:orderId/stocktaking/:stocktakingId/:warehouseId',
    element: <PrivateRoute><StocktakingDetails /></PrivateRoute> },

    { path: '/companies',
    element: <PrivateRoute><CompanyPage /></PrivateRoute> },

    { path: '/account/:id',
    element: <PrivateRoute><UserPage /></PrivateRoute> },

    { path: '/401', element: <Unauthorized /> },
    { path: '/403', element: <Forbidden /> },
    { path: '/404', element: <NotFoundPage /> },
    { path: '/fallback', element: <Fallback /> },
]);

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <GlobalStateProvider>
            <Suspense fallback={<Fallback />}>
                <RouterProvider router={router} />
            </Suspense>
        </GlobalStateProvider>
    </React.StrictMode>
)
