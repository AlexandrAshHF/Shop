import React from "react";
import classes from "./styles/FullOrder.module.css";
import { useParams } from "react-router-dom";

export default function FullOrderPage({...params}){
    const {orderId} = useParams();


    return(
        <div className={classes.main}>
            
        </div>
    )
}