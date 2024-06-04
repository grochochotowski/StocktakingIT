/* eslint-disable react-refresh/only-export-components */
import React, { Suspense, lazy } from 'react'
import ReactDOM from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'


const WelcomePage = lazy(() => import('./pages/BeginPages/WelcomePage'))
const DashboardPage = lazy(() => import('./pages/BeginPages/DashboardPage'))
const LoginPage = lazy(() => import('./pages/BeginPages/LoginPage'))
const RegisterPage = lazy(() => import('./pages/BeginPages/RegisterPage'))

const OrderPage = lazy(() => import('./pages/OrderPages/OrderPage'))

const StocktakingDetails = lazy(() => import('./pages/StocktakingPages/StocktakingDetails'))

const User = lazy(() => import('./pages/UserPages/User'))

const Unauthorized = lazy(() => import('./pages/ErrorPages/Unauthorized'))
const Forbidden = lazy(() => import('./pages/ErrorPages/Forbidden'))
const NotFoundPage = lazy(() => import('./pages/ErrorPages/NotFoundPage'))
const Fallback = lazy(() => import('./pages/ErrorPages/Fallback'))

import './styles/index.css'


const router = createBrowserRouter([
    {
        path: '/',
        element: <WelcomePage />,
        errorElement: <NotFoundPage />,
    },
    {
        path: '/login',
        element: <LoginPage  />,
    },
    {
        path: '/register',
        element: <RegisterPage  />,
    },  
    {
        path: '/dashboard',
        element: <DashboardPage />,
    },


    {
        path: '/orders',
        element: <OrderPage />,
    },
    {
        path: '/orders/:id/stocktaking',
        element: <StocktakingDetails />
    },


    {
        path: '/account/:id',
        element: <User />
    },


    {
        path: '/401',
        element: <Unauthorized  />,
    },
    {
        path: '/403',
        element: <Forbidden  />,
    },
    {
        path: '/404',
        element: <NotFoundPage  />,
    },
    {
        path: '/fallback',
        element: <Fallback  />,
    },
])

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <Suspense fallback={<Fallback />}>
            <RouterProvider router={router}/>
        </Suspense>
    </React.StrictMode>
)
