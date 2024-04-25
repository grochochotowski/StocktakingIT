import React from 'react'
import ReactDOM from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'


import WelcomePage  from './pages/BeginPages/WelcomePage'
import LoginPage  from './pages/BeginPages/LoginPage'
import RegisterPage from './pages/BeginPages/RegisterPage'

import NotFoundPage from './pages/ErrorPages/NotFoundPage'


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
    <React.StrictMode>
        <RouterProvider router={router}/>
    </React.StrictMode>,
)
