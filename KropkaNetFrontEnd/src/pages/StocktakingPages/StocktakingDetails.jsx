import React, { useState } from 'react'

function StocktakingDetails() {

    const [stocktaking, setStocktaking] = useState({
        
    })

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
        </>
    )
}

export default StocktakingDetails