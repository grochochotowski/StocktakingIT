import { useState, useEffect } from 'react';
import axios from '../../api/axios';

import NavBar from '../../components/NavBar'
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
        let apiCall = `kropkaNet/product/list`
        const jwtCookie = document.cookie
            .split(';')
            .map(cookie => cookie.trim())
            .find(cookie => cookie.startsWith('jwtToken='));

        let jwtToken = '';
        if (jwtCookie) {
            jwtToken = jwtCookie.split('=')[1];
        }

        try {
            const response = await axios.get(apiCall, {
                headers: {
                    'Authorization': `Bearer ${jwtToken}`
                }
            });
            
            console.log(response)
        } catch (error) {
            console.error('Error fetching data:', error);
            /*setMessageBoxOpt(
                {
                    "active": true,
                    "header" : error.response.status,
                    "message" : error.message,
                    "type" : "error"
                }
            )*/
        }
    }

    useEffect(() => {
        fetchData();
    }, []);

    return (
        <>
            <NavBar />
            <div>
                {result.map((item, key) => (
                    <p key={key}>{item.id} - {item.name} - {item.category}</p>
                ))}
            </div>
            { messageBoxOpt.active && <MessageBox header={messageBoxOpt.header} message={messageBoxOpt.message} type={messageBoxOpt.type}/> }
        </>
    )
}

export default DashboardPage