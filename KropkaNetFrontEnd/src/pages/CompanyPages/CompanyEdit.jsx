import React, { useState } from 'react'

function CompanyEdit({ hideBox, updateData }) {

	const [company, setCompany] = useState({
		"id": 2003,
		"nip": "testcompany",
		"krs": "testcompany",
		"companyName": "testcompany",
		"note": "testcompany",
		"addressId": 4003,
		"address": {
		  "id": 4003,
		  "country": "testcompany",
		  "city": "testcompany",
		  "zipCode": "testcompany",
		  "street": "testcompany",
		  "building": "testcompany",
		  "premises": "testcompany"
		},
		"departments": [],
		"users": []
	})

	const [companyEdit, setCompanyEdit] = useState({
		"nip": company.nip,
		"krs": company.krs,
		"companyName": company.companyName,
		"note": company.note,
		"country": company.address.country,
		"city": company.address.city,
		"zipCode": company.address.zipCode,
		"street": company.address.street,
		"building": company.address.building,
		"premises": company.address.premises
	})

	const [departments, setDepartments] = useState([
		{
			"id": 1,
			"departmentName": "name1"
		},
		{
			"id": 2,
			"departmentName": "name2"
		},
		{
			"id": 3,
			"departmentName": "name3"
		},
		{
			"id": 4,
			"departmentName": "name4"
		},
		{
			"id": 5,
			"departmentName": "name5"
		},
	])

	const [newDepartment, setNewDepartment] = useState("")
	
	const handleSubmit = (e) => {
		e.preventDefault();
		//console.log(dataToSend);
		hideBox();
	};
	const addDepartment = (e) => {
		e.preventDefault();
		console.log(newDepartment);
	};

    function handleInputChange(inputId) {
        setCompanyEdit(prev => (
            {
                ...prev,
                [inputId]: document.getElementById(inputId).value
            }
        ))
    }

	function handleDelete(element) {
		alert(`deleted ${element}`)
	}

    const [sorting, setSorting] = useState(["id", 0])
	function sortTable(column) {
        setSorting(prev => {
            if (prev[0] === column && prev[1] === 0) return [column, 1]
            return [column, 0]
        })
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
                    <th className="wide" onClick={() => sortTable("departmentName")}>
                        {
                            sorting[0] == "departmentName" &&
                            (
                                sorting[1] === 0
                                ? <i className="fa-solid fa-arrow-down-a-z"></i>
                                : <i className="fa-solid fa-arrow-up-a-z"></i>
                            )
                        }
                        Department Name
                    </th>
                    <th className="u-thin no-select">
                        
                    </th>
                </tr>
            </thead>
        );
    }
    function generateBody() {
        return (
            <tbody>
                {departments.map((department) => (
                    <tr className="not-clickable" key={department.id} id={department.id}>
                        <td className="u-thin">{department.id}</td>
                        <td>{department.departmentName}</td>
                        <td className="u-thin delete-item" onClick={() => handleDelete(department.id)}>
							<i className="fa-solid fa-trash-can"></i>
						</td>
                    </tr>
                ))}
            </tbody>
        )
    }

	return (
		<div className="outside-box">
			<div className="content big half">
				<div className="half">
					<h1>Edit company</h1>
					<form>
						<h4>Company data</h4>
						<div className="layer row">
                	        <div className="input-container">
                	            <label htmlFor="companyName">Company name:</label>
                	            <input
                	                type="text"
                	                id="companyName"
                	                onChange={() => handleInputChange("companyName")}
                	                value={companyEdit.companyName}
                	            />
                	        </div>
                	        <div className="input-container">
                	            <label htmlFor="nip">NIP:</label>
                	            <input
                	                type="text"
                	                id="nip"
                	                onChange={() => handleInputChange("nip")}
                	                value={companyEdit.nip}
                	            />
                	        </div>
                	        <div className="input-container">
                	            <label htmlFor="krs">KRS:</label>
                	            <input
                	                type="text"
                	                id="krs"
                	                onChange={() => handleInputChange("krs")}
                	                value={companyEdit.krs}
                	            />
                	        </div>
                	    </div>
						<h4>Addres</h4>
						<div className="layer row">
                	        <div className="input-container">
                	            <label htmlFor="country">Country:</label>
                	            <input
                	                type="text"
                	                id="country"
                	                onChange={() => handleInputChange("country")}
                	                value={companyEdit.country}
                	            />
                	        </div>
                	        <div className="input-container">
                	            <label htmlFor="city">City:</label>
                	            <input
                	                type="text"
                	                id="city"
                	                onChange={() => handleInputChange("city")}
                	                value={companyEdit.city}
                	            />
                	        </div>
                	        <div className="input-container">
                	            <label htmlFor="zipCode">Zip code:</label>
                	            <input
                	                type="text"
                	                id="zipCode"
                	                onChange={() => handleInputChange("zipCode")}
                	                value={companyEdit.zipCode}
                	            />
                	        </div>
                	    </div>
						<div className="layer row">
                	        <div className="input-container">
                	            <label htmlFor="street">Street:</label>
                	            <input
                	                type="text"
                	                id="street"
                	                onChange={() => handleInputChange("street")}
                	                value={companyEdit.street}
                	            />
                	        </div>
                	        <div className="input-container">
                	            <label htmlFor="building">Building:</label>
                	            <input
                	                type="text"
                	                id="building"
                	                onChange={() => handleInputChange("building")}
                	                value={companyEdit.building}
                	            />
                	        </div>
                	        <div className="input-container">
                	            <label htmlFor="premises">Premises:</label>
                	            <input
                	                type="text"
                	                id="premises"
                	                onChange={() => handleInputChange("premises")}
                	                value={companyEdit.premises}
                	            />
                	        </div>
                	    </div>
						<h4>Notes</h4>
						<div className="layer row">
                	        <div className="input-container">
                	            <textarea
                	                type="text"
                	                id="note"
                	                onChange={() => handleInputChange("note")}
                	                value={companyEdit.note}
                	            />
                	        </div>
                	    </div>
						<button type="submit" onClick={handleSubmit}>Update</button>
					</form>
				</div>
				<div className="half">
					<h1>Departments</h1>
					<div className="list">
						<table>
							{ generateHeader() }
							{ generateBody() }
						</table>
                	</div>
					<div className="new-object">
						<input
                	        type="text"
                	        id="newDepartment"
                	        onChange={() => setNewDepartment(document.getElementById("newDepartment").value)}
                	        value={newDepartment}
                	    />
						<button type="submit" onClick={addDepartment}>Add</button>
					</div>
				</div>
			</div>
		</div>
	)
}

export default CompanyEdit