
import React, {useState, useRef} from 'react';
import { useSearchParams, useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { SingleDataTypeViewTable } from './../subcomponents/SingleDataTypeViewComponents/SingleDataTypeViewTable';
import { Constants } from './../common/constants';
import SingleDataTypeView from './SingleDataTypeView';
import SingleDataTypeForm from './../subcomponents/SingleDataTypeViewComponents/SingleDataTypeForm';
import { TextField,SubmitButton, GeneralButton, SelectField } from './../common/form';

import { GetAllHwModelListDtos,GetHwModelListDtoById,GetHwModelDataById, DeleteHwModelByIdAsync, PostHWModel, UpdateHWModel } from "../../service/DataServiceHwModel";
import DataTypeForm from './../subcomponents/DataTypeComponents/DataTypeForm';
import { useEffect } from 'react';
import { GetAllDeviceTypeDtos } from '../../service/DataServiceDeviceType';
import { GetAllVendorDataDtos } from '../../service/DataServiceVendor';
import TranslatePropertyKeys from '../../util/translatePropertyKeys';


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
        label: "DeviceType Name",
        datakey: "deviceTypeName",
        visible: true,
        type: Constants.text,
    },
    {
        label: "Vendor Name",
        datakey: "vendorName",
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

function HwModelView(){

    //const [dataTypeIdForModal, setDataTypeIdForModal] = useState(1);
    const APIGetAllDto = GetAllHwModelListDtos;
    const APIGetById = GetHwModelDataById;
    const APIDeleteById = DeleteHwModelByIdAsync;
    const APIPostDto = PostHWModel;
    const APIUpdateDto = UpdateHWModel;
    
    const dataTypeLabel = 'hwModel'
    const translatePrefixString = 'hwModel' 
    const typeName = "Name";
    const searchFields = ['Name', 'deviceTypeName','vendorName']

    // extended elements for Dataform
    
    const statekeyDeviceTypeId = "deviceTypeId";
    const statekeyVendorId = "vendorId";
    const [deviceTypeDtos, setDeviceTypeDtos] = useState({});
    const [vendorDtos, setVendorDtos] = useState({});


    const getData = async ()=>{
        //load DeviceTypeDto 
        let temp_deviceTypeDtos = await GetAllDeviceTypeDtos();
        const deviceTypePrefix = "deviceType";
        temp_deviceTypeDtos = TranslatePropertyKeys(temp_deviceTypeDtos, deviceTypePrefix);

        //add dummy option
        temp_deviceTypeDtos.unshift({
            Id: 0,
            Name: "Choose a Device Type",
        })

        setDeviceTypeDtos(temp_deviceTypeDtos);
        //load VendorDto
        let temp_vendorDtos = await GetAllVendorDataDtos();
        const vendorPrefix = "vendor";
        temp_vendorDtos = TranslatePropertyKeys(temp_vendorDtos, vendorPrefix);
        temp_vendorDtos.unshift({
            Id: 0,
            Name: "Choose a Vendor",
        })
        setVendorDtos(temp_vendorDtos);
        console.log('temp_vendorDtos')
        console.log(temp_vendorDtos)
    }

    useEffect(()=>{
        console.log('useeffect')
        getData();

    },[])

    return (
        <>
        <SingleDataTypeView 
            GetAllDataTypeDtos = {APIGetAllDto} 
            translatePrefixString = {translatePrefixString}
            dataTypeLabel = {dataTypeLabel} 
            columnNames = {columnNames} 
            apiDeleteId = {APIDeleteById}
            searchFields = {searchFields}
            formDetail = {(singleDataTypeObjs, modalinject)=>(
                <DataTypeForm 
                label = {modalinject} 
                translatePrefixString = {translatePrefixString} 
                objectId = {singleDataTypeObjs.dataTypeIdForModal} 
                handleSave = {singleDataTypeObjs.handleSave} 
                handleCancel = {singleDataTypeObjs.handleCancel} 
                apiGetDtoById ={APIGetById} 
                apiPostDto= {APIPostDto} 
                apiUpdateDto= {APIUpdateDto}>
                    {(funct)=>
                        <>
                        <h2>{dataTypeLabel} Detail</h2>
                        <TextField placeHolder = "Example: Department" label = {`${dataTypeLabel} Name`} onChange = {funct.handleOnChange} statekey = {typeName} value = {funct.objectDto.Name} key ={typeName}/>
                        <SelectField options={deviceTypeDtos} onChange ={funct.handleOnChange} value={funct.objectDto.deviceTypeId} label={"device type"} statekey={statekeyDeviceTypeId}/>
                        <SelectField options ={vendorDtos} onChange ={funct.handleOnChange} value={funct.objectDto.vendorId} label={"vendor"} statekey={statekeyVendorId}/>
                        <SubmitButton label = "Save"/> 
                        <GeneralButton label = "Reset" handleOnClick = {funct.handleReset}/>
                        </>
                    }
                </DataTypeForm>
                )}
            />
        </>
    )

}

export default HwModelView;