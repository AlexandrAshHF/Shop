import React from "react";
import classes from "./LayoutUser.module.css";

export default function LayoutUser({setSearchLine, ...params}){
    return(
        <div {...params} className={classes.main}>
            <div>
                <input type="text" onChange={(e) => setSearchLine(e.target.value)}
                className={classes.searchLine} placeholder="Search..."/>
                <button>
                    Search
                </button>
            </div>
            <button>

            </button>
            <button>

            </button>
        </div>
    )
}