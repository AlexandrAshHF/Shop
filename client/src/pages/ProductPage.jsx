import React, { useEffect, useState } from "react";
import classes from "./styles/ProductPage.module.css";
import Select from 'react-select';
import { useParams } from "react-router-dom";

export default function ProductPage({...params})
{
    const {productId} = useParams();

    const[product, SetProduct] = useState(null);

    const[currentImg, SetCurrentImg] = useState(0);
    const[selectedSize, SetSelectedSize] = useState(null);
    const[options, SetOptions] = useState();

    const handleSizeChange = (size) => {
        SetSelectedSize(size.value);
    };

    function switchImg(side) {
        if(currentImg + side > product.imageLinks.length - 1)
            SetCurrentImg(0);

        else if(currentImg + side < 0)
            SetCurrentImg(product.imageLinks.length - 1);

        else
        {
            let copy = currentImg + side;
            SetCurrentImg(copy);
        }
    };

    async function fetchProduct() {
        console.log("fetch");
        let response = await fetch(`https://localhost:7265/api/Catalog/GetById?id=${productId}`, {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
            }
        });

        if(response.ok)
        {
            let data = await response.json();
            SetProduct(data);
            SetOptions(data.paramValues[0].map(size => ({ value: size, label: size })));
        }
    }

    function addToBasket(){
        if(selectedSize)
        {
            if(product.number < 1)
            {
                console.log("product number < 1");
                return;
            }

            let basket = localStorage.getItem("basket") 
                ? JSON.parse(localStorage.getItem("basket"))
                : [];

            if(basket.length > 0)
            {
                basket.push({product: product, size: selectedSize});
                localStorage.removeItem("basket");
                localStorage.setItem("basket", JSON.stringify(basket));
            }
            else
            {
                basket.push({product: product, size: selectedSize});
                localStorage.setItem("basket", JSON.stringify(basket));
            }

            window.history.back();
        }
        else
        {
            alert("Select type");
        }
    }

    useEffect(() => {
        console.log("UseEffect");
        fetchProduct();
        //console.log(product.imageLinks);
        if(product)
            console.log(product.paramValues);
    }, []);

    return(
        <div {...params} className={classes.main}>
        {product ? (
            <>
                <div className={classes.imgBlock}>
                    <img alt="product" src={product.imageLinks[currentImg]}/>
                    <div className={classes.switcherImg}>
                        <button onClick={() => switchImg(-1)}>Prev</button>
                        <button onClick={() => switchImg(1)}>Next</button>
                    </div>
                </div>
                <div className={classes.actionBlock}>
                    <label className={classes.name}>{product.name}</label>
                    <div style={{width: "80%"}}>
                        <label className={classes.desc}>{product.description}</label>
                    </div>
                    <div className={classes.priceBlock}>
                        <label className={classes.truePrice}>${product.price}</label>
                        <label className={classes.disc}>- {product.discount}%</label>
                        <label className={classes.price}>${
                            (product.price - (product.price * (product.discount/100)))
                        }
                        </label>
                    </div>
                    {product.number < 1 && (
                        <label>This product is out of stock</label>
                    )}
                    <div className={classes.action}>
                        <button className={classes.bascketBtn} onClick={() => addToBasket()}>
                            Add to bascket
                        </button>
                        <Select options={options} className={classes.sizeLine}
                            onChange={handleSizeChange} value={{value: selectedSize, label: selectedSize}}/>
                    </div>
                </div>
            </>
        ) : (
            <div>Loading...</div>
        )}
    </div>
    )
}