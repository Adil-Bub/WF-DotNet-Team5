import React, {useState, useEffect} from "react";
import {useNavigate} from 'react-router-dom';
import axios from "axios";
import { NavBar } from "../Component/LUMANav";
import '../App.css';

const ApplyLoansPage = () => {
  const storedUser = localStorage.getItem('user');
  const user = storedUser ? JSON.parse(storedUser) : null;
  const navigate = useNavigate();

  const url ='https://localhost:7189/api/Items';
    
  const [data, setData] = useState([]);

  const fetchInfo = () => {
    return axios.get(url).then((res) => setData(res.data));
  };

  useEffect(() => {
    fetchInfo();
  }, []);
  
  const handleButtonClick = async(item) => {
    console.log("hi");
    try {
      const requestData = {
        employeeId: user.employeeId,
        itemId: item.itemId
      };
      console.log(requestData);
      const response = await axios.post("https://localhost:7189/api/EmployeeRequest/add", requestData, {
        headers: { 'Authorization': 'Bearer ' + user.token }
      });
      
      if(response.status === 200)
      {
        navigate("/dashboard/user/my-loans");
      }
      else 
      {
        window.alert("Failed to apply, try again");
      }
    }
    catch(error)
    {
      console.log("Error", error);
    }
  }
  return (
    <>
    <NavBar/>
    <div >
        <h1 >Apply for Loan</h1>
        <div class="container text-center d-flex gap-3 mt-5">
           
            {data.map(dataObj => (
              
                <div class="card w-25 mx-auto fs-5" key={dataObj.itemId}>
                    <div class="card-body">
                    <h3 class="card-title">{dataObj.itemDescription}</h3>
                        <p class="card-text5"  > 
                        Item Id: {dataObj.itemId} <br />
                        Item Category: {dataObj.itemCategory}  <br />
                        {/* // Item Description: {dataObj.itemDescription}  <br /> */}
                        Item Make: {dataObj.itemMake}  <br />
                        Issue Status: {dataObj.issueStatus}  <br />
                        </p>
                        <div className="mt-3">
                        <button className="btn btn-primary btn-lg" onClick={() => handleButtonClick(dataObj)}> Apply </button>
                    </div>
                    </div>
                </div>
                
        ))}
        </div>
    </div>
    </>
  );
}


export default ApplyLoansPage;