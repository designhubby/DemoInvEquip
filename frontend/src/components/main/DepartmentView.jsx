import React, {useState, useRef} from 'react';
import { useSearchParams, useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { SingleDataTypeViewTable } from './../subcomponents/SingleDataTypeViewComponents/SingleDataTypeViewTable';
import { GetAllDepartmentDtos, GetDepartmentDtoById, PostDepartmentDto, PutDepartmentDto, DeleteDepartmentDto} from '../../service/DataServiceDepartment';
import { Constants } from './../common/constants';
import SingleDataTypeView from './SingleDataTypeView';
import SingleDataTypeForm from './../subcomponents/SingleDataTypeViewComponents/SingleDataTypeForm';
import DataTypeForm from './../subcomponents/DataTypeComponents/DataTypeForm';
import { TextField,SubmitButton, GeneralButton } from './../common/form';

import Joi from 'joi';

const joiValidationSchema = Joi.object().keys({
    Name : Joi.string().label('Department Name').min(3).max(60).required(),
}).options({allowUnknown: true});

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

function DepartmentView(){

    //const [dataTypeIdForModal, setDataTypeIdForModal] = useState(1);
    const dataTypeLabel = 'Department'
    const translatePrefixString = 'department' 
    const typeName = "Name";



    return (
        <>
        <SingleDataTypeView 
            GetAllDataTypeDtos = {GetAllDepartmentDtos} 
            translatePrefixString = {translatePrefixString}
            dataTypeLabel = {dataTypeLabel} 
            columnNames = {columnNames} 
            apiDeleteId = {DeleteDepartmentDto}
            formDetail = {(singleDataTypeObjs, modalinject)=>(
                <DataTypeForm 
                label = {modalinject} 
                translatePrefixString = {translatePrefixString} 
                objectId = {singleDataTypeObjs.dataTypeIdForModal} 
                handleSave = {singleDataTypeObjs.handleSave} 
                handleCancel = {singleDataTypeObjs.handleCancel} 
                apiGetDtoById ={GetDepartmentDtoById} 
                apiPostDto= {PostDepartmentDto} 
                joiValidationScheme = {joiValidationSchema}
                apiUpdateDto= {PutDepartmentDto}>
                    {(funct)=>
                        <>
                        <h2>{dataTypeLabel} Detail</h2>
                        <TextField placeHolder = "Example: Department" label = {`${dataTypeLabel} Name`} onChange = {funct.handleOnChange} statekey = {typeName} value = {funct.objectDto.Name} key ={typeName}/>
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

export default DepartmentView;