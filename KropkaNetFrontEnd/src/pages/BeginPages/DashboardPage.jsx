import { useState, useEffect } from 'react';
import instance from "../../api/axios"

import NavBar from '../../components/NavBar'

function DashboardPage() {

    const [restult, setResult] = useState([])

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
            <div>DashboardPage</div>
        </>
    )
}

export default DashboardPage