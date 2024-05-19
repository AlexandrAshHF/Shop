import React, { useState } from "react";
import LayoutUser from "../components/LayoutUser";

export default function ProfilePage({...params})
{
    const[currentOrder, SetCurrentOrder] = useState();
    const[orders, SetOrders] = useState();
    
    return(
        <div {...params}>
            <LayoutUser/>
            profile
        </div>
    )
}