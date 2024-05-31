import React, { useState } from 'react'
import { Link } from 'react-router-dom'

import NavBar from '../../components/NavBar'

import '../../styles/mainSubPage.css'
import '../../styles/form.css'
import '../../styles/list.css'

function OrderPage() {

    const [sorting, setSorting] = useState(["id", 0])
    const [filters, setFilters] = useState({ "filters" : "" })
    const [selected, setSelected] = useState(0);
    const [page, setPage] = useState(1);
    const [result, setResult] = useState({
        items: [
            {
            "id" : 1,
            "dateOfOrderExecution" : "01/01/0001",
            "departmentName" : "Department 1"
            },
            {
            "id" : 2,
            "dateOfOrderExecution" : "02/02/0002",
            "departmentName" : "Department 2"
            },
            {
            "id" : 3,
            "dateOfOrderExecution" : "03/03/0003",
            "departmentName" : "Department 3"
            },
            {
            "id" : 4,
            "dateOfOrderExecution" : "04/04/0004",
            "departmentName" : "Department 4"
            },
            {
            "id" : 5,
            "dateOfOrderExecution" : "05/05/0005",
            "departmentName" : "Department 5"
            },
            {
            "id" : 6,
            "dateOfOrderExecution" : "06/06/0006",
            "departmentName" : "Department 6"
            },
            {
            "id" : 7,
            "dateOfOrderExecution" : "07/07/0007",
            "departmentName" : "Department 7"
            },
            {
            "id" : 8,
            "dateOfOrderExecution" : "08/08/0008",
            "departmentName" : "Department 8"
            },
            {
            "id" : 9,
            "dateOfOrderExecution" : "09/09/0009",
            "departmentName" : "Department 9"
            },
            {
            "id" : 10,
            "dateOfOrderExecution" : "10/10/0010",
            "departmentName" : "Department 10"
            }
        ],
        totalPages: 3
    })

    function sortTable(column) {
        setSorting(prev => {
            if (prev[0] === column && prev[1] === 0) return [column, 1]
            return [column, 0]
        })
    }
    function updateFilters(filter) {
        setFilters(prev => ({
            [filter] : document.getElementById(filter).value
        }))
    }
    function filter() {
        if(token) {
            fetchData();
        }
    }

    function generateHeader() {
        return (
            <thead>
                <tr>
                    <th className="thin" onClick={() => sortTable("id")}>
                        {
                            sorting[0] == "id" &&
                            (
                                sorting[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Order
                    </th>
                    <th className="wide" onClick={() => sortTable("departmentName")}>
                        {
                            sorting[0] == "departmentName" &&
                            (
                                sorting[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Department Name
                    </th>
                    <th className="wide" onClick={() => sortTable("dateOfOrderExecution")}>
                        {
                            sorting[0] == "dateOfOrderExecution" &&
                            (
                                sorting[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Date of execution
                    </th>
                </tr>
            </thead>
        );
    }
    function generateBody() {
        return (
            <tbody>
                {result.items && result.items.map((order) => (
                    <tr className={order.id === selected && "selected"} key={order.id} id={order.id} onClick={() => setSelected(order.id)}>
                        <td>{order.id}</td>
                        <td>{order.departmentName}</td>
                        <td>{order.dateOfOrderExecution}</td>
                    </tr>
                ))}
            </tbody>
        )
    }
    function generatePagination() {

        const paginationItems = [];

        if (result.length != 0) {

            // Generate left arrow
            if (page > 1) {
                paginationItems.push(
                    <li className="clickable" onClick={() => setPage(page - 1)} key={"arrow-left"}>
                        <i className="fa-solid fa-caret-left"></i>
                    </li>
                )
            }
            else {
                paginationItems.push(
                    <li className="disable" key={"arrow-left"}>
                        <i className="fa-solid fa-caret-left"></i>
                    </li>
                )
            }

            if (result.totalPages <= 7) {
                for (let i = 1; i <= result.totalPages; i++) {
                    if (i == page) {
                    paginationItems.push(<li key={i} className="selected" onClick={() => setPage(i)}>{i}</li>);
                    }
                    else {
                        paginationItems.push(<li key={i} className="clickable" onClick={() => setPage(i)}>{i}</li>);
                    }
                }
            }
            else {

                if (page <= 4) {
                    for (let i = 1; i <= 7; i++) {
                        if (i == page) {
                            paginationItems.push(<li key={i} className="selected" onClick={() => setPage(i)}>{i}</li>);
                        }
                        else {
                            paginationItems.push(<li key={i} className="clickable" onClick={() => setPage(i)}>{i}</li>);
                        }
                    }
                    paginationItems.push(<li key={"dots2"}>...</li>)
                    paginationItems.push(<li key={result.totalPages} className="clickable" onClick={() => setPage(result.totalPages)}>{result.totalPages}</li>);
                }
                else if (result.totalPages - page < 5) {
                    paginationItems.push(<li key={1} className="clickable" onClick={() => setPage(1)}>{1}</li>);
                    paginationItems.push(<li key={"dots1"}>...</li>)
                    for (let i = result.totalPages-6; i <= result.totalPages; i++) {
                        if (i == page) {
                            paginationItems.push(<li key={i} className="selected" onClick={() => setPage(i)}>{i}</li>);
                        }
                        else {
                            paginationItems.push(<li key={i} className="clickable" onClick={() => setPage(i)}>{i}</li>);
                        }
                    }
                }
                else {
                    paginationItems.push(<li key={1} className="clickable" onClick={() => setPage(1)}>{1}</li>);
                    paginationItems.push(<li key={"dots1"}>...</li>)

                    for (let i = page-2; i < page; i++) {
                        paginationItems.push(<li key={i} className="clickable" onClick={() => setPage(i)}>{i}</li>);
                    }

                    paginationItems.push(<li key={page} className="selected" onClick={() => setPage(page)}>{page}</li>)

                    for (let i = page+1; i <= page+2; i++) {
                        paginationItems.push(<li key={i} className="clickable" onClick={() => setPage(i)}>{i}</li>);
                    }
                
                    paginationItems.push(<li key={"dots2"}>...</li>)
                    paginationItems.push(<li key={result.totalPages} className="clickable" onClick={() => setPage(result.totalPages)}>{result.totalPages}</li>);
                }
            }
                
            // Generate right arrow
            if (page < result.totalPages) {
                paginationItems.push(
                    <li className="clickable" onClick={() => setPage(page + 1)} key={"arrow-right"}>
                        <i className="fa-solid fa-caret-right"></i>
                    </li>
                )
            }
            else {
                paginationItems.push(
                    <li className="disable" key={"arrow-right"}>
                        <i className="fa-solid fa-caret-right"></i>
                    </li>
                )
            }
            return paginationItems;
        }
    }


    return (
        <>
            <NavBar />
            <div className="container">
                <div className="list">
                    <div className="filter">
                        <input
                            type="text"
                            id="filters"
                            onChange={() => updateFilters("filters")}
                            value={filters.filters}
                        />
                        <button onClick={() => filter()}>Filter</button>
                    </div>
                    <table>
                        { generateHeader() }
                        { generateBody() }
                    </table>
                    <ul>
                        { generatePagination() }
                    </ul>
                </div>
                <div className="list-menu">
                    <Link to="/orders" className="current button">
                        <i className="fa-solid fa-list"></i>
                        <p>List</p>
                    </Link>
                    <Link to={selected && `/orders/details/${selected}`} className={selected ? "button" : "disable button"}>
                        <i className="fa-solid fa-info"></i>
                        <p>Details</p>
                    </Link>
                    <Link to={selected && `/orders/edit/${selected}`} className={selected ? "button" : "disable button"}>
                        <i className="fa-solid fa-pen-to-square"></i>
                        <p>Edit</p>
                    </Link>
                    <Link to={`/orders/new/`} className="button">
                        <i className="fa-solid fa-plus"></i>
                        <p>New</p>
                    </Link>
                </div>
            </div>
        </>
    )
}

export default OrderPage