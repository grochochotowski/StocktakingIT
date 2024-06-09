import React, { useState, useEffect, useContext } from 'react'
import { useParams } from 'react-router-dom'
import { axiosInstance, refreshToken } from '../../api/axios';
import { GlobalStateContext } from '../../GlobalState';

import '../../styles/index.css'
import '../../styles/details.css'
import '../../styles/list.css'

function AddProduct({warehouseId, hideBox, updateData}) {

    const { state, setState } = useContext(GlobalStateContext);

    const [products, setProducts] = useState([]);
    const [sorting, setSorting] = useState(["id", 0])
    const [filters, setFilters] = useState({ "filters" : "" })

    async function fetchData() {
        const token = await refreshToken();
        let apiCall = `kropkaNet/product/all/getNoPag?` +
        `sortBy=${sorting[0]}&` +
        `sortDirection=${sorting[1] == 0 ? "ASC" : "DESC"}`;
        if (filters.filters) {
            apiCall += `&filters=${filters.filters}`;
        }

        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setProducts(response.data);
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    useEffect(() => {
        fetchData()
    }, [sorting, filters])
    
    
	function sortTable(column) {
        setSorting(prev => {
            if (prev[0] === column && prev[1] === 0) return [column, 1]
            return [column, 0]
        })
    }
    function changeFilters(filter) {
        setFilters(prev => ({
            "filters" : document.getElementById(filter).value == "" ? null : document.getElementById(filter).value
        }))
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
                    <th className="wide" onClick={() => sortTable("category")}>
                        {
                            sorting[0] == "category" &&
                            (
                                sorting[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Category
                    </th>
                    <th className="thin no-select">
                        Quantity
                    </th>
                    <th className="u-thin no-select"></th>
                </tr>
            </thead>
        );
    }
    function generateBody() {
        return (
            <tbody>
                {products.map((product) => (
                    <tr className="not-clickable" key={product.id} id={product.id}>
                        <td className="u-thin">{product.id}</td>
                        <td>{product.name}</td>
                        <td>{product.category}</td>
                        <td>
                            <input id={`q-${product.id}`} type="number" min={0} defaultValue={0}/>
                        </td>
                        <td>
                            <button onClick={()=> addProduct(product.id)}>Add</button>
                        </td>
                    </tr>
                ))}
            </tbody>
        )
    }
    async function addProduct(productId) {
        let quantity = parseInt(document.getElementById(`q-${productId}`).value)

        const token = await refreshToken();
        let apiCall = `kropkaNet/warehouse/${warehouseId}/addProduct/${productId}?quantity=${quantity == 0 ? 1 : quantity}`;
        try {
            const response = await axiosInstance.patch(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            hideBox()
            updateData()
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    return (
		<div className="outside-box">
			<div className="content big">
				<h1>Add product</h1>
                <input type="text" id="fitlers-product" placeholder="Find your product" onChange={() => changeFilters("fitlers-product")} value={filters.filters}/>
                <div className="list">
                    <table>
                        { generateHeader() }
                        { generateBody() }
                    </table>
                </div>
			</div>
		</div>
    )
}

export default AddProduct