import React, {useEffect, useState} from 'react';
import { Constants } from './../common/constants';
import SingleDataTypeView from './SingleDataTypeView';
import PersonAPI, {GetPersonDetailsDto, getPersonDetailsById, PostPerson, UpdatePersonByDto, DeletePerson, getPeopleWhere} from '../../service/DataServicePerson';

import { GetAllDepartmentDtos, GetDeptRoles } from '../../service/DataServiceDepartment';
import DataTypeForm from '../subcomponents/DataTypeComponents/DataTypeForm';
import { TextField, SubmitButton, GeneralButton, SelectField, DepartmentSelectField, RoleSelectField } from '../common/form';
import TranslatePropertyKeys from '../../util/translatePropertyKeys';
import Joi from 'joi';

const joiValidationSchema = Joi.object().keys({
    fname: Joi.string().label('First Name').min(1).max(100).required(),
    lname:Joi.string().label('Last Name').min(1).max(100).required(),
    departmentName:Joi.string().label('Department Name').required(),
    roleName :Joi.string().label('Role Name').required(),

}).options({allowUnknown: true});

const ColumnNames = [
    {
        label: "id",
        datakey: "personId",
        visible: false,
        type: Constants.text,
    },
    {
        label:"First Name",
        datakey: "fname",
        visible: true,
        type: Constants.text,

    },
    {
        label:"Last Name",
        datakey: "lname",
        visible: true,
        type: Constants.text,
    },
    {
        label:"Department Name",
        visible: true,
        datakey:"departmentName",
        type: Constants.text,
    },
    {
        label:"Role Name",
        visible: true,
        datakey:"roleName",
        type: Constants.text,
    },
    {
        label:"Edit",
        visible: true,
        type: Constants.button,
        actionKey:"Id",
        action : (funct, actionkey)=>(
            <button type = "button" className = "btn btn-primary" onClick = {()=>funct.onHandleEditClick(actionkey)}>Edit</button>
        )
        
    },
    {
        label:"ViewDevices",
        visible: true,
        datakey: "Id",
        type: Constants.link,
        link: "/main/PersonDeviceView/"
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

export function PersonView(){

    const dataTypeLabel = 'Person'
    const translatePrefixString = 'person' 

    const typeId = "Id";
    const typeName = "Name";
    const typeFname = "fname";
    const typeLname ="lname";
    const departmentId = "departmentId";
    const roleId = "roleId";

    const searchFields = ["fname","lname"];

    const [dataLoaded, setDataLoaded] = useState(true);

    const [extendedSearchFields, setExtendedSearchFields] = useState();

    const handleOnChange = (e)=>{
        const name = e.target.dataset.statekey;
        const value = e.target.value;
        //setSelectedSearchDepartmentId(value);
        if(value == 0){
            setExtendedSearchFields(null);
        }else{
            const _extendedSearchFields = [
                {[name]: value},
            ]
            setExtendedSearchFields(_extendedSearchFields);
        }
    }

    return(
    <>
        {dataLoaded && <SingleDataTypeView 
            GetAllDataTypeDtos = {GetPersonDetailsDto} 
            translatePrefixString = {translatePrefixString}
            dataTypeLabel = {dataTypeLabel} 
            columnNames = {ColumnNames} 
            apiDeleteId = {DeletePerson}
            children = {(funct)=><DepartmentSelectField apiDepartmentData = {GetAllDepartmentDtos} onChange = {funct.handleOnExtendedSearchFieldsChange} currentValue = {funct.showExtendedSearchFieldCurrentValue(departmentId)} label = {"Department"} statekey = {departmentId}/>}
            formDetail = {(singleDataTypeObjs, modalinject)=>(
                <DataTypeForm 
                label = {modalinject} 
                translatePrefixString = {translatePrefixString} 
                objectId = {singleDataTypeObjs.dataTypeIdForModal} 
                handleSave = {singleDataTypeObjs.handleSave} 
                handleCancel = {singleDataTypeObjs.handleCancel} 
                apiGetDtoById ={getPersonDetailsById} 
                apiPostDto= {PostPerson} 
                apiUpdateDto= {UpdatePersonByDto}
                joiValidationScheme = {joiValidationSchema}
                >
                    {(funct)=>
                        <>
                        <h2>{dataTypeLabel} Detail</h2>
                        <TextField placeHolder = "Example: John" label = {`${dataTypeLabel} First Name`} onChange = {funct.handleOnChange} statekey = {typeFname} value = {funct.objectDto.fname}/>
                        <TextField placeHolder = "Example: Smith" label = {`${dataTypeLabel} Last Name`} onChange = {funct.handleOnChange} statekey = {typeLname} value = {funct.objectDto.lname}/>
                        <DepartmentSelectField apiDepartmentData = {GetAllDepartmentDtos} onChange = {funct.handleOnChange} currentValue = {funct.objectDto.departmentId} label = {"Department"} statekey = {departmentId} labelkey="Name"/>
                        <RoleSelectField apiRoleData = {GetDeptRoles} onChange ={funct.handleOnChange} currentValue ={funct.objectDto.roleId} label ={"Role"} statekey="roleId" departmentId ={funct.objectDto.departmentId}/>
                        {funct.objectDto.Id > 0 && <GeneralButton label = "Delete" handleOnClick = {singleDataTypeObjs.onHandleDeleteClick} data = {funct.objectDto.Id}/>}
                        <SubmitButton label = "Save"/> 
                        <GeneralButton label = "Reset" handleOnClick = {funct.handleReset}/>
                        </>
                    }
                </DataTypeForm>
                )}
            searchFields = {searchFields}
            />
        }
    </>
    )

}


