import React from "react";
import classes from "./styles/AuthPage.module.css";
import { useParams } from "react-router-dom";

export default function AuthPage({...params})
{
    const {type} = useParams();

    return(
        <>
            {type == "signIn" &&
                <div className={classes.main}>
                    <label className={classes.mainLabel}>SignIn</label>
                    <div className={classes.inputs}>
                        <div className={classes.inputBlock}>
                            <label>Email</label>
                            <input type="email"/>
                        </div>
                        <div className={classes.inputBlock}>
                            <label>Password</label>
                            <input type="password"/>
                        </div>
                    </div>
                    <div className={classes.actionBlock}>
                        <button>
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
                            <input type="email"/>
                        </div>
                        <div className={classes.inputBlock}>
                            <label>Password</label>
                            <input type="password"/>
                        </div>
                        <div className={classes.inputBlock}>
                            <label>Password</label>
                            <input type="password"/>
                        </div>
                    </form>
                    <div className={classes.actionBlock}>
                        <button>
                            <label>SignUp</label>
                        </button>
                        <a href="http://localhost:3000/account/signIn">Already have an account?</a>
                    </div>
                </div>
            }
        </>
    );
}