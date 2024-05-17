import React, { useState, useEffect } from "react";
import classes from "./styles/BasketPage.module.css";
import {Table, Button} from "react-bootstrap";

export default function BasketPage({...props}){
    const [products, SetProducts] = useState([]);
    
    async function makeOrder(){
        let response = await fetch("https://localhost:7265/api/Orders/CreateOrder", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({"productsId": products})
        });

        if(response.ok){
            localStorage.removeItem("basket");
            SetProducts([]);
        }
    }

    useEffect(() => {
        products = localStorage.getItem("basket")
    }, [])

    return(
        <div className={classes.main}>
            {products.length < 1 && (
                <label>You haven't products in basket</label>
            )}
            {products.map((item) => (
                <Table className={classes.tableItem} style={{backgroundColor: "lightgray"}}>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Price</th>
                            <th>Link</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{item.name}</td>
                            <td>{item.price}</td>
                            <td>
                                <Button variant="primary">Move to product</Button>
                            </td>
                            <td>
                                <Button variant="danger">Delete</Button>
                            </td>
                        </tr>
                    </tbody>
                </Table>
            ))}
            <Button>Make an order</Button>
        </div>
    )
}