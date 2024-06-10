import React, { useState, useContext, useEffect } from 'react';
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';

function ProductEdit({ updateData, selected }) {

    const { state, setState } = useContext(GlobalStateContext);

    const backendUrl = "https://localhost:44396";

    const [product, setProduct] = useState({})
	const [updateProduct, setUpdateProduct] = useState({
        "name": "",
        "category": "",
        "note": ""
	})

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
	useEffect(() => {
        if (product) {
            updateInputs();
        }
    }, [product]);

    function updateInputs() {
        setUpdateProduct({
            "name": product.name,
            "category": product.category,
            "note": product.note
        })
    }
	
	async function handleSubmit(e) {
        e.preventDefault();

        const token = await refreshToken();
        const formData = new FormData();
        formData.append('name', updateProduct.name);
        formData.append('category', updateProduct.category);
        formData.append('note', updateProduct.note);

        const apiCall = `kropkaNet/product/update/${selected}`;
        try {
            const response = await axiosInstance.put(apiCall, formData, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });

            updateData();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    function handleInputChange(inputId) {
        setUpdateProduct(prev => ({
            ...prev,
            [inputId]: document.getElementById(inputId).value
        }))
    }

    return (
        <div className="outside-box">
			<div className="content big scroll">
                <h1 className="title">Edit product - {selected}</h1>
                <div className="bottom divide">
                    <div className="half">
                        <form>
                            <div className="layer">
                                <div className="input-container">
                                    <label htmlFor="name">Name:</label>
                                    <input
                                        type="text"
                                        id="name"
                                        onChange={() => handleInputChange("name")}
                                        value={updateProduct.name}
                                    />
                                </div>
                            </div>
                            <div className="layer">
                                <div className="input-container">
                                    <label htmlFor="category">Category:</label>
                                    <input
                                        type="text"
                                        id="category"
                                        onChange={() => handleInputChange("category")}
                                        value={updateProduct.category}
                                    />
                                </div>
                            </div>
                            <div className="layer">
                                <label htmlFor="note">Notes:</label>
                                <div className="input-container product-note">
                                    <textarea
                                        type="text"
                                        id="note"
                                        onChange={() => handleInputChange("note")}
                                        value={updateProduct.note}
                                        placeholder="Notes"
                                    />
                                </div>
                            </div>
                        </form>
                    </div>
                    <div className="half image-upload">
                        <div className="layer">
                            <img
                                src={product.imgUrl ? `${backendUrl}${product.imgUrl}` : "https://i.pinimg.com/564x/ef/e8/d3/efe8d36db6281666a126189f05bfeff1.jpg"} 
                                alt="product-img"
                                style={{ width: '70%', height: 'auto' }}
                            />
                        </div>
                    </div>
                </div>
                <div className="finish product">
                    <button onClick={handleSubmit}>Update</button>
                </div>
            </div>
        </div>
    )
}

export default ProductEdit