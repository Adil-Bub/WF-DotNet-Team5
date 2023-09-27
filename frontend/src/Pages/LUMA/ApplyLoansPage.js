import React, { useState, useEffect } from "react";
import { useNavigate } from 'react-router-dom';
import axios from "axios";
import { NavBar } from "../../Component/LUMANav";
import { useAppContext } from "../../Context/App.context";


const ApplyLoansPage = () => {

  const storedUser = localStorage.getItem('user');
  const user = storedUser ? JSON.parse(storedUser) : null;
  const navigate = useNavigate();

  const { selectedCategory } = useAppContext();
  const url = 'https://localhost:7189/api/Items/all';


  const [data, setData] = useState([]);
  const [disCard, setDisCard] = useState([]);

  const fetchInfo = () => {
    return axios.get(url, {
      headers: { 'Authorization': 'Bearer ' + user.token }
  })
    .then((res) => setData(res.data))
    .catch((error) => {
      console.error('Error fetching data: ', error);
  });
  };

  const disabledItems = () => {
    return axios.get(`https://localhost:7189/my-loans/${user.employeeId}`, {
      headers: { 'Authorization': 'Bearer ' + user.token }
  }).then((res) => setDisCard(res.data.map(item=>item.itemId)))
  }


  const byCategory = data.filter((data) => data.itemCategory === selectedCategory);


  useEffect(() => {
    fetchInfo();
    disabledItems();
  }, []);

  const handleButtonClick = async (item) => {
    console.log('I was clicked')
    try {
      const requestData = {
        employeeId: user.employeeId,
        itemId: item.itemId
      };
      console.log(requestData)
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
        <h4 className="mt-3">
                    Choose Item for <code>{user.employeeName} ({user.employeeId})</code> from <code>{selectedCategory}</code> Category
        </h4>
        <div class="container text-center d-flex gap-3 mt-3">

          {byCategory.map(dataObj => (
            <div class="card shadow w-25 mx-auto fs-5" key={dataObj.itemId}>
              <div class="card-body">
                <h3 class="card-header mb-2">{dataObj.itemDescription}</h3>
                <p class="card-text5"  >
                  <strong>Item Id:</strong> {dataObj.itemId} <br />
                  <strong>Category:</strong> {dataObj.itemCategory}  <br />
                  <strong>Make:</strong> {dataObj.itemMake}  <br />
                </p>
                <div className="mt-3">
                  <button className="btn btn-outline-dark btn-lg" onClick={()=>handleButtonClick(dataObj)} disabled = {disCard.includes(dataObj.itemId)}>Apply</button>
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