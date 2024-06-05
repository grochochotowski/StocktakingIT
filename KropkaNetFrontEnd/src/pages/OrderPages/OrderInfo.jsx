import React, { useState } from 'react'
import { Link } from 'react-router-dom';

import '../../styles/mainSubPage.css'
import '../../styles/info.css'

function OrderNew({ updateData }) {

	const [data, setData] = useState({
		"id" : 1,
		"dateOfOrderExecution": "2024-05-27T20:48:41.712",
		"state": -1,
		"departmentName": "string2003",
		"stocktakingId": 3
	});

	const formatDateTime = (dateString) => {
		const date = new Date(dateString);
		const options = { year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit', second: '2-digit' };
		return date.toLocaleString(undefined, options);
	};
	
	return (
		<div className="outside-box">
			<div className="content">
				<h1>Order {data.id} - {data.departmentName}</h1>
				<div className="info-box">
					<div className="info-line">
						<h4>Date & time:</h4>
						<h6>{formatDateTime(data.dateOfOrderExecution)}</h6>
					</div>
					<div className="info-line">
						<h4>State:</h4>
						{
							(() => {
								if (data.state == -1) {
									return <h6 className='warning'>Rejected</h6>;
								}
								else if (data.state == 0) {
									return <h6>No decision</h6>;
								} 
								else if (data.state == 1) {
									return <h6 className='success'>Accepted</h6>;
								} 
								else {
									return <h6 className='warning'>State error</h6>;
								}
							})()
						}
					</div>
					<div className="info-line">
						<h4>Stockatking ID:</h4>
						{
							(() => {
								if (data.stocktakingId == null && data.state == -1) {
									return <h6 className='warning'>Order rejected - no stocktaking</h6>;
								}
								else if (data.stocktakingId == null && data.state != -1) {
									return <h6>No decision - no stocktaking</h6>;
								} 
								else if (data.state != null && data.state != -1) {
									return <h6>{data.stocktakingId}</h6>;
								} 
								else {
									return <h6 className='warning'>Data ERROR</h6>;
								}
							})()
						}
					</div>
				</div>
				<Link to={`/orders/${data.id}/stocktaking`} className='button wide'>Go to stocktaking</Link>
			</div>
		</div>
	)
}

export default OrderNew