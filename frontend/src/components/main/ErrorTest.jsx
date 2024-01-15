import React,{useEffect, useState, useRef} from "react";
import { ErrorTestDeleteAPI } from "../../service/DataServiceErrorTest";
import { toast } from 'react-toastify';
import {useToasts } from 'react-toast-notifications'

export function ErrorTest(){
    const {addToast} =  useToasts();
    const [errorDetails, setErrorDetails] = useState();
    const [textField, setTextField] = useState();

    const toastNotify = (err)=>{
        toast(err);
    }


    const handleOnClick = async(e)=>{
        let result = ""
        console.log(`handleOnClick`)
        const value = e.target.value;
        try{
            result = await ErrorTestDeleteAPI(textField);
            setErrorDetails("success")

        }catch(err){
            console.log('catched error')
            console.log(err)
            
            if(err.status == 405){
                console.log('405 branch');
                console.log(err.data.title ?? "405a")
                addToast(err.data.title ?? "405a", {appearance : 'error'})
                setErrorDetails(err.data.title ?? "405a")
            }else if(err.status == 404){
                console.log('404 branch');
                console.log(err.data ?? 404)
                addToast(err.data ?? "404a", {appearance : 'error'})
                setErrorDetails(err.data ?? 404)
            }else{
                //console.log(err)
                console.log('else branch');
                addToast(err.data.title ?? "error", {appearance : 'error'})
                setErrorDetails(err.data.title)
            }
        }

    }

    const handleOnChangeField = (e)=>{
        const value = e.target.value;
        setTextField(value);

    }

    return(
        <>
        <h1>ErrorTestDelete API </h1>
        <input type = 'text' onChange = {(e) => handleOnChangeField(e)}/>
        <button className="btn btn-primary" type="button" onClick={(e)=>handleOnClick(e)} >Test Error API</button>
        <br/>
        <span>{errorDetails}</span>
        </>
    )
}