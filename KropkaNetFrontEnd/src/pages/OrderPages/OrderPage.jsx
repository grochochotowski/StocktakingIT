import React, { useState, useEffect, useContext } from 'react'
import { Link } from 'react-router-dom'
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';

import NavBar from '../../components/NavBar'
import OrderNew from './OrderNew'
import OrderEdit from './OrderEdit'
import OrderInfo from './OrderInfo'

import '../../styles/mainSubPage.css'
import '../../styles/form.css'
import '../../styles/list.css'
import '../../styles/new.css'
import '../../styles/info.css'

function OrderPage() {

    const { state, setState } = useContext(GlobalStateContext);

    const [sorting, setSorting] = useState(["id", 0])
    const [filters, setFilters] = useState({ "filters" : "" })
    const [selected, setSelected] = useState(0);
    const [page, setPage] = useState(1);
    const [result, setResult] = useState({})
    const [box, setBox] = useState("");
    const [checked, setChecked] = useState({
        "none": true,
        "reject": true,
        "accept": true
    })

    async function fetchData() {
        const token = await refreshToken();
        let apiCall = `kropkaNet/order/all?` +
            `${filters.filters && "filters=" + filters.filters + "&"}` +
            `sortBy=${sorting[0]}&` +
            `sortDireciton=${sorting[1] == 0 ? "ASC" : "DESC"}&` +
            `page=${page}`
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setResult(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    useEffect(() => {
      fetchData();
    }, [sorting, page])
    

    useEffect(() => {
        function handleClickOutside(event) {
          if (event.target.closest(".outside-box") && !event.target.closest(".content")) setBox("");
        }
    
        document.addEventListener("click", handleClickOutside);
    
        return () => {
          document.removeEventListener("click", handleClickOutside);
        };
    }, []);

    useEffect(() => {
        function handleClickOutside(event) {
          if (event.target.closest(".outside-box") && !event.target.closest(".content")) setBox("");
        }
    
        document.addEventListener("click", handleClickOutside);
    
        return () => {
          document.removeEventListener("click", handleClickOutside);
        };
    }, []);

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
        fetchData();
    }
    function updateCheckBoxes(checkBox) {
        setChecked(prev => ({
            ...prev,
            [checkBox] : !prev[checkBox]
        }))
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
                    <th className="wide" onClick={() => sortTable("state")}>
                        {
                            sorting[0] == "state" &&
                            (
                                sorting[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        State
                    </th>
                </tr>
            </thead>
        );
    }
    function generateBody() {
        return (
            <tbody>
                {result.items && result.items.map((order) => (
                    <tr className={order.id === selected ? "selected" : ""} key={order.id} id={order.id} onClick={() => setSelected(order.id)}>
                        <td>{order.id}</td>
                        <td>{order.departmentName}</td>
                        <td>{order.dateOfOrderExecution}</td>
                        {
							(() => {
								if (order.state == -1) {
									return <td className='warning'>Rejected</td>;
								}
								else if (order.state == 0) {
									return <td>No decision</td>;
								} 
								else if (order.state == 1) {
									return <td className='success'>Accepted</td>;
								} 
								else {
									return <td className='warning'>State error</td>;
								}
							})()
                        }
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
                    <div className="filter w-check">
                        <input
                            type="text"
                            id="filters"
                            onChange={() => updateFilters("filters")}
                            value={filters.filters}
                        />
                        <button onClick={() => filter()}>Filter</button>
                        <div className="checkBoxex">
                            <div className="input-container">
                                <label htmlFor="none">No decision</label>
                                <input type="checkbox" name="none" id="none" checked={checked.none} onClick={() => updateCheckBoxes("none")}/>
                            </div>
                            <div className="input-container">
                                <label htmlFor="accept">Accepted</label>
                                <input type="checkbox" name="accept" id="accept" checked={checked.accept} onClick={() => updateCheckBoxes("accept")}/>
                            </div>
                            <div className="input-container">
                                <label htmlFor="reject">Rejected</label>
                                <input type="checkbox" name="reject" id="reject" checked={checked.reject} onClick={() => updateCheckBoxes("reject")}/>
                            </div>
                        </div>
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
                    <div onClick={() => selected != 0 && setBox("info")} className={selected ? "button objectOption" : "disable button objectOption"}>
                        <i className="fa-solid fa-info"></i>
                        <p>Details</p>
                    </div>
                    <div onClick={() => selected != 0 && setBox("edit")} className={selected ? "button objectOption" : "disable button objectOption"}>
                        <i className="fa-solid fa-pen-to-square"></i>
                        <p>Edit</p>
                    </div>
                    <div onClick={() => setBox("new")} className="button objectOption">
                        <i className="fa-solid fa-plus"></i>
                        <p>New</p>
                    </div>
                </div>
            </div>
            { box && box == "new" && <OrderNew hideBox={() => setBox("")} updateData={() => fetchData()}/> }
            { box && box == "edit" && <OrderEdit hideBox={() => setBox("")} updateData={() => fetchData()} selected={selected}/> }
            { box && box == "info" && <OrderInfo updateData={() => fetchData()} selected={selected}/> }
        </>
    )
}

export default OrderPage