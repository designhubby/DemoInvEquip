import React,{useEffect, useState, useRef} from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { GetAllDeviceTypeDtos, GetDeviceTypeDtoByDeviceTypeId, DeleteDeviceTypeById,PostDeviceTypeDto,UpdateDeviceTypeDto } from "../../service/DataServiceDeviceType";

import { TextField, SubmitButton, GeneralButton, SelectField } from '../common/form';
import DataTypeForm from '../subcomponents/DataTypeComponents/DataTypeForm';
import SingleDataTypeView from './SingleDataTypeView';
import TranslatePropertyKeys from "../../util/translatePropertyKeys";
import { Constants } from "../common/constants";


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

export function DeviceTypeView2(){

    const dataTypeLabel = 'DeviceType'
    const translatePrefixString = 'deviceType' 

    const typeId = "Id";
    const typeName = "Name";
    const dataLoaded = true;

    return(
<>
        {dataLoaded && <SingleDataTypeView 
            GetAllDataTypeDtos = {GetAllDeviceTypeDtos} 
            translatePrefixString = {translatePrefixString}
            dataTypeLabel = {dataTypeLabel} 
            columnNames = {columnNames} 
            apiDeleteId = {DeleteDeviceTypeById}
            formDetail = {(singleDataTypeObjs, modalinject)=>(
                <DataTypeForm 
                label = {modalinject} 
                translatePrefixString = {translatePrefixString} 
                objectId = {singleDataTypeObjs.dataTypeIdForModal} 
                handleSave = {singleDataTypeObjs.handleSave} 
                handleCancel = {singleDataTypeObjs.handleCancel} 
                apiGetDtoById ={GetDeviceTypeDtoByDeviceTypeId} 
                apiPostDto= {PostDeviceTypeDto} 
                apiUpdateDto= {UpdateDeviceTypeDto}>
                    {(funct)=>
                        <>
                        <h2>{dataTypeLabel} Detail</h2>
                        <TextField placeHolder = "Example: Laptop" label = {`${dataTypeLabel} Name`} onChange = {funct.handleOnChange} statekey = {typeName} value = {funct.objectDto[typeName]}/>
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