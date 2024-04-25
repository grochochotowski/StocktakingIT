/* eslint-disable react-refresh/only-export-components */
import React, { Suspense, lazy } from 'react'
import ReactDOM from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'


const WelcomePage = lazy(() => import('./pages/BeginPages/WelcomePage'));
const LoginPage = lazy(() => import('./pages/BeginPages/LoginPage'));
const RegisterPage = lazy(() => import('./pages/BeginPages/RegisterPage'));

import NotFoundPage from './pages/ErrorPages/NotFoundPage'
import Fallback from './pages/ErrorPages/Fallback'


import './styles/index.css'


const router = createBrowserRouter([
    {
        path: '/',
        element: <WelcomePage />,
        errorElement: <NotFoundPage />
    },
    {
        path: '/login',
        element: <LoginPage  />
    },
    {
        path: '/register',
        element: <RegisterPage  />
    }
])

ReactDOM.createRoot(document.getElementById('root')).render(
    <Suspense fallback={Fallback}>
        <React.StrictMode>
            <RouterProvider router={router}/>
        </React.StrictMode>
    </Suspense>
)
