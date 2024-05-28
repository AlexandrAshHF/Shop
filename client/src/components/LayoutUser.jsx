import React, { useState } from "react";
import classes from "./LayoutUser.module.css";

export default function LayoutUser({...params}){
    const[searchLine, setSearchLine] = useState("");

    function searchClick(){
        if(searchLine.length > 0)
            window.location = `/catalog?searchLine=${searchLine}`;
    }

    function profileClick(){
        window.location = "/basket";
    }

    function authClick(){
        window.location = "/account/signIn";
    }

    async function adminClick(){
        let key = prompt("Input admin key: ");

        let response = await fetch(`https://localhost:7265/api/Admin/${key}/CheckKey`, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            }
        })

        console.log(response.status);

        if(response.ok)
            window.location = `/admin/${key}/products`;

        else
            alert("Wrong key");
    }

    return(
        <div {...params} className={classes.main}>
            <div>
                <input type="text" onChange={(e) => setSearchLine(e.target.value)}
                className={classes.searchLine} placeholder="Search..."/>
                <button onClick={() => searchClick()} style={{marginRight: 150}}>
                    Search
                </button>
            </div>

            <button style={{marginRight: 10}} onClick={() => profileClick()}>
                Basket
            </button>
            <button style={{marginRight: 10}} onClick={() => authClick()}>
                SignIn
            </button>
            <button onClick={async () => await adminClick()}>
                Admin
            </button>
        </div>
    )
}