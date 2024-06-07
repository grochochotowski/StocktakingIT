import React, { useState, useContext, useEffect } from 'react'
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';

function OrderNew({ updateData, selected }) {

    const { state, setState } = useContext(GlobalStateContext);

	const [order, setOrder] = useState({})
	const [users, setUsers] = useState([])
	const [notInUsers, setNotInUsers] = useState([])
	
	const [newUser, setNewUser] = useState("")
	const [orderDate, setOrderDate] = useState({});

    const [sortingUser, setSortingUser] = useState(["id", 0])

    async function fetchData() {
        const token = await refreshToken();
        getOrder(token)
        getUsers(token)
        getNotInUsers(token)
    }

    async function getOrder(token) {
        let apiCall = `kropkaNet/order/${selected}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setOrder(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }
    async function getUsers(token) {
        let apiCall = `kropkaNet/user/getFromOrder/${selected}?` +
            `sortBy=${sortingUser[0]}&` +
            `sortDirection=${sortingUser[1] == 0 ? "ASC" : "DESC"}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setUsers(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }
    async function getNotInUsers(token) {
        let apiCall = `kropkaNet/user/getAll/notInOrder?orderId=${selected}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setNotInUsers(response.data);
			setNewUser(response.data[0].id)
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

	useEffect(() => {
		fetchData();
    }, [sortingUser])
	useEffect(() => {
        if (order.dateOfOrderExecution) {
            setOrderDate({"dateOfOrderExecution": order.dateOfOrderExecution})
        }
    }, [order]);

	const handleDateChange = (e) => setOrderDate({"dateOfOrderExecution": e.target.value});
	const handleUserChange = (e) => setNewUser(e.target.value);

	async function handleSubmit(e) {
		e.preventDefault();
        const token = await refreshToken();
		let apiCall = `kropkaNet/order/update/${selected}`;
        try {
            const response = await axiosInstance.put(apiCall, orderDate,{
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setOrder(response.data);
			updateData();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
	};
	async function handleDelete(e) {
		e.preventDefault();
        const token = await refreshToken();
		let apiCall = `kropkaNet/order/delete/${selected}`;
        try {
            const response = await axiosInstance.delete(apiCall, orderDate,{
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
			updateData();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
	};
	async function addUser(e) {
		e.preventDefault();
		
		const token = await refreshToken();
        const apiCall = `kropkaNet/order/addUser?userId=${newUser}&orderId=${selected}`;
        try {
            const response = await axiosInstance.patch(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

			fetchData();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
	};
	async function handleDeleteUser(element) {
		const token = await refreshToken();
        const apiCall = `kropkaNet/order/removeUser/?userId=${element}&orderId=${selected}`;
        try {
            const response = await axiosInstance.patch(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

			fetchData();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
	}


	function sortTableUser(column) {
        setSortingUser(prev => {
            if (prev[0] === column && prev[1] === 0) return [column, 1]
            return [column, 0]
        })
    }
	function generateHeaderUser() {
        return (
            <thead>
                <tr>
                    <th className="u-thin" onClick={() => sortTableUser("id")}>
                        {
                            sortingUser[0] == "id" &&
                            (
                                sortingUser[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        ID
                    </th>
                    <th className="wide" onClick={() => sortTableUser("name")}>
                        {
                            sortingUser[0] == "name" &&
                            (
                                sortingUser[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Name
                    </th>
                    <th className="wide" onClick={() => sortTableUser("surname")}>
                        {
                            sortingUser[0] == "surname" &&
                            (
                                sortingUser[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Surname
                    </th>
                    <th className="u-thin"></th>
                </tr>
            </thead>
        );
    }
    function generateBodyUser() {
        return (
            <tbody>
                {users.map((user) => (
                    <tr className="not-clickable" key={user.id} id={user.id}>
                        <td className="u-thin">{user.id}</td>
                        <td>{user.name}</td>
                        <td>{user.surname}</td>
						<td className="u-thin delete-item" onClick={() => handleDeleteUser(user.id)}>
							<i className="fa-solid fa-trash-can"></i>
						</td>
                    </tr>
                ))}
            </tbody>
        )
    }

	return (
		<div className="outside-box">
			<div className="content scroll">
				<div className="top">
					<h1>Update order</h1>
					<form>
						<input 
							type="datetime-local" 
							id="order-date" 
							name="order-date" 
							value={orderDate.dateOfOrderExecution} 
							onChange={handleDateChange}
						/>
						<button type="submit" onClick={handleSubmit}>Update</button>
						{
							!order.stocktakingId &&
							<button className="warning" onClick={handleDelete}>Delete</button>
						}
					</form>
				</div>
				<div className="bottom single">
					<h3>Users</h3>
					<div className="list rows-5">
						<table>
							{ generateHeaderUser() }
							{ generateBodyUser() }
						</table>
					</div>
					<div className="new-object">
						<select name="user" id="user" onChange={handleUserChange}>
							{notInUsers.map(user => (
								<option key={user.id} value={user.id}>{user.name} {user.surname}</option>
							))}
						</select>
						<button type="submit" onClick={addUser}>Add</button>
					</div>
				</div>
			</div>
		</div>
	)
}

export default OrderNew