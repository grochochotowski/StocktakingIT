import { useState, useEffect } from 'react';
import instance from "../../api/axios"

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
        try {
            const response = await instance().get(apiCall);
            setResult(response.data);
        } catch (error) {
            setMessageBoxOpt(
                {
                    "active": true,
                    "header" : error.code,
                    "message" : error.message,
                    "type" : "error"
                }
            )
            console.error('Error fetching data:', error);
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