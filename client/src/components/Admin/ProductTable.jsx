import React from "react";
import {Table, Button} from "react-bootstrap";

export default function ProductTable({products, editClick, delClick, ...params}){
    return(
        <div style={{padding: 10, display: "flex", justifyContent: "center", alignItems: "center"}}>
                <Table style={{backgroundColor: "lightgray", textAlign: "center"}}>
                    <thead>
                        <tr>
                            <th style={{width:250}}>Name</th>
                            <th style={{width:350}}>Description</th>
                            <th style={{width:100}}>Sizes</th>
                            <th style={{width:60}}>Price</th>
                            <th style={{width:60}}>Discount</th>
                            <th style={{width:200}}>ImageLinks</th>
                            <th style={{width: 200}}>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {products.map((item) => (
                        <tr>
                            <td style={{whiteSpace: "normal"}}>{item.name}</td>
                            <td style={{whiteSpace: "normal"}}>{item.description}</td>
                            <td style={{whiteSpace: "normal"}}>
                                {item.paramValues[0].map((item) => (
                                    <p style={{display: "inline-block", marginRight: "5px"}}>{item}</p>
                                ))}
                            </td>
                            <td style={{whiteSpace: "normal"}}>{item.price}</td>
                            <td style={{whiteSpace: "normal"}}>{item.discount}</td>
                            <td style={{whiteSpace: "normal"}}>
                                {item.imageLinks.map((item) => (
                                    <p>{item}</p>
                                ))}
                            </td>
                            <td style={{display: "flex", flexDirection: "column", alignItems: "center",
                                gap: 5}}>
                                <Button style={{width: 100}} variant="primary" onClick={() => editClick(item)}>Edit</Button>{' '}
                                <Button style={{width: 100}} variant="danger" onClick={() => delClick(item)}>Delete</Button>
                            </td>
                        </tr>))}
                    </tbody>
                </Table>
        </div>
    )
}