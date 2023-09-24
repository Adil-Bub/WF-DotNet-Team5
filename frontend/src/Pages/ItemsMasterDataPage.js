import React, { useContext, useState,useEffect } from "react";
import axios from 'axios';
import { AppContext } from "../Context/App.context";
import { useNavigate } from 'react-router-dom';
import { NavBar } from "../Component/LAMANav";
import { FaEdit, FaTrash } from 'react-icons/fa'
import EditItemModal  from "../Component/EditItemModal";


const ItemsMasterDataPage = () => {

    const storedUser = localStorage.getItem('user');
    const user = storedUser ? JSON.parse(storedUser) : null;

    const navigate = useNavigate();
    const [items, setItems] = useState([]);

    const [showModal, setShowModal] = useState(false);
    const [selectedItem, setSelectedItem] = useState({});

    const handleCloseModal = () => {
        setSelectedItem(null);
        setShowModal(false);
    };

    useEffect(() => {
        axios
            .get('https://localhost:7189/api/Items', {
                headers: { 'Authorization': 'Bearer ' + user.token }
            })
            .then((response) => {
                setItems(response.data);
                console.log(response.data);
            })
            .catch((error) => {
                console.error('Error fetching data: ', error);
            });
        }, [user.token]);
   
    return (
       

                <div>
                    <NavBar/>
                    <div className="container mt-5">
                        <div className="row justify-content-center">
                            <div className="table-responsive">
                                <table className="table table-hover table-bordered">
                                    <thead className="text-center align-items-center">
                                        <tr>
                                            <th className="align-middle">Item ID</th>
                                            <th className="align-middle">Category</th>
                                            <th className="align-middle">Description</th>
                                            <th className="align-middle">Make</th>
                                            <th className="align-middle">Valuation</th>
                                            <th className="align-middle">Edit</th>
                                            <th className="align-middle">Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody className="table-group-divider text-center">
                                        {items.map((item) => (
                                    <tr key={item.itemId}>
                                        <td>{item.itemId}</td>
                                        <td>{item.itemCategory}</td>
                                        <td>{item.itemDescription}</td>
                                        <td>{item.itemMake}</td>
                                        <td>{item.itemValuation}</td>
                                        <td>
                                                    <FaEdit className="edit-icon" color="blue" onClick={() => {
                                                    setSelectedItem(item);
                                                    setShowModal(true);
                                                    }}></FaEdit>
                                                </td>
                                                <td>
                                                    <FaTrash className="delete-icon" color="red" onClick={() => {
                                                        axios
                                                        .delete(`https://localhost:7189/api/Items/`, {
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
                            </div>
                        </div>
                    </div>
                </div>
            );
        }
        
    


export default ItemsMasterDataPage;