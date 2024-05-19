import React, { useEffect, useState } from "react";
import classes from "./styles/CategoryPage.module.css"
import LayoutUser from "../components/LayoutUser";

export default function CategoryPage({...params})
{
    const [categories, setCategories] = useState([]);

    async function fetchCategories(){
        let response = await fetch("https://localhost:7265/api/CTP/GetCategories", {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
            }
        });

        if(response.ok){
            let data = await response.json();
            setCategories(data);
        }
    }

    function clickBlock(item){
        window.location = `/catalog/${item.id}`;
    }

    useEffect(() => {
        fetchCategories();
    }, []);

    return(
        <div {...params} className={classes.main}>
            <LayoutUser style={{marginBottom: 0}}/>
            {categories.map((item) => (
                <button key={item.id} className={classes.item} onClick={() => clickBlock(item)}>
                    <img alt="category" src={item.imageLink} className={classes.img}/>
                    <label>{item.name}</label>
                </button>
            ))}
        </div>
    )
}