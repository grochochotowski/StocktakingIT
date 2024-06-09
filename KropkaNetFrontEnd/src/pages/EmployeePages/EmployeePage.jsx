import React, { useState, useEffect, useContext } from 'react'
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';
import { Link } from 'react-router-dom';

import NavBarEmployee from '../../components/NavBarEmployee'

import EmployeeNew from './EmployeeNew'
import EmployeeEdit from './EmployeeEdit'
import EmployeeInfo from './EmployeeInfo'

import '../../styles/mainSubPage.css'
import '../../styles/form.css'
import '../../styles/list.css'
import '../../styles/new.css'
import '../../styles/info.css'

function EmployeePage() {

    const { state, setState } = useContext(GlobalStateContext);

    const [newState, setNewState] = useState([0, 0])
    const [sorting, setSorting] = useState(["id", 0])
    const [filters, setFilters] = useState({ "filters" : "" })
    const [selected, setSelected] = useState(0);
    const [page, setPage] = useState(1);
    const [result, setResult] = useState({})
    const [box, setBox] = useState("");

    async function fetchData() {
        const token = await refreshToken();
        let apiCall = `kropkaNet/employee/getAll?` +
            `${filters.filters && "filter=" + filters.filters + "&"}` +
            `sortBy=${sorting[0]}&` +
            `sortDirection=${sorting[1] == 0 ? "ASC" : "DESC"}&` +
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

    function generateHeader() {
        return (
            <thead>
                <tr>
                    <th className="u-thin" onClick={() => sortTable("id")}>
                        {
                            sorting[0] == "id" &&
                            (
                                sorting[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        ID
                    </th>
                    <th className="wide" onClick={() => sortTable("name")}>
                        {
                            sorting[0] == "name" &&
                            (
                                sorting[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Name
                    </th>
                    <th className="wide" onClick={() => sortTable("surname")}>
                        {
                            sorting[0] == "surname" &&
                            (
                                sorting[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Surname
                    </th>
                    <th className="wide" onClick={() => sortTable("position")}>
                        {
                            sorting[0] == "position" &&
                            (
                                sorting[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Position
                    </th>
                </tr>
            </thead>
        );
    }
    function generateBody() {
        return (
            <tbody>
                {result.items && result.items.map((employee) => (
                    <tr className={employee.id === selected ? "selected" : ""} key={employee.id} id={employee.id} onClick={() => setSelected(employee.id)}>
                        <td>{employee.id}</td>
                        <td>{employee.name}</td>
                        <td>{employee.surname}</td>
                        <td>{employee.positionName}</td>
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
            <NavBarEmployee />
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
                    <div className="current button objectOption" onClick={() => fetchData()}>
                        <i className="fa-solid fa-list"></i>
                        <p>Refresh data</p>
                    </div>
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
            { box && box == "new" && <EmployeeNew hideBox={() => setBox("")} updateData={() => fetchData()}/> }
            { box && box == "edit" && <EmployeeEdit updateData={() => fetchData()} selected={selected}/> }
            { box && box == "info" && <EmployeeInfo selected={selected}/> }
        </>
    )
}

export default EmployeePage