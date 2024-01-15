import React, {useEffect, useRef, useState} from 'react';
import { useParams } from 'react-router';
import { GetAllDepartmentDtos, GetDeptRoles } from '../../service/DataServiceDepartment';
import { TextField, SubmitButton, GeneralButton, SelectField } from './../common/form';
import TranslatePropertyKeys, { TranslatePropertyKeysToDto  } from './../../util/translatePropertyKeys';


export function PersonDetailsForm({funct1, singleDataTypeObjs}){
    
    const dataTypeLabel = 'Person' 
    const typeFname = "fname";
    const typeLname ="lname";
    const departmentId = "departmentId";
    const roleId = "roleId";

    return(
        <>
        <h2>{dataTypeLabel} Detail</h2>
        <TextField placeHolder = "Example: John" label = {`${dataTypeLabel} First Name`} onChange = {funct1.handleOnChange} statekey = {typeFname} value = {funct1.objectDto.fname}/>
        <TextField placeHolder = "Example: Smith" label = {`${dataTypeLabel} Last Name`} onChange = {funct1.handleOnChange} statekey = {typeLname} value = {funct1.objectDto.lname}/>
        <DepartmentSelectField apiDepartmentData = {GetAllDepartmentDtos} onChange = {funct1.handleOnChange} currentValue = {funct1.objectDto.departmentId} label = {"Department"} statekey = {departmentId}/>
        <RoleSelectField apiRoleData = {GetDeptRoles} onChange ={funct1.handleOnChange} currentValue ={funct1.objectDto.roleId} label ={"Role"} statekey={roleId} departmentId ={funct1.objectDto.departmentId}/>
        {funct1.objectDto.Id > 0 && <GeneralButton label = "Delete" handleOnClick = {singleDataTypeObjs.onHandleDeleteClick} data = {funct1.objectDto.Id}/>}
        <SubmitButton label = "Save"/> 
        <GeneralButton label = "Reset" handleOnClick = {funct1.handleReset}/>        
        </>
    )

}

export const DepartmentSelectField = ({apiDepartmentData, onChange, currentValue, label, statekey})=>{
    const [departmentsAll, setDepartmentsAll] = useState([]);
    const [dataLoaded, setDataLoaded] = useState(true);

    useEffect(()=>{
        GetData();
    },[]);

    async function GetData(){
        const departmentsDtoArray = await apiDepartmentData();
        const translatedKeysArray = TranslatePropertyKeys(departmentsDtoArray, "department")
        await Promise.resolve(setDepartmentsAll(translatedKeysArray));
        await Promise.resolve(setDataLoaded(true));
    } 
    return(
        <>{dataLoaded && 
            <SelectField options ={departmentsAll} onChange={onChange} value={currentValue} label={label} statekey={statekey}/>
        }
        </>
    )
}

export const RoleSelectField = ({apiRoleData, onChange, currentValue, label, statekey, departmentId})=>{

    const [rolesAllData, setRolesAllData] = useState([]);
    const [dataLoaded, setDataLoaded] = useState(true);

    useEffect(()=>{
        getData();
    },[departmentId])

    const unassignedEntry =   {
        "roleId": 0,
        "roleName": "Unassigned",
        "departmentId": 0
      };

    const getData = async ()=>{
        let data =[];
        if(departmentId){
            console.log(`Role SelectField: departmentId provided`)
            console.log(departmentId);
            data = await apiRoleData(departmentId);
        }else{
            console.log(`Role SelectField: no departmentId provided`)
            data  = await apiRoleData();
        }
        data.unshift(unassignedEntry);
        const translatedKeysArray = TranslatePropertyKeys(data, "role")
        await Promise.resolve(setRolesAllData(translatedKeysArray));
        await Promise.resolve(setDataLoaded(true));

    }
    return(
        <>
        {dataLoaded && <SelectField options ={rolesAllData} onChange={onChange} value={currentValue} label={label} statekey={statekey}/>}
        </>
    )

}