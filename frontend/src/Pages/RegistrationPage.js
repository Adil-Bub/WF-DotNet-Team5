import React , {useContext,useState} from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import {useNavigate} from 'react-router-dom';


//npm install react-router-dom 
const RegistrationPage = () => {
    const [signUpObj,setSignUpObj] = useState({EID: '', Pass: '', Name:'', Desg: '', Dept: '', Gen: '', DoB: '', DoJ: ' '});
    const [empID,setEmpID] =useState('');
    const [empPass,setEmpPass] = useState('');
    const [empName,setEmpName] = useState('');
    const [empDesg,setEmpDesg] = useState('');
    const [empDept,setEmpDept] = useState('');
    const [empGen,setEmpGen] = useState('');
    const [empDoB,setEmpDoB] = useState('');
    const [empDoJ,setEmpDoJ] = useState('');

    const [error,setError] = useState(false);
    const{user,setUser} = useState(AppContext);
    
    const navigate = useNavigate();

    const handleEmpID = (event) => {
        setEmpID(event.target.value);
    }
    const handleEmpPass = (event) => {
        setEmpPass(event.target.value);
    }
    const handleEmpName = (event) => {
        setEmpName(event.target.value);
    }
    const handleEmpDesg = (event) => {
        setEmpDesg(event.target.value);
    }
    const handleEmpDept = (event) => {
        setEmpDept(event.target.value);
    }
    const handleEmpGen = (event) => {
        setEmpGen(event.target.value);
    }
    const handleEmpDoB = (event) => {
        setEmpDoB(event.target.value);
    }
    const handleEmpDoJ = (event) => {
        setEmpDoJ(event.target.value);
    }
    
    const handleSubmit = async (event) => {
        signUpObj.EmployeeId = empID;
        signUpObj.Password = empPass;
        signUpObj.EmployeeName = empName;
        signUpObj.Designation = empDesg;
        signUpObj.Department = empDept;
        signUpObj.Gender = empGen;
        signUpObj.DateOfBirth = empDoB;
        signUpObj.DateOfJoining = empDoJ;
        event.preventDefault();
        try {

            const response = await axios
                .post('https://localhost:7189/api/Authorization/register', signUpObj)
                //.get('./data.json')
                setSignUpObj(response.data);
                localStorage.setItem('user', JSON.stringify(response.data));
                console.log(response.data);
                navigate('/dashboard/user');
                
               
        }
        catch (error) {
            setError(error.Message);
        }
    }
    return (
        <div className="text-center">
            <h1>       
                Hi User👋
            </h1>
            <h6 className="mb-4">
                Please Enter Your Details   
            </h6>
            <form  onSubmit={handleSubmit}>
                
                <div className="container w-50 p-5 mb-4 text-center fs-5">
                    <div className="row">
                        <div className="mb-3 col">
                        Employee ID<input type="text" className="form-control" value={empID} onChange={handleEmpID} />
                        </div>
                        <div className="mb-3 col">
                        Create Password<input type="password" className="form-control" value={empPass} onChange={handleEmpPass} />
                        </div>
                    </div>
                    <div className="row mt-3">
                        <div className="mb-3">
                            Create Username<input type="text" className="form-control"  value={empName} onChange={handleEmpName} />
                        </div>
                    </div>
                    
                    <div className="row mt-3">
                        <div className="mb-3 col">
                            Designation<input type="text"  className="form-control" value={empDesg} onChange={handleEmpDesg} />
                        </div>
                        <div className="mb-3 col">
                            Department<input type="text"  className="form-control" value={empDept} onChange={handleEmpDept} />
                        </div>                    
                        <div className="mb-3 col">
                            Gender<input type="text"  className="form-control" value={empGen} onChange={handleEmpGen} />
                        </div>
                    </div>

                    <div className="row mt-3">
                        <div className="mb- col">
                            Date of Birth<input type="text"  className="form-control" value={empDoB} onChange={handleEmpDoB} />
                        </div>
                        <div className="mb-3 col">
                            Date of Joining<input type="text"  className="form-control" value={empDoJ} onChange={handleEmpDoJ} />
                        </div>
                    </div>

                    <div className="mt-3">
                        <button type="submit" className="btn btn-primary btn-lg"> Sign-Up </button>
                    </div>
                </div>
            </form>
        </div>
    );
}

export default RegistrationPage;
