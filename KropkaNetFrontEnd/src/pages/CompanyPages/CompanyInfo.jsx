import React, { useState, useEffect } from 'react'
import { axiosInstance, refreshToken } from '../../api/axios';

import '../../styles/info.css'
import '../../styles/list.css'

function CompanyInfo({selected}) {

	const [company, setCompany] = useState({})
	const [departments, setDepartments] = useState([])
	const [users, setUsers] = useState([])

    const [sortingUser, setSortingUser] = useState(["id", 0])
	const [sortingDepartment, setSortingDepartment] = useState(["id", 0])

    async function fetchData() {
        const token = await refreshToken();
        getCompany(token)
        getDepartments(token)
        getUsers(token)
    }

    async function getCompany(token) {
        let apiCall = `kropkaNet/company/${selected}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setCompany(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }
    async function getDepartments(token) {
        let apiCall = `kropkaNet/department/company/${selected}?` +
        `sortBy=${sortingDepartment[0]}&` +
        `sortDirection=${sortingDepartment[1] == 0 ? "ASC" : "DESC"}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setDepartments(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }
    async function getUsers(token) {
        let apiCall = `kropkaNet/user/getFromCompany/${selected}?` +
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

    useEffect(() => {
      fetchData();
    }, [sortingUser, sortingDepartment])


	function sortTableUser(column) {
        setSortingUser(prev => {
            if (prev[0] === column && prev[1] === 0) return [column, 1]
            return [column, 0]
        })
    }
	function sortTableDepartment(column) {
        setSortingDepartment(prev => {
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
                </tr>
            </thead>
        );
    }
    function generateHeaderDepartment() {
        return (
            <thead>
                <tr>
                    <th className="u-thin" onClick={() => sortTableDepartment("id")}>
                        {
                            sortingDepartment[0] == "id" &&
                            (
                                sortingDepartment[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        ID
                    </th>
                    <th className="wide" onClick={() => sortTableDepartment("departmentName")}>
                        {
                            sortingDepartment[0] == "departmentName" &&
                            (
                                sortingDepartment[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Name
                    </th>
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
                    </tr>
                ))}
            </tbody>
        )
    }
    function generateBodyDepartment() {
        return (
            <tbody>
                {departments.map((department) => (
                    <tr className="not-clickable" key={department.id} id={department.id}>
                        <td className="u-thin">{department.id}</td>
                        <td>{department.departmentName}</td>
                    </tr>
                ))}
            </tbody>
        )
    }
	
	return (
		<div className="outside-box">
			<div className="content scroll">
				<h1>{company.id} - {company.companyName}</h1>
				<div className="info-box">
					<div className="info-line">
						<h4>NIP:</h4>
						<p>{company.nip}</p>
						<h4>KRS:</h4>
						<p>{company.krs}</p>
					</div>
					<div className="info-line">
						<h4>Notes:</h4>
						<p>{company.note}</p>
					</div>
					<div className="info-line">
						<h4>Address:</h4>
                        {
                            company.address &&
                                <p>{company.address.street} {company.address.building} {company.address.zipCode} {company.address.building}
                                {company.address.premises != null ? " / " + company.address.premises : ""}</p>
                        }
					</div>
				</div>
				<div className="info-box divide">
					<div className="half rows-5">
						<h3>Users</h3>
						<div className="list">
							<table>
								{ generateHeaderUser() }
								{ generateBodyUser() }
							</table>
						</div>
					</div>
					<div className="half rows-5">
						<h3>Departments</h3>
						<div className="list">
							<table>
								{ generateHeaderDepartment() }
								{ generateBodyDepartment() }
							</table>
						</div>
					</div>
				</div>
			</div>
		</div>
	)
}

export default CompanyInfo