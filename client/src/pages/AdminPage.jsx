import React, { useEffect, useState } from "react";
import OrderTable from "../components/Admin/OrderTable";
import ProductTable from "../components/Admin/ProductTable";
import { useParams } from "react-router-dom";
import classes from "./styles/AdminPage.module.css";
import { Button, Form, Container, Row, Col } from 'react-bootstrap';
import Select from "react-select";
import { type } from "@testing-library/user-event/dist/type";

export default function AdminPage({...params})
{
    const[page, SetPage] = useState(false);
    const {adminKey} = useParams();

    const [products, SetProducts] = useState([]);
    const [orders, SetOrders] = useState([]);
    const [types, SetTypes] = useState([]);

    const options = [
        {value: 'XS', label: 'Size XS'},
        {value: 'S', label: 'Size S'},
        {value: 'M', label: 'Size M'},
        {value: 'L', label: 'Size L'},
        {value: 'XL', label: 'Size XL'},
    ]

    const opthTypes = types.map(item => ({
        value: item.id,
        label: item.name
    }));

    const [selectedType, SetSelectedType] = useState();

    const [selectedOpth, SetSelectedOpth] = useState([]);

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

    async function fetchProducts(){
        let response = await fetch("https://localhost:7265/api/Catalog/GetAll",{
            method: "GET",
            headers:{
                'Content-Type': 'application/json',
            }
        })

        if(response.ok){
            let data = await response.json();
            SetProducts(data);
        }
    }

    async function fetchTypes(){
        let response = await fetch("https://localhost:7265/api/CTP/GetTypes",{
            method: "GET",
            headers:{
                'Content-Type': 'application/json',
            }
        })

        if(response.ok){
            let data = await response.json();
            SetTypes(data);
        }
    }

    async function fetchOrders(){
        let response = await fetch("https://localhost:7265/api/Orders/GetAll",{
            method: "GET",
            headers:{
                'Content-Type': 'application/json',
            }
        })

        if(response.ok){
            let data = await response.json();
            SetOrders(data);
        }
    }

    const[name, SetName] = useState();
    const[description, SetDescription] = useState();
    const[price, SetPrice] = useState();
    const[discount, SetDiscount] = useState();
    const[number, SetNumber] = useState();

    async function createProduct(){
        let formData = new FormData();
        formData.append("Id", "");
        formData.append("Name", name);
        formData.append("Description", description);
        formData.append("Price", price);
        formData.append("Discount", discount);
        formData.append("Number", number);
        // Добавьте файлы изображений в formData
        for (let i = 0; i < images.length; i++) {
            formData.append("ImageLinks", images[i]);
            console.log(images[i]);
        }
        formData.append("TypeId", selectedType.value);
        console.log(selectedType.value);
        // Добавьте значения параметров в formData
        for (let i = 0; i < opthTypes.length; i++){
            console.log(opthTypes[i].value)
            formData.append("ParamValues", opthTypes[i].value);
        }

        console.log(formData.values);
    
        let key = prompt("Input admin key");
        let response = await fetch(`https://localhost:7265/api/Admin/${key}/CreateProduct`, {
            method: "POST",
            body: formData
        })
    
        if(response.ok)
            window.location.reload();
    }

    useEffect(() =>{
        fetchProducts();
        fetchOrders();
        fetchTypes();
    }, [])

    return(
        <div {...params} className={classes.main}>
            <div className={classes.btns}>
                <Button className={!page ? classes.selected : classes.unselected} onClick={() => SetPage(false)}>Products</Button>
                <Button className={page ? classes.selected : classes.unselected} onClick={() => SetPage(true)}>Orders</Button>
            </div>
            {!page && (
                <div>
                    <Container style={{ marginTop: '15px', marginBottom: '15px', marginLeft: '50px', marginRight: '50px' }}>
                        <Row className="justify-content-md-center">
                            <Col xs lg="6">
                            <Form>
                                <Form.Group controlId="formName">
                                <Form.Label>Name</Form.Label>
                                <Form.Control type="text" placeholder="Input name" 
                                    onChange={(e) => SetName(e.target.value)}/>
                                </Form.Group>

                                <Form.Group controlId="formDescription">
                                <Form.Label>Description</Form.Label>
                                <Form.Control type="text" placeholder="Input short description" 
                                    onChange={(e) => SetDescription(e.target.value)}/>
                                </Form.Group>

                                <Form.Group controlId="formPrice">
                                <Form.Label>Price</Form.Label>
                                <Form.Control type="number" placeholder="Input price" 
                                    onChange={(e) => SetPrice(e.target.value)}/>
                                </Form.Group>

                                <Form.Group controlId="formDiscount">
                                <Form.Label>Discount</Form.Label>
                                <Form.Control type="number" placeholder="Input discount" 
                                    onChange={(e) => SetDiscount(e.target.value)}/>
                                </Form.Group>

                                <Form.Group controlId="formNumber">
                                <Form.Label>Amount</Form.Label>
                                <Form.Control type="number" placeholder="Input number" 
                                    onChange={(e) => SetNumber(e.target.value)}/>
                                </Form.Group>

                                <Form.Group controlId="formImages">
                                <Form.Label>Images</Form.Label>
                                <Form.Control type="file" multiple onChange={handleImageUpload} />
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
                                    Create
                                </Button>
                            </Form>
                            </Col>
                        </Row>
                    </Container>
                    <ProductTable products={products}/>
                </div>
            )}
            {page && (
                <OrderTable orders={orders}/>
            )}
        </div>
    )
}