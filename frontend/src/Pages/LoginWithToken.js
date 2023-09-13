import React , {useContext,useState} from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import {useNavigate} from 'react-router-dom';
//npm install react-router-dom 
const LoginWithToken = () => {
    const [loginobj,setLogin] = useState({EmployeeId: '', Password: ''});
    const [username,setUsername] =useState('');
    const [password,setPassword] = useState('');
    const [error,setError] = useState(false);
    const{user,setUser} = useContext(AppContext);
    const navigate = useNavigate();
    const handleUsername = (event) => {
        setUsername(event.target.value);
    }
    const handlepwd = (event) => {
        setPassword(event.target.value);
    }
    
    const handleSubmit = async (event) => {
        loginobj.EmployeeId= username;
        loginobj.Password=password;
        console.log("login", loginobj);
        event.preventDefault();
        try {

            const response = await axios
                .post('https://localhost:7189/api/Authorization/login', loginobj)
                //.get('./data.json')
                setUser(response.data);
                console.log(response.data);
                if(response.data.designation==='admin')
                {
                    navigate('/profile');
                }
               
        }
        catch (error) {
            setError(error.Message);
        }
    }
    return (
        <div>
            <form onSubmit={handleSubmit}>
                <div>
                    Username: <input type="text" value={username} onChange={handleUsername} />
                </div>
                <div>
                    Password: <input type="password" value={password} onChange={handlepwd} />
                </div>
                <div>
                    <button type="submit"> Login </button>
                </div>
                {Error &&<div>Invalid Details</div>}
            </form>
        </div>
    );
}

export default LoginWithToken;
