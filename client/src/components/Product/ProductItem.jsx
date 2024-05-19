import React from "react";
import classes from "./styles/ProductItem.module.css";

export default function ProductItem({product, isSelected, ...props}){
    const price = product.price - (product.price*(product.discount/100));

    function clickItem(){
        window.location = `/product/${product.id}`;
    }

    return(
        <button className={classes.main} {...props} onClick={() => clickItem()}>
            <img alt="none" src={product.imageLinks[0]} className={classes.img}/>
            <div className={classes.info}>
                <label className={classes.name}>{product.name}</label>
                <div style={{width: "80%", display: "flex", padding: "0", alignItems: "start"}}>
                    <label className={classes.desc}>{product.description}</label>
                </div>
                <div className={classes.priceBlock}>
                    <label className={classes.truePrice}>${product.price}</label>
                    <label className={classes.disc}>- {product.discount}%</label>
                    <label className={classes.price}>${price.toFixed(2)}</label>
                </div>
            </div>
        </button>
    )
}