import React, { useState } from 'react'

function CompanyNew({ hideBox, updateData }) {

	const [newCompanyData, setNewCompanyData] = useState({
		"nip": "",
		"krs": "",
		"companyName": "",
		"note": "",
		"country": "",
		"city": "",
		"zipCode": "",
		"street": "",
		"building": "",
		"premises": ""
	})
	
	const handleSubmit = (e) => {
		e.preventDefault();
		//console.log(dataToSend);
		hideBox();
	};

    function handleInputChange(inputId) {
        setNewCompanyData(prev => (
            {
                ...prev,
                [inputId]: document.getElementById(inputId).value
            }
        ))
    }

	return (
		<div className="outside-box">
			<div className="content big">
				<h1>New company</h1>
				<form>
					<h4>Company data</h4>
					<div className="layer row">
                        <div className="input-container">
                            <label htmlFor="companyName">Company name:</label>
                            <input
                                type="text"
                                id="companyName"
                                onChange={() => handleInputChange("companyName")}
                                value={newCompanyData.companyName}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="nip">NIP:</label>
                            <input
                                type="text"
                                id="nip"
                                onChange={() => handleInputChange("nip")}
                                value={newCompanyData.nip}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="krs">KRS:</label>
                            <input
                                type="text"
                                id="krs"
                                onChange={() => handleInputChange("krs")}
                                value={newCompanyData.krs}
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
                                value={newCompanyData.country}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="city">City:</label>
                            <input
                                type="text"
                                id="city"
                                onChange={() => handleInputChange("city")}
                                value={newCompanyData.city}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="zipCode">Zip code:</label>
                            <input
                                type="text"
                                id="zipCode"
                                onChange={() => handleInputChange("zipCode")}
                                value={newCompanyData.zipCode}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="street">Street:</label>
                            <input
                                type="text"
                                id="street"
                                onChange={() => handleInputChange("street")}
                                value={newCompanyData.street}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="building">Building:</label>
                            <input
                                type="text"
                                id="building"
                                onChange={() => handleInputChange("building")}
                                value={newCompanyData.building}
                            />
                        </div>
                        <div className="input-container">
                            <label htmlFor="premises">Premises:</label>
                            <input
                                type="text"
                                id="premises"
                                onChange={() => handleInputChange("premises")}
                                value={newCompanyData.premises}
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
                                value={newCompanyData.note}
                            />
                        </div>
                    </div>
					<button type="submit" onClick={handleSubmit}>Create</button>
				</form>
			</div>
		</div>
	)
}

export default CompanyNew