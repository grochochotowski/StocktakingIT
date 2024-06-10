import React, { useState, useContext, useEffect } from 'react';
import { useDropzone } from 'react-dropzone';
import { GlobalStateContext } from '../../GlobalState';
import { axiosInstance, refreshToken } from '../../api/axios';

function ProductNew({ hideBox, updateData }) {
    const { state, setState } = useContext(GlobalStateContext);

	const [newProduct, setNewProduct] = useState({
        "name": "",
        "category": "",
        "note": ""
	})
    const [image, setImage] = useState(null)

    const { getRootProps, getInputProps } = useDropzone({
        accept: 'image/*',
        onDrop: acceptedFiles => {
            setImage(acceptedFiles[0]);
        }
    });
	
	async function handleSubmit(e) {
        e.preventDefault();

        const token = await refreshToken();
        const formData = new FormData();
        formData.append('name', newProduct.name);
        formData.append('category', newProduct.category);
        formData.append('note', newProduct.note);
        if (image) {
            formData.append('image', image);
        }

        const apiCall = 'kropkaNet/product/create';
        try {
            const response = await axiosInstance.post(apiCall, formData, {
                headers: {
                    Authorization: `Bearer ${token}`,
                    'Content-Type': 'multipart/form-data'
                }
            });

            updateData();
            hideBox();
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    }

    function handleInputChange(inputId) {
        setNewProduct(prev => ({
            ...prev,
            [inputId]: document.getElementById(inputId).value
        }))
    }

    return (
        <div className="outside-box">
			<div className="content big scroll">
                <h1 className="title">New product</h1>
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
                                        value={newProduct.name}
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
                                        value={newProduct.category}
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
                                        value={newProduct.note}
                                        placeholder="Notes"
                                    />
                                </div>
                            </div>
                        </form>
                    </div>
                    <div className="half image-upload">
                        <div className="layer">
                            <label htmlFor="note">Image:</label>
                            <div {...getRootProps({ className: 'dropzone image' })}>
                                <input {...getInputProps()} />
                                {!image && <h4>Drag an image here, or click to select one</h4>}
                                {image && <h4>{image.name}</h4>}
                            </div>
                        </div>
                    </div>
                </div>
                <div className="finish product">
                    <button onClick={handleSubmit}>Create</button>
                </div>
            </div>
        </div>
    )
}

export default ProductNew