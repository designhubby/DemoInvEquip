import React, {useEffect, useState} from 'react';
import { Constants } from './../common/constants';
import SingleDataTypeView from './SingleDataTypeView';
import { GetAllRoleDtos,GetRoleById, PostRoleDto, PutRoleDto, DeleteRoleById } from './../../service/DataServiceRole';
import { GetAllDepartmentDtos } from '../../service/DataServiceDepartment';
import SingleDataTypeForm from './../subcomponents/SingleDataTypeViewComponents/SingleDataTypeForm';
import DataTypeForm from '../subcomponents/DataTypeComponents/DataTypeForm';
import { TextField, SubmitButton, GeneralButton, SelectField, DepartmentSelectField } from '../common/form';
import TranslatePropertyKeys from '../../util/translatePropertyKeys';
import Joi from 'joi';

const joiValidationSchema = Joi.object().keys({
    Name: Joi.string().label('First Name').min(1).max(100).required(),
    departmentName:Joi.string().label('Department Name').required(),

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
        label: "Department Name",
        datakey: "departmentName",
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

export function RoleView(){

    const dataTypeLabel = 'Role'
    const translatePrefixString = 'role' 

    const typeId = "Id";
    const typeName = "Name";
    const departmentId = "departmentId"




    return(
    <>
        <SingleDataTypeView 
            GetAllDataTypeDtos = {GetAllRoleDtos} 
            translatePrefixString = {translatePrefixString}
            dataTypeLabel = {dataTypeLabel} 
            columnNames = {columnNames} 
            apiDeleteId = {DeleteRoleById}
            children = {(funct)=><DepartmentSelectField apiDepartmentData = {GetAllDepartmentDtos} onChange = {funct.handleOnExtendedSearchFieldsChange} currentValue = {funct.showExtendedSearchFieldCurrentValue(departmentId)} label = {"Department"} statekey = {departmentId}/>}
            formDetail = {(singleDataTypeObjs, modalinject)=>(
                <DataTypeForm 
                label = {modalinject} 
                translatePrefixString = {translatePrefixString} 
                objectId = {singleDataTypeObjs.dataTypeIdForModal} 
                handleSave = {singleDataTypeObjs.handleSave} 
                handleCancel = {singleDataTypeObjs.handleCancel} 
                apiGetDtoById ={GetRoleById} 
                apiPostDto= {PostRoleDto} 
                joiValidationScheme={joiValidationSchema}
                apiUpdateDto= {PutRoleDto}>
                    {(funct)=>
                        <>
                        <h2>{dataTypeLabel} Detail</h2>
                        <TextField placeHolder = "Example: Laptop" label = {`${dataTypeLabel} Name`} onChange = {funct.handleOnChange} statekey = {typeName} value = {funct.objectDto.Name} key ={typeName}/>
                        <DepartmentSelectField apiDepartmentData ={GetAllDepartmentDtos} onChange={funct.handleOnChange} currentValue={funct.objectDto.departmentId} label="Department Name" statekey={departmentId}/>
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

