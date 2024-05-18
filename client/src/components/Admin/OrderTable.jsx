import React from "react";
import {Table, Button} from "react-bootstrap";

export default function OrderTable({orders, updateOrder, ...params}){
    const statusList = ["None", "Proccessing", "Sent", "Recivied"];

    return(
        <div {...params}>
            {orders.map((item) => (
                <Table style={{backgroundColor: "lightgray"}}>
                    <thead>
                        <tr>
                            <th>Date of create</th>
                            <th>Status</th>
                            <th>Amount</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{item.dateOfCreate}</td>
                            <td>{statusList[item.status]}</td>
                            <td>{item.amount}</td>
                            <td>
                                <Button variant="primary" onClick={(item) => updateOrder(item)}>Status Up</Button>
                                <Button variant="primary" 
                                    onClick={(item) => window.location = `/order/${item.id}`}>
                                        More Details
                                    </Button>
                            </td>
                        </tr>
                    </tbody>
                </Table>
            ))}
        </div>
    )
}