import React, { useState } from "react";

export default function AdminPage({...params})
{
    const[page, SetPage] = useState();
    return(
        <div {...params}>
            admin
        </div>
    )
}