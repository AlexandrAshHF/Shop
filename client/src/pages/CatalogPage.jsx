import React, { useEffect, useState } from "react";
import classes from "./styles/CatalogPage.module.css";
import ProductList from "../components/Product/ProductList";
import { useParams, useLocation } from "react-router-dom";
import LayoutUser from "../components/LayoutUser";

export default function CatalogPage({...params})
{
    const {categoryId, typeId} = useParams();
    const[products, SetProducts] = useState([]);
    const[types, SetTypes] = useState([]);
    const[selectedProds, SetSelectedProds] = useState([]);
    const location = useLocation();
    const[selectedType, SetSelectedType] = useState();
    const[searchLine, SetSearch] = useState();

    async function fetchProducts(){
        if(typeId){
            let response = await fetch(`https://localhost:7265/api/Catalog/GetByType?id=${typeId}`, {
                method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                }
            });

            if(response.ok){
                let data = await response.json();
                
                let query = new URLSearchParams(location.search);
                if(query.get("searchLine")){
                    let copy = data.filter(x => {
                        let name = x.name.toUpperCase();
                        return name.includes(query.get("searchLine").toUpperCase());
                    });
                    SetProducts(copy);
                }
                
                else
                    SetProducts(data);
            }
        }

        else if(categoryId){
            let response = await fetch(`https://localhost:7265/api/Catalog/GetByCategory?id=${categoryId}`, {
                method: "GET",
                headers: {
                    'Content-Type': 'application/json',
                }
            });

            if(response.ok){
                let data = await response.json();
                
                let query = new URLSearchParams(location.search);
                if(query.get("searchLine")){
                    let copy = data.filter(x => {
                        let name = x.name.toUpperCase();
                        return name.includes(query.get("searchLine").toUpperCase());
                    });
                    SetProducts(copy);
                }
                
                else
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
                
                let query = new URLSearchParams(location.search);
                if(query.get("searchLine")){
                    let copy = data.filter(x => {
                        let name = x.name.toUpperCase();
                        return name.includes(query.get("searchLine").toUpperCase());
                    });
                    SetProducts(copy);
                }
                
                else
                    SetProducts(data);
            }
        }
    }

    function clickType(item){
        window.location = `/catalog/${item.categoryId}/${item.id}`;
    }

    async function fetchTypes(){
        if(categoryId){
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

        else{
            let response = await fetch(`https://localhost:7265/api/CTP/GetTypes`, {
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
            <LayoutUser/>
            <div className={classes.types}>
                {types.map((item) => (
                    <button className={classes.typeBtn} key={item.id} onClick={() => clickType(item)}>
                        <label>{item.name}</label>
                    </button>
                ))}
            </div>
            <ProductList products={products} selectedList={selectedProds}/>
        </div>
    )
}