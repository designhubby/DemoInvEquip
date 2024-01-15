import React,{useEffect, useState, useRef} from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import SingleDataTypeView from './SingleDataTypeView';

import {  GetAllVendorDataDtos,GetVendorDataDtoByVendorId, UpdateVendorDataByDto, PostVendorDataByDto } from "../../service/DataServiceVendor";

import { Constants } from './../common/constants';
import {GeneralButton, TextField, SelectField, SubmitButton} from "../common/form";
import DataTypeForm from "../subcomponents/DataTypeComponents/DataTypeForm";

const columnNames = [

    {
        label: "id",
        datakey: "Id",
        visible: false,
        type: Constants.text,
    },
    {
        label: "Name",
        datakey: "Name",
        visible: true,
        type: Constants.text,
    },
    {
        label: "Edit",
        actionKey: "Id",
        visible: true,
        type: Constants.button,
        action : (funct, actionkey)=>(
            <button type = "button" className = "btn btn-primary" onClick = {()=>funct.onHandleEditClick(actionkey)}>Edit</button>
        )
    },
    {
        label: "Delete",
        actionKey: "Id",
        visible: true,
        type: Constants.button,
        action : (funct, actionkey)=>(
            <button type = "button" className = "btn btn-primary" onClick = {()=>funct.onHandleDeleteClick(actionkey)}>{funct.btnDisplayStatus(actionkey)}</button>
        )
    },

]

function VendorView2(){
    
    

    const dataTypeLabel = 'Vendor'
    const translatePrefixString = 'vendor' 

    const typeId = "Id";
    const typeName = "Name";
    const dataLoaded = true;


    return(
    <>
        {dataLoaded && <SingleDataTypeView 
            GetAllDataTypeDtos = {GetAllVendorDataDtos} 
            translatePrefixString = {translatePrefixString}
            dataTypeLabel = {dataTypeLabel} 
            columnNames = {columnNames} 
            apiDeleteId = {null}
            formDetail = {(singleDataTypeObjs, modalinject)=>(
                <DataTypeForm 
                label = {modalinject} 
                translatePrefixString = {translatePrefixString} 
                objectId = {singleDataTypeObjs.dataTypeIdForModal} 
                handleSave = {singleDataTypeObjs.handleSave} 
                handleCancel = {singleDataTypeObjs.handleCancel} 
                apiGetDtoById ={GetVendorDataDtoByVendorId} 
                apiPostDto= {PostVendorDataByDto} 
                apiUpdateDto= {UpdateVendorDataByDto}>
                    {(funct)=>
                        <>
                        <h2>{dataTypeLabel} Detail</h2>
                        <TextField placeHolder = "Example: Laptop" label = {`${dataTypeLabel} Name`} onChange = {funct.handleOnChange} statekey = {typeName} value = {funct.objectDto.Name} key ={typeName}/>
                        <SubmitButton label = "Save"/> 
                        <GeneralButton label = "Reset" handleOnClick = {funct.handleReset}/>
                        </>
                    }


                </DataTypeForm>
                )}
            />
        }
    </>
    )

}

export default VendorView2;