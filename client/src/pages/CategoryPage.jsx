import React, { useState } from "react";
import classes from "./styles/CategoryPage.module.css"

export default function CategoryPage({...params})
{
    const [categories, setCategories] = useState([
        {id: 1, name: "name1", imageLink: "image"},
        {id: 1, name: "name1", imageLink: "image"},
        {id: 1, name: "name1", imageLink: "image"},
        {id: 1, name: "name1", imageLink: "image"}
    ]);

    return(
        <div {...params} className={classes.main}>
            {categories.map((item) => (
                <div key={item.id} className={classes.item}>
                    <img alt="category" src={item.imageLink} className={classes.img}/>
                    <label>{item.name}</label>
                </div>
            ))}
        </div>
    )
}