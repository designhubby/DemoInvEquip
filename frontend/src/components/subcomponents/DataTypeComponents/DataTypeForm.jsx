import React,{useEffect, useState, useRef} from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { Constants } from "../../common/constants";
import Joi from 'joi';
import TranslatePropertyKeys from '../../../util/translatePropertyKeys';

import {useToasts } from 'react-toast-notifications'
import { GeneralButton, TextField, SelectField, SubmitButton } from '../../common/form';

function DataTypeForm({label, translatePrefixString, objectId, handleSave, handleCancel, apiGetDtoById, apiPostDto, apiUpdateDto, joiValidationScheme,  children}){

    const {addToast} =  useToasts();
    const [objectDto, setObjectDto] = useState("");
    let {idDeviceType} = useParams();
    const nagivate = useNavigate();
    const [oriObjectDto, setOriObjectDto] = useState("");
    const _blankDeviceTypeDto = {
        [translatePrefixString+"Id"]: 0,
        [translatePrefixString+"Name"]: "",
    }

    async function getData(objId){
        //if idVendor isNAN then blank form, else get data from vendorId
        let _objectDto;

        if(objId !="new"){
            console.log(`vId: ${objId}`);
            _objectDto = await apiGetDtoById(objId);
            
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
                handleReset();
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

        return ()=>{
            window.removeEventListener('keydown', handleKeyDown)
            
        };
    },[objectId])



    const handleOnChange = (e, optionDto) =>{
        console.log(`HandleOnChange`);
        console.log(e);
        //optionDto.[labelkey]
        const value = e.target.value;
        const dataKey = e.target.dataset.statekey;
        
        const labelKeys = optionDto && Object.keys(optionDto);

        const labelValue = labelKeys && optionDto["Name"];
        console.log(`optionDto`)
        console.log(optionDto)
        console.log(`dataKey`)
        console.log(dataKey)
        console.log(`labelValue`)
        console.log(labelValue)
        let tempState = {
            ...objectDto,[dataKey]: value, //add:need to update the name not just id
        }
        console.log(`tempState1`)
        console.log(tempState)
        if(labelKeys){
            const cutoff = dataKey.indexOf("Id");
            const keyname = dataKey.substring(0, cutoff);
            console.log(`keyname`)
            console.log(keyname)
            tempState = {
                ...tempState, [keyname+"Name"]: labelValue,
            }
        }
        console.log(`tempState2`)
        console.log(tempState)
        setObjectDto(tempState)
    }

    const [errorList, setErrorList] = useState({});
    const validateFormSuccess = ()=>{
        if(joiValidationScheme){
            console.log(`joiValidationScheme Exists Branch`)
            const validationScheme = joiValidationScheme;
            const validationResult = validationScheme.validate(objectDto,{abortEarly : false});
            console.log(`validationResult`)
            console.log(validationResult)
            let _errorList = null;
            if(validationResult.error){
                console.log(`validationResult.error branch`)
                validationResult.error.details.forEach(indivFieldError =>{
                    console.log(`indivFieldError`)
                    console.log(indivFieldError)
                    _errorList ? (_errorList = Object.assign({},{..._errorList},{[indivFieldError.context.key]: indivFieldError.message})) : _errorList = Object.assign({},{[indivFieldError.context.key]: indivFieldError.message});
                    
                })
                setErrorList(_errorList);
            }
            return _errorList;
        }else{
            console.log(`joiValidationScheme NOT Exists Branch`)
            return true;
        }


    }

    const handleHandleSave = async()=>{
        if(objectDto.Id < 1){
            console.log(`DeviceType submission`);
            console.log(objectDto);
            console.log(`PostDeviceTypeDto(deviceTypeDto)`)
            try{
                const result = await apiPostDto(objectDto)
                addToast("Saved!", {appearance :'success'});
                handleSave(objectDto)
            }catch(err){
                console.error(err.data);
                addToast(err.data.title, {appearance : 'error'})
                handleSave(objectDto, true); 
            }

        }else{
            console.log(`UpdateDeviceTypeDto(deviceTypeDto)`)
            
            try{
                const result = await apiUpdateDto(objectDto);
                addToast("Saved!", {appearance :'success'});
                handleSave(objectDto);
            }catch(err){
                console.error(err.data);
                addToast(err.data.title, {appearance : 'error'});
                handleSave(objectDto, true);

            }
        }
    }

    const handleSubmit =   (e)=>{
        e.preventDefault();

        //if scheme exists then validate test
        ////if pass then NEXT
        ////if fail then AddToast
        //if scheme not exist then NEXT

        if(joiValidationScheme){
            console.log(`joiValidationScheme`)
            const validationActionResult = validateFormSuccess();
            if(!validationActionResult){
                console.log(`handleHandleSave branch`)
                handleHandleSave()
            }else{
                
                console.log(`NOT SUCCESS joiValidationScheme`)
                console.log(`errorList`);
                console.log(validationActionResult);
                const errors = Object.entries(validationActionResult);
                errors.forEach(indiv=>{
                    addToast(`${indiv[0]} : ${indiv[1]}`, {appearance : 'error'});
                })
            }
        }else{
            console.log(`Direct HandleHandle Save`)
            handleHandleSave()
        }
    }



    const handleReset = ()=>{
        setObjectDto(oriObjectDto);

    }
    const funct = {
        handleReset : handleReset,
        handleSubmit: handleSubmit,
        handleOnChange: handleOnChange,
        objectDto:objectDto,
    }


    return (
        
        <React.Fragment>
            <form onSubmit = {handleSubmit}>
                {children(funct)}

            </form>
        </React.Fragment>

    );

}

export default DataTypeForm;