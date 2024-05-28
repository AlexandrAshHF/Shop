import React, { useEffect, useState } from "react";
import classes from "./styles/ModalProduct.module.css";
import { Button, Form, Container, Row, Col, Table } from 'react-bootstrap';
import Select from "react-select";

export default function ModalProduct({product, types, adminKey, closeWindow, ...props}){
    const options = [
        {value: '36', label: '36'},
        {value: '38', label: '38'},
        {value: '40', label: '40'},
        {value: '42', label: '42'},
        {value: '44', label: '44'},
    ]

    const opthTypes = types.map(item => ({
        value: item.id,
        label: item.name
    }));

    const [selectedType, SetSelectedType] = useState(
        product 
        ? opthTypes.find(opt => opt.value == product.typeId)
        : null
    );

    const [selectedOpth, SetSelectedOpth] = useState(
        product
        ? options.filter(option => product.paramValues[0].includes(option.value))
        : []
    );

    const [images, setImages] = useState([]);

    const handleImageUpload = event => {
        setImages([...images, ...event.target.files]);
    };

    const handleChange = (selectedOpth) => {
        SetSelectedOpth(selectedOpth);
        console.log(`Option selected:`, selectedOpth);
    };

    const handleChangeType = (selectedType) => {
        SetSelectedType(selectedType);
        console.log(selectedType);
    }
    console.log(product);
    const[name, SetName] = useState(product ? product.name : "");
    const[description, SetDescription] = useState(product ? product.description : "");
    const[price, SetPrice] = useState(product ? product.price : 0);
    const[discount, SetDiscount] = useState(product ? product.price : 0);
    const[number, SetNumber] = useState(product ? product.price : null);
    const[imgLinks, SetImgLinks] = useState(
        product 
        ? product.imageLinks
        : []
    );

    async function createProduct(){
        let formData = new FormData();
        formData.append("Id", product ? product.id : "");
        formData.append("Name", name);
        formData.append("Description", description);
        formData.append("Price", price);
        formData.append("Discount", discount);
        formData.append("Number", number);

        for (let i = 0; i < images.length; i++) {
            formData.append("Images", images[i]);
            console.log(images[i]);
        }

        for (let i = 0; i < imgLinks.length; i++) {
            formData.append("ImageLinks", imgLinks[i]);
        }

        formData.append("TypeId", selectedType.value);

        for (let i = 0; i < selectedOpth.length; i++){
            console.log(opthTypes[i])
            formData.append("ParamValues", selectedOpth[i].value);
        }

        console.log(formData.values);

        console.log(`Send values: \n 
            Product:${product}\n
            Name: ${name}\n
            Description: ${description}\n
            Price: ${price}
            Discount: ${discount}
            Amount: ${number}
            Images: ${images}
            ImageLinks: ${imgLinks}
            Sizes: ${selectedOpth}
            Type: ${selectedType}`)

        let Url = product ? "UpdateProduct" : "CreateProduct";
        let methodUrl = product ? "PATCH" : "POST";

        let response = await fetch(`https://localhost:7265/api/Admin/${adminKey}/${Url}`, {
            method: methodUrl,
            body: formData
        })
    
        if(response.ok)
            window.location.reload();
    }

    function delImgLink(link){
        const newLinks = imgLinks.filter(x => x != link);
        SetImgLinks(newLinks);
    }

    return(
        <div className={classes.main} {...props}>        
            <div className={classes.inner}>
                <Button style={{margin: "auto"}} onClick={() => closeWindow()}>Close</Button>
                <Table className={classes.tableItem} style={{backgroundColor: "lightgray", textAlign: "center",
                width: 500, margin:"10px auto"}}>
                        <thead>
                            <tr>
                                <th style={{width: 400}}>ImageLinks</th>
                                <th style={{width: 100}}></th>
                            </tr>
                        </thead>
                        <tbody>
                            { imgLinks.map((item) => (
                                <tr>
                                    <td>{item}</td>
                                    <td>
                                        <Button variant="danger" onClick={() => delImgLink(item)}>Delete</Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                </Table>  
                <Container style={{ marginTop: '15px', marginBottom: '15px', marginLeft: '50px', marginRight: '50px' }}>
                    <Row className="justify-content-md-center">
                        <Col xs lg="6">
                        <Form>
                            <Form.Group controlId="formName">
                                <Form.Label>Name</Form.Label>
                                <Form.Control type="text" placeholder="Input name" 
                                    onChange={(e) => SetName(e.target.value)} value={name}/>
                            </Form.Group>

                            <Form.Group controlId="formDescription">
                                <Form.Label>Description</Form.Label>
                                <Form.Control type="text" placeholder="Input short description" 
                                    onChange={(e) => SetDescription(e.target.value)} value={description}/>
                            </Form.Group>

                            <Form.Group controlId="formPrice">
                                <Form.Label>Price</Form.Label>
                                <Form.Control type="number" placeholder="Input price" 
                                    onChange={(e) => SetPrice(e.target.value)} value={price}/>
                            </Form.Group>

                            <Form.Group controlId="formDiscount">
                                <Form.Label>Discount</Form.Label>
                                <Form.Control type="number" placeholder="Input discount" 
                                    onChange={(e) => SetDiscount(e.target.value)} value={discount}/>
                            </Form.Group>

                            <Form.Group controlId="formNumber">
                                <Form.Label>Amount</Form.Label>
                                <Form.Control type="number" placeholder="Input number" 
                                    onChange={(e) => SetNumber(e.target.value)} value={number}/>
                            </Form.Group>

                            <Form.Group controlId="formImages">
                                <Form.Label>Images</Form.Label>
                                <Form.Control type="file" multiple onChange={handleImageUpload}/>
                            </Form.Group>

                            <Form.Group controlId="formSelect">
                                <Form.Label>Sizes</Form.Label>
                                <Select
                                    isMulti
                                    options={options}
                                    className="basic-multi-select"
                                    classNamePrefix="select"
                                    onChange={handleChange}
                                    value={selectedOpth}
                                />
                            </Form.Group>

                            <Form.Group controlId="formSelect">
                                <Form.Label>Type</Form.Label>
                                <Select
                                    options={opthTypes}
                                    onChange={handleChangeType}
                                    value={selectedType}
                                />
                            </Form.Group>

                            <Button variant="primary" style={{margin: "10px 0px"}} onClick={async () => await createProduct()}>
                                {product ? "Update" : "Create"}
                            </Button>
                        </Form>
                        </Col>
                    </Row>
                </Container>
            </div>
        </div>
    )
}