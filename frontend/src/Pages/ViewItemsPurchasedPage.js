import React, { useState, useEffect } from "react";
import axios from 'axios';
import { NavBar } from "../Component/LUMANav";

const ViewItemsPurchasedPage = () => {

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;

    const [ itemsPur, setItemsPur ] = useState([]);

    useEffect(() => {
        axios
            .get('https://localhost:7189/my-loans/' + user.employeeId, {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                setItemsPur(response.data.filter((item) => item.requestStatus == "Approved"));
            })
            .catch((error) => {
                console.error('Error fetching data: ', error);
            });
        }, [user.token]);

   
    return (
        <>
        <NavBar/>
        <div className="text-center">
            <h1>
            
            </h1>
            <div class="container text-center d-flex gap-3 mt-5">

          {itemsPur.map(dataObj => (

            <div class="card w-25 mx-auto fs-5" key={dataObj.itemId}>
                <div className="card-header">
                {dataObj.itemId}
                </div>
              <div class="card-body">
                <h3 class="card-title">{dataObj.itemDescription}</h3>
                  <p class="card-text5"  >
                  <strong>Category:</strong> {dataObj.itemCategory}  <br />
                  <strong>Make:</strong> {dataObj.itemMake}  <br />
                  <strong>Valuation:</strong> ₹ {dataObj.itemValuation}  <br />
                  </p>
              </div>
            </div>

          ))}
        </div>
            
        </div>
        </>
    );
}

export default ViewItemsPurchasedPage;