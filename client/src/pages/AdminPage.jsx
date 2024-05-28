import React, { useEffect, useState } from "react";
import OrderTable from "../components/Admin/OrderTable";
import ProductTable from "../components/Admin/ProductTable";
import { useParams } from "react-router-dom";
import classes from "./styles/AdminPage.module.css";
import { Button, Form, Container, Row, Col } from 'react-bootstrap';
import Select from "react-select";
import ModalProduct from "../components/Product/ModalProduct";
import OrderModal from "../components/Admin/OrderModal";

export default function AdminPage({...params})
{
    const {adminKey, page} = useParams();

    const[selectedProd, SetSelectedProd] = useState(null);

    const [modalVisible, setModalVisible] = useState(false);

    const [products, SetProducts] = useState([]);
    const [orders, SetOrders] = useState([]);
    const [types, SetTypes] = useState([]);

    async function fetchProducts(){
        let response = await fetch("https://localhost:7265/api/Catalog/GetAll",{
            method: "GET",
            headers:{
                'Content-Type': 'application/json',
            }
        })

        if(response.ok){
            let data = await response.json();
            SetProducts(data);
        }
    }

    async function fetchTypes(){
        let response = await fetch("https://localhost:7265/api/CTP/GetTypes",{
            method: "GET",
            headers:{
                'Content-Type': 'application/json',
            }
        })

        if(response.ok){
            let data = await response.json();
            SetTypes(data);
        }
    }

    async function fetchOrders(){
        let response = await fetch(`https://localhost:7265/api/Orders/GetAll`,{
            method: "GET",
            headers:{
                'Content-Type': 'application/json',
            }
        })

        if(response.ok){
            let data = await response.json();
            SetOrders(data);
        }
    }

    async function UpdateOrder(order){
        let nextStatus = order.status + 1;

        if(nextStatus > 3)
            return;

        console.log(order.id);
        console.log(nextStatus);
        let response = await fetch("https://localhost:7265/api/Orders/UpdateOrder", {
            method: "PATCH",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({"id": order.id, "status": nextStatus})
        });

        if(response.ok)
            window.location.reload();

        else
            console.log(await response.json())
    }

    async function DeleteProduct(prod){
        console.log(prod.id);
        let response = await fetch(`https://localhost:7265/api/Admin/${adminKey}/DeleteProduct`, {
            method: "DELETE",
            headers: {
                'Content-Type': 'application/json',                
            },
            body: JSON.stringify(prod.id)
        })

        if(response.ok)
            window.location.reload();

        else
            console.log(await response.json());
    }

    useEffect(() =>{
        fetchProducts();
        fetchOrders();
        fetchTypes();
    }, [])

    return(
        <div {...params} className={classes.main}>
            {(modalVisible && page == "products") && (
                <ModalProduct product={selectedProd} types={types} adminKey={adminKey} 
                closeWindow={() => {setModalVisible(false); SetSelectedProd(null)}}/>
            )}
            {(modalVisible && page == "orders") && (
                <OrderModal closeWindow={() => setModalVisible(false)}/>
            )}
            <div className={classes.btns}>
                <Button className={page == "products" ? classes.selected : classes.unselected}
                onClick={() => window.location = `/admin/${adminKey}/products`}>
                    Products
                </Button>
                <Button className={page == "orders" ? classes.selected : classes.unselected}
                onClick={() => window.location = `/admin/${adminKey}/orders`}>
                    Orders
                </Button>
            </div>
            <Button variant="success" onClick={() => {SetSelectedProd(null); setModalVisible(true)}}
                style={{marginTop: 20, marginBottom: 20, width: 100}}>
                Create
            </Button>
            {page == "products" && (
                <div>
                    <ProductTable products={products} delClick={async (item) => await DeleteProduct(item)}
                        editClick={(item) => {SetSelectedProd(item); setModalVisible(true)}}/>
                </div>
            )}
            {page == "orders" && (
                <div>                                
                    <OrderTable orders={orders} updateOrder={async (item) => await UpdateOrder(item)}/>
                </div>
            )}
        </div>
    )
}