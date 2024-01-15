import React,{useEffect, useState, useRef} from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { Constants } from "../../common/constants";
import TranslatePropertyKeys from '../../../util/translatePropertyKeys';

import { GetContractDataDtosOwnByVendorByVendorId, GetAllVendorDataDtos,GetVendorDataDtoByVendorId, UpdateVendorDataByDto, PostVendorDataByDto } from "../../../service/DataServiceVendor";
import { GeneralButton, TextField, SelectField, SubmitButton } from '../../common/form';

function VendorForm({vendorId, handleSave, handleCancel}){

    const [vendorDto, setVendorDto] = useState("");
    let {idVendor} = useParams();
    const nagivate = useNavigate();
    const [oriVendorDto, setOriVendorDto] = useState("");
    const _blankVendorDto = {
        vendorId:0,
        vendorName:"",
    }

    async function getData(vId){
        //if idVendor isNAN then blank form, else get data from vendorId
        let vendorDto;

        if(vId !="new"){
            console.log(`vId: ${vId}`);
            vendorDto = await GetVendorDataDtoByVendorId(vId);
            
        }else{
            vendorDto =_blankVendorDto;
        }
        vendorDto = await Promise.resolve(TranslatePropertyKeys([vendorDto], "vendor"))
        setOriVendorDto(vendorDto[0]);
        setVendorDto(vendorDto[0])

    } 

    useEffect(()=>{
        const handleKeyDown = (e) =>{
            if(e.key === 'Escape'){
                handleCancel();
            }
        }
        window.addEventListener('keydown', handleKeyDown)
        if(vendorId){
            if(isNaN(vendorId) && vendorId !="new"){
                return null;
            }else{
                getData(vendorId);
            }
            
        }else{
            if(isNaN(idVendor) && idVendor !="new"){
                nagivate('/main/General/');
    
            }else{
                getData(idVendor);
            }
        }

        return ()=>window.removeEventListener('keydown', handleKeyDown);
    },[vendorId])



    const handleOnChange = (e) =>{
        const value = e.target.value;
        const dataKey = e.target.dataset.statekey;
        setVendorDto((prev)=>({
            ...prev, [dataKey]: value,

        }))
    }

    const handleSubmit = (e)=>{
        e.preventDefault();
        if(vendorDto.Id < 1){
            PostVendorDataByDto(vendorDto)
        }else{
            UpdateVendorDataByDto(vendorDto);
        }
        handleSave();
        console.log(`vendorDto submission`);
        console.log(vendorDto);

    }
    const handleReset = ()=>{
        setVendorDto(oriVendorDto);

    }

    const rendorView = ()=>{
        const vendorId = Object.keys(vendorDto)[0];
        const vendorName = Object.keys(vendorDto)[1];

        return(
            <React.Fragment>
                <h2>Vendor Detail</h2>
                <TextField placeHolder = "Example: Lenovo" label = "Vendor Name" onChange = {handleOnChange} statekey = {vendorName} value = {vendorDto.Name} key ={vendorName}/>
                <SubmitButton label = "Save"/> 
                <GeneralButton label = "Reset" handleOnClick = {handleReset}/>

            </React.Fragment>
        )
    }

    return (
        <React.Fragment>
            <form onSubmit = {handleSubmit}>
                {rendorView()}

            </form>
        </React.Fragment>
        //Field: Vendor Name
        //Button: Save
        //Button: Reset

    );

}

export default VendorForm;