import React, { useState, useEffect } from "react";
import { useNavigate } from 'react-router-dom';
import axios from "axios";
import { NavBar } from "../Component/LUMANav";
import '../App.css';
import { useAppContext } from "../Context/App.context";


const ApplyLoansPage = () => {

  const storedUser = localStorage.getItem('user');
  const user = storedUser ? JSON.parse(storedUser) : null;
  const navigate = useNavigate();

  const { selectedCategory } = useAppContext();
  const url = 'https://localhost:7189/api/Items/all';


  const [data, setData] = useState([]);

  const fetchInfo = () => {
    return axios.get(url, {
      headers: { 'Authorization': 'Bearer ' + user.token }
  })
    .then((res) => setData(res.data))
    .catch((error) => {
      console.error('Error fetching data: ', error);
  });
  };


  const byCategory = data.filter((data) => data.itemCategory === selectedCategory);


  useEffect(() => {
    fetchInfo();
  }, []);

  const handleButtonClick = async (item) => {
    console.log('I was clicked')
    try {
      const requestData = {
        employeeId: user.employeeId,
        itemId: item.itemId
      };

      const response = await axios.post("https://localhost:7189/api/EmployeeRequest/add", requestData, {
        headers: { 'Authorization': 'Bearer ' + user.token }
      });
      console.log(response)
      if (response.status === 200) {
        navigate("/dashboard/user/my-loans");
      }
      else {
        console.log("hi");
        window.alert("Failed to apply, try again");
      }
    }
    catch (error) {
      console.log("Error", error);
    }
  }


  return (
    <>
      <NavBar />
      <div className="text-center">
        <h1>
          Choose your Item
        </h1>
        <div class="container text-center d-flex gap-3 mt-5">

          {byCategory.map(dataObj => (

            <div class="card w-25 mx-auto fs-5" key={dataObj.itemId}>
              <div class="card-body">
                <h3 class="card-title">{dataObj.itemDescription}</h3>
                <p class="card-text5"  >
                  Item Id: {dataObj.itemId} <br />
                  Item Category: {dataObj.itemCategory}  <br />
                  Item Make: {dataObj.itemMake}  <br />
                  Issue Status: {dataObj.issueStatus}  <br />
                </p>
                <div className="mt-3">
                  <button  className="btn btn-outline-dark btn-lg" onClick={handleButtonClick}> Apply </button>
                </div>
              </div>
            </div>

          ))}
        </div>
      </div>
    </>
  )
    ;

}


export default ApplyLoansPage;