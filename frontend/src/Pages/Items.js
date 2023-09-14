import React, {useState, useEffect} from "react"
import axios from "axios";
import '../App.css';
import { Alert } from "bootstrap";
const ShowItems = () => {
    const url ='https://localhost:7189/api/Items';
  
  const [data, setData] = useState([]);

  const fetchInfo = () => {
    return axios.get(url).then((res) => setData(res.data));
  };

  useEffect(() => {
    fetchInfo();
  }, []);
  function handleClick() {
    // Alert("Item Loan request sent");
    console.log('in cardClick');
} 
  return (
    <div >
      <h1 >Apply for Loan</h1>
     
        {data.map((dataObj) => {
          return (
            
            <div className="grid"           >
              
                
                 <p className='item'  onClick={handleClick} > 
                  Item Id: {dataObj.itemId} <br />
                  Item Category: {dataObj.itemCategory}  <br />
                  Item Description: {dataObj.itemDescription}  <br />
                  Item Make: {dataObj.itemMake}  <br />
                  Issue Status: {dataObj.issueStatus}  <br />
                 </p>
                
            </div>
          
          );
        })}
     
    </div>
  );
}

export default ShowItems;