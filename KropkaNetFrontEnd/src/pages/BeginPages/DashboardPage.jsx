import { useState, useEffect } from 'react';
import useAxiosPrivate from '../../hooks/useAxiosPrivate';
import useRefreshToken from '../../hooks/useRefreshToken';

import NavBar from '../../components/NavBar'
import MessageBox from '../../components/MessageBox'

function DashboardPage() {

    const refresh = useRefreshToken()
    const axiosPrivate = useAxiosPrivate()

    const [messageBoxOpt, setMessageBoxOpt] = useState({
        "active": false,
        "header" : "",
        "message" : "",
        "type" : ""
    })
    const [result, setResult] = useState([])

    useEffect(() => {
        let isMounted = true
        const controller = new AbortController()
        let apiCall = `kropkaNet/product/list`

        async function fetchData() {
            try {
                const response = await axiosPrivate.get(apiCall, {
                    signal: controller.signal,
                    withCredentials: true
                });
                console.log(response.data)
                isMounted && setResult(response.data);
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

        fetchData();

        return () => {
            isMounted = false;
            controller.abort();
        }
    }, []);

    return (
        <>
            <NavBar />
            <div>
                {result.map((item, key) => (
                    <p key={key}>{item.id} - {item.name} - {item.category}</p>
                ))}
                <button onClick={() => refresh()}>Refresh</button>
            </div>
            { messageBoxOpt.active && <MessageBox header={messageBoxOpt.header} message={messageBoxOpt.message} type={messageBoxOpt.type}/> }
        </>
    )
}

export default DashboardPage