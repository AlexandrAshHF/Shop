import React from "react";
import classes from "./styles/ProductTable.module.css";
import {Table, Button} from "react-bootstrap";

export default function ProductTable({products, ...params}){
    return(
        <div className={classes.main}>
            {products.map((item) => (
                <Table className={classes.tableItem} style={{backgroundColor: "lightgray"}}>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Description</th>
                            <th>Price</th>
                            <th>Discount</th>
                            <th>ImageLinks</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{item.name}</td>
                            <td>{item.description}</td>
                            <td>{item.price}</td>
                            <td>{item.discount}</td>
                            <td>{item.imageLinks}</td>
                            <td>
                                <Button variant="primary">Редактировать</Button>{' '}
                                <Button variant="danger">Удалить</Button>
                            </td>
                        </tr>
                    </tbody>
                </Table>
            ))}
        </div>
    )
}