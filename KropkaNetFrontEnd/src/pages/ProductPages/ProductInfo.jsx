import React, { useState, useContext, useEffect } from 'react';
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';

function ProductInfo({selected}) {

    const { state, setState } = useContext(GlobalStateContext);

    const backendUrl = "https://localhost:44396";

    const [product, setProduct] = useState({})

    async function fetchData() {
        const token = await refreshToken();
        getProduct(token)
    }

    async function getProduct(token) {
        let apiCall = `kropkaNet/product/${selected}`;
        try {
            const response = await axiosInstance.get(apiCall, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setProduct(response.data);
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
				<h1>Product - {product.id}</h1>
                <div className="info-box divide">
					<div className="half product-info">
                        <div className="info-line">
                            <h4>Name:</h4>
                            <h6>{product.name}</h6>
                        </div>
                        <div className="info-line">
                            <h4>Category:</h4>
                            <h6>{product.category}</h6>
                        </div>
                        <div className="info-line">
                            <h4>Note:</h4>
                            <h6>{product.note}</h6>
                        </div>
					</div>
					<div className="half">
                        <img
                            src={product.imgUrl ? `${backendUrl}${product.imgUrl}` : "https://i.pinimg.com/564x/ef/e8/d3/efe8d36db6281666a126189f05bfeff1.jpg"} 
                            alt="product-img"
                            style={{ width: '70%', height: 'auto', borderRadius: '10px' }}
                        />
					</div>
				</div>
			</div>
		</div>
    )
}

export default ProductInfo