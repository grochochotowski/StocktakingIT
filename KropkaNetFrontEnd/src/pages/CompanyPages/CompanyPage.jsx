import React, { useState, useEffect, useRef } from 'react'
import { Link } from 'react-router-dom'

import NavBar from '../../components/NavBar'
import CompanyNew from './CompanyNew'
import CompanyEdit from './CompanyEdit'
import CompanyInfo from './CompanyInfo'

import '../../styles/mainSubPage.css'
import '../../styles/form.css'
import '../../styles/list.css'
import '../../styles/new.css'
import '../../styles/info.css'

function CompanyPage () {

    const [sorting, setSorting] = useState(["id", 0])
    const [filters, setFilters] = useState({ "filters" : "" })
    const [selected, setSelected] = useState(0);
    const [page, setPage] = useState(1);
    const [result, setResult] = useState({
        "items": [
          {
            "id": 4,
            "companyName": "test"
          },
          {
            "id": 1003,
            "companyName": "string4"
          },
          {
            "id": 2002,
            "companyName": "user2company"
          },
          {
            "id": 2003,
            "companyName": "testcompany"
          }
        ],
        "totalItems": 4,
        "totalPages": 1
    })
    const [box, setBox] = useState("");

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
                        ID
                    </th>
                    <th className="wide" onClick={() => sortTable("companyName")}>
                        {
                            sorting[0] == "companyName" &&
                            (
                                sorting[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Company Name
                    </th>
                </tr>
            </thead>
        );
    }
    function generateBody() {
        return (
            <tbody>
                {result.items && result.items.map((company) => (
                    <tr className={company.id === selected && "selected"} key={company.id} id={company.id} onClick={() => setSelected(company.id)}>
                        <td>{company.id}</td>
                        <td>{company.companyName}</td>
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
                    <Link to="/companies" className="current button">
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
            { box && box == "new" && <CompanyNew hideBox={() => setBox("")} updateData={() => fetchData()}/> }
            { box && box == "edit" && <CompanyEdit hideBox={() => setBox("")} updateData={() => fetchData()} selected={selected}/> }
            { box && box == "info" && <CompanyInfo updateData={() => fetchData()} selected={selected}/> }
        </>
    )
}

export default CompanyPage