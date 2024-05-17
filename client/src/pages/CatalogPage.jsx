import React, { useEffect, useState } from "react";
import classes from "./styles/CatalogPage.module.css";
import ProductList from "../components/Product/ProductList";
import { useParams } from "react-router-dom";

export default function CatalogPage({...params})
{
    const {categoryId, typeId} = useParams();
    const[products, SetProducts] = useState([]);
    const[types, SetTypes] = useState([]);
    const[selectedProds, SetSelectedProds] = useState([]);
    const[selectedType, SetSelectedType] = useState();
    const[searchLine, SetSearch] = useState();

    async function fetchProducts(){
        if(typeId !== null && typeId !== undefined){
            let response = await fetch(`https://localhost:7265/api/Catalog/GetByType?id=${typeId}`, {
                method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                }
            });

            if(response.ok){
                let data = await response.json();
                SetProducts(data);
            }
        }

        else if(categoryId !== null && categoryId !== undefined){
            let response = await fetch(`https://localhost:7265/api/Catalog/GetByCategory?id=${categoryId}`, {
                method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                }
            });

            if(response.ok){
                let data = await response.json();
                SetProducts(data);
            }
        }

        else{
            let response = await fetch(`https://localhost:7265/api/Catalog/GetAll`, {
                method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                }
            });

            if(response.ok){
                let data = await response.json();
                SetProducts(data);
            }
        }
    }

    async function fetchTypes(){
        if(categoryId !== null && categoryId !== undefined){
            let response = await fetch(`https://localhost:7265/api/CTP/GetTypesByCId?id=${categoryId}`, {
                method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                }
            });

            if(response.ok){
                let data = await response.json();
                SetTypes(data);
            }
        }
    }

    useEffect(() => {
        fetchProducts();
        fetchTypes();
    }, [])

    return(
        <div {...params} className={classes.main}>
            <div className={classes.types}>
                {types.map((item) => (
                    <button>
                        <label>{item.name}</label>
                    </button>
                ))}
            </div>
            <ProductList products={products} selectedList={selectedProds}/>
        </div>
    )
}