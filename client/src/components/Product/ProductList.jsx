import React from "react";
import ProductItem from "./ProductItem";
import classes from "./styles/ProductList.module.css";

export default function ProductList({products, selectedList, ...props}){
    return(
        <div className={classes.main} {...props}>
            {products.map((item) => (
                <ProductItem product={item} isSelected={selectedList.findIndex(x => x == item.id) >= 0}/>         
            ))}
        </div>
    )
}