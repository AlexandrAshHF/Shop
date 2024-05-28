import React, { useState } from "react";
import classes from "./styles/AuthPage.module.css";

export default function AuthCode({...params}){
    const code = localStorage.getItem("code");
    const[inputCode, SetCode] = useState();

    async function Confirm(){
        console.log(inputCode)
        if(code == inputCode){
            let response = await fetch("https://localhost:7265/api/Accounts/Confirmation", {
                method: "POST",
                headers:{
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(inputCode)
            });

            if(response.ok)
                window.location = '/account/signIn';

            else{
                console.log(await response.json())
            }
        }

        else{
            alert("Incorrect code");
        }
    }

    return(
        <div {...params} className={classes.main} style={{marginTop: 100}}>
            <div className={classes.inputs}>
                <div className={classes.inputBlock}>
                    <label>Code</label>
                    <input type="number" onChange={(e) => SetCode(e.target.value)}/>
                </div>
            </div>
            <div className={classes.actionBlock}>
                <button onClick={async () => await Confirm()}>
                    <label>Check</label>
                </button>
                <a href="http://localhost:3000/account/signUp">Don't have an account?</a>
            </div>
        </div>
    )
}