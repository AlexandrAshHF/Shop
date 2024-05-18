import React from "react";
import classes from "./styles/ProductTable.module.css";
import {Table, Button} from "react-bootstrap";

export default function OrderTable({orders, updateOrder, ...params}){
    const statusList = ["None", "Proccessing", "Sent", "Recivied"];

    return(
        <div className={classes.main} {...params}>
            {orders.map((item) => (
                <Table className={classes.tableItem} style={{backgroundColor: "lightgray"}}>
                    <thead>
                        <tr>
                            <th>Date of create</th>
                            <th>Status</th>
                            <th>Email</th>
                            <th>Amount</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{item.dateOfCreate}</td>
                            <td>{statusList[item.status]}</td>
                            <td>{item.email}</td>
                            <td>{item.amount}</td>
                            <td>
                                <Button variant="primary" onClick={(item) => updateOrder(item)}>Status Up</Button>
                                <Button variant="primary" 
                                    onClick={(item) => window.location = `/order/${item.id}`}>
                                        Status Up
                                    </Button>
                            </td>
                        </tr>
                    </tbody>
                </Table>
            ))}
        </div>
    )
}