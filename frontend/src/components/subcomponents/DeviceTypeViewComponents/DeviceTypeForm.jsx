import React,{useEffect, useState, useRef} from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { Constants } from "../../common/constants";
import TranslatePropertyKeys from '../../../util/translatePropertyKeys';

import { GetContractDataDtosOwnByVendorByVendorId, GetAllVendorDataDtos,GetVendorDataDtoByVendorId, UpdateVendorDataByDto, PostVendorDataByDto } from "../../../service/DataServiceVendor";
import { GeneralButton, TextField, SelectField, SubmitButton } from '../../common/form';
import { GetDeviceTypeDtoByDeviceTypeId, UpdateDeviceTypeDto, PostDeviceTypeDto } from "../../../service/DataServiceDeviceType";

function DeviceTypeForm({deviceTypeId, handleSave, handleCancel}){

    const [deviceTypeDto, setDeviceTypeDto] = useState("");
    let {idDeviceType} = useParams();
    const nagivate = useNavigate();
    const [oriDeviceTypeDto, setOriDeviceTypeDto] = useState("");
    const _blankDeviceTypeDto = {
        deviceTypeId:0,
        deviceTypeName:"",
    }

    async function getData(vId){
        //if idVendor isNAN then blank form, else get data from vendorId
        let deviceTypeDto;

        if(vId !="new"){
            console.log(`vId: ${vId}`);
            deviceTypeDto = await GetDeviceTypeDtoByDeviceTypeId(vId);
            
        }else{
            deviceTypeDto =_blankDeviceTypeDto;
        }
        deviceTypeDto = await Promise.resolve(TranslatePropertyKeys([deviceTypeDto], "deviceType"))
        setOriDeviceTypeDto(deviceTypeDto[0]);
        setDeviceTypeDto(deviceTypeDto[0])

    } 

    useEffect(()=>{
        const handleKeyDown = (e) =>{
            if(e.key === 'Escape'){
                handleCancel();
            }
        }
        window.addEventListener('keydown', handleKeyDown)
        if(deviceTypeId){
            if(isNaN(deviceTypeId) && deviceTypeId !="new"){
                return null;
            }else{
                getData(deviceTypeId);
            }
            
        }else{
            if(isNaN(idDeviceType) && idDeviceType !="new"){
                console.log(`navigate happening`)
                nagivate('/main/General/');
    
            }else{
                getData(idDeviceType);
            }
        }

        return ()=>window.removeEventListener('keydown', handleKeyDown);
    },[deviceTypeId])



    const handleOnChange = (e) =>{
        const value = e.target.value;
        const dataKey = e.target.dataset.statekey;
        setDeviceTypeDto((prev)=>({
            ...prev, [dataKey]: value,

        }))
    }

    const handleSubmit = (e)=>{
        e.preventDefault();
        if(deviceTypeDto.Id < 1){
            console.log(`PostDeviceTypeDto(deviceTypeDto)`)
            console.log(deviceTypeDto);
            PostDeviceTypeDto(deviceTypeDto)
        }else{
            console.log(`UpdateDeviceTypeDto(deviceTypeDto)`)
            console.log(deviceTypeDto);
            UpdateDeviceTypeDto(deviceTypeDto);
        }
        handleSave();
        console.log(`DeviceType submission`);
        console.log(deviceTypeDto);

    }
    const handleReset = ()=>{
        setDeviceTypeDto(oriDeviceTypeDto);

    }

    const rendorView = ()=>{
        const vendorId = Object.keys(deviceTypeDto)[0];
        const vendorName = Object.keys(deviceTypeDto)[1];

        return(
            <React.Fragment>
                <h2>Vendor Detail</h2>
                <TextField placeHolder = "Example: Laptop" label = "DeviceType Name" onChange = {handleOnChange} statekey = {vendorName} value = {deviceTypeDto.Name} key ={vendorName}/>
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

export default DeviceTypeForm;