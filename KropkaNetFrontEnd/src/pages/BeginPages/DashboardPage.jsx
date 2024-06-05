import { useState, useEffect } from 'react';
import { axiosInstance, refreshToken } from '../../api/axios';

import NavBarEmployee from '../../components/NavBarEmployee'
import MessageBox from '../../components/MessageBox'

function DashboardPage() {

    const [messageBoxOpt, setMessageBoxOpt] = useState({
        "active": false,
        "header" : "",
        "message" : "",
        "type" : ""
    })
    const [result, setResult] = useState([])

    async function fetchData() {
        let apiCall = `/kropkaNet/product/all?page=1`

        try {
            const token = await refreshToken();
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });
            console.log(response.data)
            setResult(response.data)
        } catch (error) {
            console.error('Error fetching data:', error);
            setMessageBoxOpt(
                {
                    "active": true,
                    "header" : error.response.status,
                    "message" : error.message,
                    "type" : "error"
                }
            )
        }
    }

    useEffect(() => {
        fetchData();
    }, []);

    return (
        <>
            <NavBarEmployee />
            <div>
                {result.items && result.items.map((item, key) => (
                    <p key={key}>{item.id} - {item.name} - {item.category}</p>
                ))}
            </div>
            { messageBoxOpt.active && <MessageBox header={messageBoxOpt.header} message={messageBoxOpt.message} type={messageBoxOpt.type}/> }
        </>
    )
}

export default DashboardPage