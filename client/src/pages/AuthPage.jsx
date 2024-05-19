import React, { useState } from "react";
import classes from "./styles/AuthPage.module.css";
import { useParams } from "react-router-dom";
import LayoutUser from "../components/LayoutUser";

export default function AuthPage({...params})
{
    const {type} = useParams();

    const [email, SetEmail] = useState();
    const [password, SetPassword] = useState();
    const [secondPassword, SetSecondPassword] = useState();

    const [error, SetError] = useState("");

    async function LogIn(){
        let response = await fetch("https://localhost:7265/api/Accounts/SignIn", {
            method: "POST",
            headers:{
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({"email": email, "password": password})
        });

        if(response.ok){
            let data = await response.text();
            localStorage.setItem("auth", data);

            window.location = "/category";
        }
        else{
            let data = await response.json();
            alert(data);
            SetError(data);
        }
    }

    async function Register(){
        
        if(secondPassword != password){
            SetError("Password mismatch");
            return;
        }

        let response = await fetch("https://localhost:7265/api/Accounts/SignUp", {
            method: "POST",
            headers:{
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({"email": email, "password": password})
        });

        if(response.ok){
            window.location = "/account/signIn";
        }
        else{
            let data = await response.json();
            alert(data);
            SetError(data);
        }
    }

    return(
        <>
            <LayoutUser style={{marginBottom: 70}}/>
            {type == "signIn" &&
                <div className={classes.main}>
                    <label className={classes.mainLabel}>SignIn</label>
                    <div className={classes.inputs}>
                        <div className={classes.inputBlock}>
                            <label>Email</label>
                            <input type="email" onChange={(e) => SetEmail(e.target.value)}/>
                        </div>
                        <div className={classes.inputBlock}>
                            <label>Password</label>
                            <input type="password" onChange={(e) => SetPassword(e.target.value)}/>
                        </div>
                    </div>
                    <label style={{color: "red", fontSize: "small", margin: "5px auto"}}>{error}</label>
                    <div className={classes.actionBlock}>
                        <button onClick={() => LogIn()}>
                            <label>SignIn</label>
                        </button>
                        <a href="http://localhost:3000/account/signUp">Don't have an account?</a>
                    </div>
                </div>
            }
            {type == "signUp" &&
                <div className={classes.main}>
                    <label className={classes.mainLabel}>SignUp</label>
                    <form className={classes.inputs}>
                        <div className={classes.inputBlock}>
                            <label>Email</label>
                            <input type="email" onChange={(e) => SetEmail(e.target.value)}/>
                        </div>
                        <div className={classes.inputBlock}>
                            <label>Password</label>
                            <input type="password" onChange={(e) => SetSecondPassword(e.target.value)}/>
                        </div>
                        <div className={classes.inputBlock}>
                            <label>Second Password</label>
                            <input type="password" onChange={(e) => SetPassword(e.target.value)}/>
                        </div>
                    </form>
                    <label style={{color: "red", fontSize: "small", margin: "5px auto"}}>{error}</label>
                    <div className={classes.actionBlock}>
                        <button onClick={() => Register()}>
                            <label>SignUp</label>
                        </button>
                        <a href="http://localhost:3000/account/signIn">Already have an account?</a>
                    </div>
                </div>
            }
        </>
    );
}