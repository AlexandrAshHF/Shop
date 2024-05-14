import React from "react";
import classes from "./styles/ProductItem.module.css";

export default function ProductItem({product, isSelected, ...props}){
    return(
        <div className={classes.main}>
            {/* <img alt="none" src=""/> */}
            <div className={classes.img}>

            </div>
            <div className={classes.info}>

            </div>
        </div>
    )
}