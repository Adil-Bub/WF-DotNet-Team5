import React, {useState, useEffect} from "react"
import axios from "axios";
import { NavBar } from "../Component/Navbar";
import '../App.css';
import { AppContext } from "../Context/App.context";
import { Alert } from "bootstrap";
const ApplyLoansPage = () => {
    const url ='https://localhost:7189/api/Items';
  
  const [data, setData] = useState([]);

  const fetchInfo = () => {
    return axios.get(url).then((res) => setData(res.data));
  };

  useEffect(() => {
    fetchInfo();
  }, []);
  function handleClick(item) {
    // Alert("Item Loan request sent");
    console.log( item);
} 
  return (
    <>
    <NavBar/>
    <div >
        <h1 >Apply for Loan</h1>
        <div class="container text-center d-flex gap-3 mt-5">
           
            {data.map(dataObj => (
               
                <div class="card w-25 mx-auto fs-5" onClick={handleClick(dataObj)}>
                  
                    <div class="card-body">
                    <h3 class="card-title">{dataObj.itemDescription}</h3>
                        <p class="card-text5"  > 
                        Item Id: {dataObj.itemId} <br />
                        Item Category: {dataObj.itemCategory}  <br />
                        {/* // Item Description: {dataObj.itemDescription}  <br /> */}
                        Item Make: {dataObj.itemMake}  <br />
                        Issue Status: {dataObj.issueStatus}  <br />
                        </p>
                    </div>
                </div>
        ))}
        </div>
         
       
       
     
    </div>
    </>
  );
}


export default ApplyLoansPage;