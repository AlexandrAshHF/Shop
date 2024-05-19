import React, { useState, useEffect } from "react";
import classes from "./styles/BasketPage.module.css";
import LayoutUser from "../components/LayoutUser";
import {Table, Button} from "react-bootstrap";

export default function BasketPage({...props}){
    const [products, SetProducts] = useState([]);
    
    async function makeOrder(){
        let token = localStorage.getItem("auth");

        if(!token){
            window.location = `/account/signIn`;
        }

        let response = await fetch("https://localhost:7265/api/Orders/CreateOrder", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
                'Authorization': token
            },
            body: JSON.stringify(products.map(x => {
                return {id: x.product.id, size: x.size}
            }))
        });

        if(response.ok){
            localStorage.removeItem("basket");
            SetProducts([]);
            window.location.reload();
        }
    }

    function delFromBasket(item){
        let copy = products.filter(x => x.product.id != item.product.id);
        let basketCopy = copy.map(x => ({product: x.product, size: x.size}))
    
        SetProducts(copy);
        localStorage.removeItem("basket");
        localStorage.setItem("basket", JSON.stringify(basketCopy));
    }

    function moveToProd(item){
        window.location = `/product/${item.product.id}`;
    }

    useEffect(() => {
        let prods = localStorage.getItem("basket")
            ? JSON.parse(localStorage.getItem("basket"))
            : [];

        SetProducts(prods);
    }, [])

    return(
        <div className={classes.main}>
            <LayoutUser/>
            {products.length < 1 && (
                <label>You haven't products in basket</label>
            )}
                <Table className={classes.tableItem} style={{backgroundColor: "lightgray",
                textAlign: "center"}}>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Price</th>
                            <th>Size</th>
                            <th style={{width: 300}}>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {products.map((item) => (
                            <tr>
                                <td>{item.product.name}</td>
                                <td>{item.product.price - (item.product.price*(item.product.discount/100))}</td>
                                <td>{item.size}</td>
                                <td>
                                    <Button variant="primary" onClick={() => moveToProd(item)}
                                    style={{marginRight: 10}}>
                                        Move to product
                                    </Button>
                                    <Button variant="danger"  onClick={() => delFromBasket(item)}>Delete</Button>
                                </td>
                            </tr>))}
                    </tbody>
                </Table>
            <Button onClick={async () => await makeOrder()}
            style={{marginLeft: 550, marginTop: 30}}>
                Make an order
            </Button>
        </div>
    )
}