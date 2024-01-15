import React, {useEffect, useRef} from 'react';
import { useParams } from 'react-router';
import {useSearchParams, useNavigate } from "react-router-dom";
import { getPersonDetailsById } from '../../../service/DataServicePerson';
import { GetDevicesAssociatedPersons } from '../../../service/DataServicePersonDevice';
import { PersonDeviceDeviceHistoryTblColumns } from './PersonDeviceDeviceHistoryTbl';

import { TextField,SubmitButton, GeneralButton, SelectField,DatePicker } from './../../common/form';
import DataTypeForm from './../DataTypeComponents/DataTypeForm';
import SingleDataTypeView from './../../main/SingleDataTypeView';
import { PersonDetailsForm } from '../PersonDetailsForm';



//Shows list of Persons who were associated with this device
export function PersonDeviceDeviceHistory({deviceId}){

    const {idDevice} = useParams();

    async function getData(){

    }

    useEffect(()=>{
        //load useparams
        //load data
    },[])

    const APIGetAllDto = GetDevicesAssociatedPersons;
    const APIGetAllDto_Parameter = idDevice || deviceId;
    const APIGetById = getPersonDetailsById;
    const APIDeleteById = null;
    const APIPostDto = null;
    const APIUpdateDto = null;
    
    const dataTypeLabel = 'Device User History'
    const translatePrefixString = 'personDevice'
    const translatePrefixStringDataTypeForm = 'person'
    
    const typeName = 'fname' 
    const searchFields = ['fname','lname']

    const extendedMethods =null

    return(
        <>
        <SingleDataTypeView 
            GetAllDataTypeDtos = {APIGetAllDto} 
            GetAllDataTypeDtosParameter = {APIGetAllDto_Parameter}
            translatePrefixString = {translatePrefixString}
            dataTypeLabel = {dataTypeLabel} 
            columnNames = {PersonDeviceDeviceHistoryTblColumns} 
            apiDeleteId = {APIDeleteById}
            searchFields = {searchFields}
            extendedMethods = {extendedMethods}
            DisableNewButton = {true}
            formDetail = {(singleDataTypeObjs, modalinject)=>(
                <DataTypeForm 
                label = {modalinject} 
                translatePrefixString = {translatePrefixStringDataTypeForm} 
                objectId = {singleDataTypeObjs.dataTypeIdForModal} 
                handleSave = {singleDataTypeObjs.handleSave} 
                handleCancel = {singleDataTypeObjs.handleCancel} 
                apiGetDtoById ={APIGetById} 
                apiPostDto= {APIPostDto} 
                apiUpdateDto= {APIUpdateDto}>
                    {(funct)=>
                        <>
                        <PersonDetailsForm funct1 = {funct} singleDataTypeObjs ={singleDataTypeObjs}/>
                        </>
                    }
                </DataTypeForm>
                )}
                
        />
        </>
    )
}