import React, { useEffect, useState } from "react";

export default function CTPTable({...props}) {

    const[types, SetTypes] = useState([]);
    const[parameters, SetParameters] = useState([]);
    const[categories, SetCategories] = useState([]);

    const [typeVMs, setTypeVMs] = useState([]);
    const [categoryVMs, setCategoryVMs] = useState([]);
    const [paramVMs, setParamVMs] = useState([]);

    async function fetchTypes(){
        let response = await fetch("https://localhost:7265/api/CTP/GetTypes", {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
            }
        });

        if(response.ok)
            SetTypes(await response.json());
        
        else
            console.log(await response.json());
    }

    async function fetchParameters(){
        let response = await fetch("https://localhost:7265/api/CTP/GetParameters", {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
            }
        });

        if(response.ok)
            SetParameters(await response.json());
        
        else
            console.log(await response.json());
    }

    async function fetchCategories(){
        let response = await fetch("https://localhost:7265/api/CTP/GetCategories", {
            method: "GET",
            headers: {
                'Content-Type': 'application/json',
            }
        });

        if(response.ok)
            SetCategories(await response.json());
        
        else
            console.log(await response.json());
    }

    useEffect(() => {
        fetchTypes();
        fetchCategories();
        fetchParameters();

        let tVM = {id: "", name: "", paramsName: [], categoryName: ""};
        let arrT = [];
        for (let i = 0; i < types.length; i++) {
            tVM.id = types[i].id;
            tVM.name = types[i].name;
            
            const catArr = categories.find(x => x.typesId.includes(types[i].id));
            console.log(catArr);
            tVM.categoryName = catArr.name;

            const paramsArr = parameters.filter(x => x.typesId.includes(types[i].id));
            console.log(paramsArr);
            tVM.paramsName = paramsArr.map(x => x.name);

            arrT.push(tVM);
        }
        setTypeVMs(arrT);

        let cVM = {id: "", name: "", typesName: []};
        let arrC = []
        for (let i = 0; i < categories.length; i++) {
            cVM.id = categories[i].id;
            cVM.name = categories[i].name;
            
            const typeArr = types.filter(x => categories[i].typesId.includes(x.id));
            console.log(typeArr);
            cVM.typesName = typeArr.map(x => x.name);
            arrC.push(cVM);
        }
        setCategoryVMs(arrC);

        let pVM = {id: "", name: "", allowValues: [], typesName: []};
        let arrP = [];
        for (let i = 0; i < parameters.length; i++) {
            pVM.id = parameters[i].id;
            pVM.name = parameters[i].name;
            pVM.allowValues = parameters[i].allowValues;

            let typeArr = types.filter(x => parameters[i].typesId.includes(x.id));
            console.log(typeArr);
            pVM.typesName = typeArr.map(x => x.name);
            arrP.push(pVM);
        }
        setParamVMs(arrP);

    }, []);

    return(
        <div style={{padding: 10, display: "flex", justifyContent: "center", alignItems: "center", textAlign:"center"}}>
                <label>Types</label>
                <Table style={{backgroundColor: "lightgray", textAlign: "center"}}>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Caregories</th>
                            <th>Parameters</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {typeVMs.map((item) => (
                            <tr>
                                <td>{item.name}</td>
                                <td>{item.categoryName}</td>
                                <td>
                                    {item.paramsName.map((p) => (
                                        <p style={{display: "inline-block", marginRight: "5px"}}>{p}</p>
                                    ))}
                                </td>
                                <td style={{display: "flex", flexDirection: "column", alignItems: "center",
                                gap: 5}}>
                                    <Button style={{width: 100}} variant="primary">Edit</Button>{' '}
                                    <Button style={{width: 100}} variant="danger">Delete</Button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
                <label>Categories</label>
                <Table style={{backgroundColor: "lightgray", textAlign: "center"}}>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Types</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {typeVMs.map((item) => (
                            <tr>
                                <td>{item.name}</td>
                                <td>{item.categoryName}</td>
                                <td>
                                    {item.paramsName.map((p) => (
                                        <p style={{display: "inline-block", marginRight: "5px"}}>{p}</p>
                                    ))}
                                </td>
                                <td style={{display: "flex", flexDirection: "column", alignItems: "center",
                                gap: 5}}>
                                    <Button style={{width: 100}} variant="primary">Edit</Button>{' '}
                                    <Button style={{width: 100}} variant="danger">Delete</Button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
        </div>
    );  
}