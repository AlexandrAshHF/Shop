import React from "react";
import ProductItem from "../components/Product/ProductItem";

export default function CatalogPage({...params})
{
    let prod = {id: 1, name: "name", description: "descriptiron321312312123132132123123132132 1 2 4 5 6 60234023", price: 12.5, discount: 0.3, 
    imageLinks: ["https://upload.wikimedia.org/wikipedia/en/thumb/e/e2/IMG_Academy_Logo.svg/1200px-IMG_Academy_Logo.svg.png", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFXOOcZnaslyfjPTGV4q_PlLC9Ypmg8kzTgBP5Nrg_FA&s"]}
    
    let prod1 = {id: 1, name: "name", description: "descriptiron321312312123132132123123132132 1 2 4 5 6 60234023", price: 12.5, discount: 0.3, 
    imageLinks: ["https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFXOOcZnaslyfjPTGV4q_PlLC9Ypmg8kzTgBP5Nrg_FA&s", "https://upload.wikimedia.org/wikipedia/en/thumb/e/e2/IMG_Academy_Logo.svg/1200px-IMG_Academy_Logo.svg.png"]}
    
    return(
        <div {...params}>
            <ProductItem product={prod} isSelected={false}/>
        </div>
    )
}