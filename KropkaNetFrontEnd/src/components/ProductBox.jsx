import React from "react"

export default function ProductBox({product}) {
    console.log(product)
    return (
        <div className="product-box">
            {/*<img src={product.img ? meal.img : "https://i.pinimg.com/564x/ef/e8/d3/efe8d36db6281666a126189f05bfeff1.jpg"} alt="product-img" />*/}
            <img src="https://i.pinimg.com/564x/ef/e8/d3/efe8d36db6281666a126189f05bfeff1.jpg" alt="product-img" />
            <div className="product-info">
                <h5>{product.name}</h5>
                <p>Category: {product.category}</p>
                <p>Quantity: {product.quantity}</p>
            </div>
        </div>
    )
}