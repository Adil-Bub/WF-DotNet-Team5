import React, { useContext, useState,useEffect } from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import { useNavigate } from 'react-router-dom';
import { NavBar } from "../Component/LAMANav";
import { FaEdit, FaTrash } from 'react-icons/fa'
import EditItemModal  from "../Component/EditItemModal";
import AddItemModal from "../Component/AddItemModal";
import { Button } from "react-bootstrap";


const ItemsMasterDataPage = () => {

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;

    const navigate = useNavigate();
    const [items, setItems] = useState([]);

    const [showModal, setShowModal] = useState(false);
    const [showAdd, setShowAdd] = useState(false);
    const [selectedItem, setSelectedItem] = useState({});

    const handleCloseModal = () => {
        setSelectedItem(null);
        setShowModal(false);
    };

    const handleAdd = () => {
        setSelectedItem(null);
        setShowAdd(false);
    };

    useEffect(() => {
        axios
            .get('https://localhost:7189/api/Items/all', {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                setItems(response.data);
                console.log(response.data);
            })
            .catch((error) => {
                console.error('Error fetching data: ', error);
            });
        }, [selectedItem]);
   
    return (
       

                <div>
                    <NavBar/>
                    <div className="container mt-3 d-flex justify-content-around align-items-center">
                        <h5>
                            All Available Items are Shown Below
                        </h5>
                        <Button onClick={()=>{setSelectedItem({});setShowAdd(true)}}>Add Items</Button>
                    </div>
                    <div className="container mt-5">
                        <div className="row justify-content-center">
                            <div className="table-responsive">
                                <table className="table table-hover table-bordered" style={{borderRadius:'10px', overflow: 'hidden', border: '2px solid #ccc'}}>
                                    <thead className="text-center align-items-center">
                                        <tr>
                                            <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Item ID</th>
                                            <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Category</th>
                                            <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Description</th>
                                            <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Make</th>
                                            <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Valuation</th>
                                            <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Edit</th>
                                            <th className="align-middle" style={{ backgroundColor: 'darkcyan', color: 'white' }}>Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody className="table-group-divider text-center">
                                        {items.map((item) => (
                                    <tr key={item.itemId}>
                                        <td>{item.itemId}</td>
                                        <td>{item.itemCategory}</td>
                                        <td>{item.itemDescription}</td>
                                        <td>{item.itemMake}</td>
                                        <td>₹ {item.itemValuation}</td>
                                        <td>
                                                    <FaEdit className="edit-icon" color="darkcyan" onClick={() => {
                                                    setSelectedItem(item);
                                                    setShowModal(true);
                                                    }}></FaEdit>
                                                </td>
                                                <td>
                                                    <FaTrash className="delete-icon" color="red" onClick={() => {
                                                        axios
                                                        .delete(`https://localhost:7189/api/Items/` + item.itemId, {
                                                                headers: { 'Authorization': 'Bearer ' + user.token }
                                                        }).then(setItems(items.filter(x => x.itemId != item.itemId)))
                                                    }
                                                    }></FaTrash>
                                                </td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                                {showModal && <EditItemModal
                            showModal={showModal}
                            handleCloseModal={handleCloseModal}
                            selectedItem={selectedItem}
                            setShowModal={setShowModal}
                        >
                        </EditItemModal>}
                        {showAdd && <AddItemModal
                        showAdd={showAdd}
                        handleAdd={handleAdd}
                        setShowAdd={setShowAdd}>
                            </AddItemModal>}
                            </div>
                        </div>
                    </div>
                </div>
            );
        }
        
    


export default ItemsMasterDataPage;