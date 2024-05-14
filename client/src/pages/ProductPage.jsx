import React, { useState } from "react";
import classes from "./styles/ProductPage.module.css";
import Select from 'react-select';

export default function ProductPage({...params})
{
    const[type, SetType] = useState();
    const[product, SetProduct] = useState({id: 1, name: "name", description: "descriptiron321312312123132132123123132132 1 2 4 5 6 60234023", price: 12.5, discount: 0.3, 
    imageLinks: ["https://upload.wikimedia.org/wikipedia/en/thumb/e/e2/IMG_Academy_Logo.svg/1200px-IMG_Academy_Logo.svg.png", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFXOOcZnaslyfjPTGV4q_PlLC9Ypmg8kzTgBP5Nrg_FA&s"]});
    const[parameter, SetParameters] = useState([]);
    const[currentImg, SetCurrentImg] = useState();
    const[selectSize, SetSize] = useState();
    const price = 0;

    function switchImg(side) {
        if(currentImg + side > product.imageLinks.length)
            SetCurrentImg(0);

        else if(currentImg + side < 0)
            SetCurrentImg(product.imageLinks.length - 1);

        else
        {
            let copy = currentImg + side;
            SetCurrentImg(copy);
        }
    }

    return(
        <div {...params} className={classes.main}>
            <div className={classes.imgBlock}>
                <img alt="product" src={product.imageLinks[currentImg]}/>
                <div className={classes.switcherImg}>
                    <button onClick={() => switchImg(1)}>Next</button>
                    <button onClick={() => switchImg(-1)}>Prev</button>
                </div>
            </div>
            <div className={classes.actionBlock}>
                <label className={classes.name}>{product.name}</label>
                <div style={{width: "80%"}}>
                    <label className={classes.desc}>{product.description}</label>
                </div>
                <div className={classes.priceBlock}>
                    <label className={classes.truePrice}>${product.price}</label>
                    <label className={classes.disc}>- {product.discount*100}%</label>
                    <label className={classes.price}>${price.toFixed(2)}</label>
                </div>
                <div className={classes.action}>
                    <button className={classes.bascketBtn}>
                        Add to bascket
                    </button>
                    <Select options={parameter} className={classes.sizeLine}/>
                </div>
            </div>
            <div className={classes.paramsBlock}>
                
            </div>
        </div>
    )
}