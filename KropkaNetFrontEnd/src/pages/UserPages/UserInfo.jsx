import React, { useState, useContext, useEffect } from 'react';
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';

function UserInfo({selected}) {

    const { state, setState } = useContext(GlobalStateContext);

    const [user, setUser] = useState({})

    async function fetchData() {
        const token = await refreshToken();
        getUser(token)
    }

    async function getUser(token) {
        let apiCall = `kropkaNet/user/${selected}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setUser(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    useEffect(() => {
        fetchData();
    }, [])

    return (
        <div className="outside-box">
			<div className="content">
				<h1>User {user.id}</h1>
				<div className="info-box">
					<div className="info-line">
						<h4>Name:</h4>
						<p>{user.name} {user.surname}</p>
					</div>
					<div className="info-line">
						<h4>Email:</h4>
						<p>{user.email}</p>
					</div>
					<div className="info-line">
						<h4>Phone number:</h4>
						<p>{user.phoneNumber}</p>
					</div>
					<div className="info-line">
						<h4>Notes:</h4>
                        <p>{user.note}</p>
					</div>
				</div>
			</div>
		</div>
    )
}

export default UserInfo