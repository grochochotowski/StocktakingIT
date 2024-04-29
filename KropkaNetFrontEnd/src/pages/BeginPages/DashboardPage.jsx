import { useState, useEffect } from 'react';
import instance from "../../api/axios"

import NavBar from '../../components/NavBar'

function DashboardPage() {

    const [result, setResult] = useState([])

    async function fetchData() {
        let apiCall = `kropkaNet/product/list`
        try {
            const response = await instance().get(apiCall);
            setResult(response.data);
        } catch (error) {
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
        </>
    )
}

export default DashboardPage