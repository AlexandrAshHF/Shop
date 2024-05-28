import {React, useState} from "react";
import classes from "./styles/OrderModal.module.css";
import { Button } from 'react-bootstrap';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';

export default function OrderModal({closeWindow, ...props}){

    const [startDate, setStartDate] = useState(new Date());
    const [endDate, setEndDate] = useState(new Date());

    async function downloadExcel(){
        console.log(startDate);
        console.log(endDate);
        await fetch('https://localhost:7265/api/Orders/DownloadExcel?' + new URLSearchParams({
            leftBorder: startDate.toISOString(),
            rightBorder: endDate.toISOString()
        }), 
        {
            method: 'GET',
            headers: {
                'Accept': 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
            }
        })
        .then(response => response.blob())
        .then(blob => {
            const url = window.URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.style.display = 'none';
            a.href = url;
            a.download = 'Report.xlsx';
            document.body.appendChild(a);
            a.click();
            document.body.removeChild(a);
        });

        closeWindow();
    }

    return(
        <div className={classes.main} {...props}>
            <Button style={{margin: "auto"}} onClick={() => closeWindow()}>Close</Button>
            <div className={classes.inner}>
                <div className={classes.pickerBlock}>
                    <label>From: </label>
                    <DatePicker selected={startDate} onChange={(date) => setStartDate(date)} />
                    <label>To:</label>
                    <DatePicker selected={endDate} onChange={(date) => setEndDate(date)} />
                </div>
                <Button onClick={async () => downloadExcel()}>Download</Button>
            </div>
        </div>
    )
}