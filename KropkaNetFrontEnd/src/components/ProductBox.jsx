import React, { useState, useEffect, useContext } from 'react'
import { axiosInstance, refreshToken } from '../api/axios';
import { GlobalStateContext } from '../GlobalState';


export default function ProductBox({warehouseId, product}) {

    const { state, setState } = useContext(GlobalStateContext);

    const [newQuantity, setNewQuantity] = useState(product.quantity)

    async function save() {
        let changeQuantity = newQuantity - product.quantity
        let version = ""
        if(changeQuantity < 0) {
            version = "removeProduct"
        }
        else if(changeQuantity > 0) {
            version = "addProduct"
        }

        const token = await refreshToken();
        let apiCall = `kropkaNet/warehouse/${warehouseId}/${version}/${product.id}?quantity=${Math.abs(changeQuantity)}`;
        try {
            const response = await axiosInstance.patch(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    function updateQuantity(e) {
        setNewQuantity(e.target.value)
    }

    return (
        <div className="product-box">
            {/*<img src={product.img ? meal.img : "https://i.pinimg.com/564x/ef/e8/d3/efe8d36db6281666a126189f05bfeff1.jpg"} alt="product-img" />*/}
            <img src="https://i.pinimg.com/564x/ef/e8/d3/efe8d36db6281666a126189f05bfeff1.jpg" alt="product-img" />
            <div className="product-info">
                <h5>{product.name}</h5>
                <p className="quantity">Quantity:&nbsp;
                    {
                        state.level == "employee" ?
                        <input type="number" min={0} value={newQuantity} onChange={updateQuantity}/> :
                        product.quantity
                    }
                &nbsp;</p>
            </div>
            {
                state.level == "employee" &&
                <div className="save" onClick={save}>
                    <i className="fa-solid fa-floppy-disk"></i>
                </div>
            }
        </div>
    )
}