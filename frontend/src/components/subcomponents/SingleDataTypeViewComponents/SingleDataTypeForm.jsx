import React,{useEffect, useState, useRef} from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { Constants } from "../../common/constants";
import TranslatePropertyKeys from '../../../util/translatePropertyKeys';

import { GeneralButton, TextField, SelectField, SubmitButton } from '../../common/form';

function SingleDataTypeForm({label, translatePrefixString, objectId, handleSave, handleCancel, apiGetDtoById, apiPostDto, apiUpdateDto}){

    const [objectDto, setObjectDto] = useState("");
    let {idDeviceType} = useParams();
    const nagivate = useNavigate();
    const [oriObjectDto, setOriObjectDto] = useState("");
    const _blankDeviceTypeDto = {
        [translatePrefixString+"Id"]: 0,
        [translatePrefixString+"Name"]: "",
    }

    async function getData(vId){
        //if idVendor isNAN then blank form, else get data from vendorId
        let _objectDto;

        if(vId !="new"){
            console.log(`vId: ${vId}`);
            _objectDto = await apiGetDtoById(vId);
            
        }else{
            _objectDto =_blankDeviceTypeDto;
        }
        _objectDto = await Promise.resolve(TranslatePropertyKeys([_objectDto], translatePrefixString))
        setOriObjectDto(_objectDto[0]);
        setObjectDto(_objectDto[0])
        console.log(`_objectDto`)
        console.log(_objectDto)

    } 

    useEffect(()=>{
        const handleKeyDown = (e) =>{
            if(e.key === 'Escape'){
                handleCancel();
            }
        }
        window.addEventListener('keydown', handleKeyDown)
        if(objectId){
            if(isNaN(objectId) && objectId !="new"){
                return null;
            }else{
                getData(objectId);
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
    },[objectId])



    const handleOnChange = (e) =>{
        const value = e.target.value;
        console.log(`HandleOnChange`);
        console.log(value);
        const dataKey = e.target.dataset.statekey;
        console.log(`dataKey`)
        console.log(dataKey)
        setObjectDto((prev)=>({
            ...prev, [dataKey]: value,

        }))
    }

    const handleSubmit = (e)=>{
        e.preventDefault();
        if(objectDto.Id < 1){
            console.log(`PostDeviceTypeDto(deviceTypeDto)`)
            console.log(objectDto);
            apiPostDto(objectDto)
        }else{
            console.log(`UpdateDeviceTypeDto(deviceTypeDto)`)
            console.log(objectDto);
            apiUpdateDto(objectDto);
        }
        handleSave();
        console.log(`DeviceType submission`);
        console.log(objectDto);

    }
    const handleReset = ()=>{
        setObjectDto(oriObjectDto);

    }

    const rendorView = ()=>{
        const typeId = "Id";
        const typeName = "Name";

        return(
            <React.Fragment>
                <h2>{label} Detail</h2>
                <TextField placeHolder = "Example: Laptop" label = {`${label} Name`} onChange = {handleOnChange} statekey = {typeName} value = {objectDto.Name} key ={typeName}/>
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

export default SingleDataTypeForm;