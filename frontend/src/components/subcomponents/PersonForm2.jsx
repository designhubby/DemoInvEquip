import React, {useEffect, useState, useRef, Component} from 'react';
import axios from 'axios';
import { useParams } from 'react-router';
import {useSearchParams } from "react-router-dom";
import { TextField,EmailField, SelectField, SubmitButton,GeneralButton } from "../common/form";
import PersonAPI, {PostPerson, DeletePerson} from '../../service/DataServicePerson';
//import {getPerson}  from '../../service/fakeServicePerson';
import DepartmentAPI from '../../service/DataServiceDepartment';
//import { getAllDepartments, getDepartmentsRoleByDepartmentId } from '../../service/fakeServiceDepartment';
import RoleAPI from '../../service/DataServiceRole';
//import { getRole } from './../../service/fakeServiceRole';
import { useNavigate } from "react-router-dom";
import TranslatePropertyKeys from '../../util/translatePropertyKeys';

function PersonForm2(props){

    const history = useNavigate();

    const Loaded = 2;
    const Loading = 1;
    const Unloaded = 0;

    const [PersonData, SetPersonData] = useState(null);
    const [DepartmentListData, SetDepartmentList] = useState(null);
    const [RoleListData, SetRoleListData] = useState();
    const [DataLoaded,SetDataLoaded] = useState(Unloaded);
    const SelectedDepartmentId = useRef();
    const {idPerson} = useParams()
    const Mounted = useRef(false);

    
    useEffect(()=>{
        if(isNaN(idPerson) && idPerson !== "new"){
            history("/main/PersonView");
            return null;
        }
        
        Mounted.current = true;

        if(Mounted.current){
            console.log("running useEffect1");
            LoadData();

        }
        console.log("mounted.current: " , Mounted.current)
        
        return function CleanUp(){
            console.log("RunCleanUp")
            SetDataLoaded(Unloaded);
            Mounted.current = false;
        }
    },[idPerson])



    const LoadData = async ()=>{

        if(idPerson !=="new"){
                
                
            const [, result2] = await Promise.all([LoadDepartmentListData(),LoadPersonData(idPerson)])
            const roleDto = await RoleAPI.get(`GetRoleById/${result2.roleId}`).then(result=>result.data)//get role entity, within it, use role's department -> get all other roles belongings to that department
            
            await Promise.all([LoadRoleListData(roleDto.departmentId), Promise.resolve(SelectedDepartmentId.current= roleDto.departmentId)]);
            await Promise.resolve(SetDataLoaded( Loaded));

        }else{
            SetPersonData({
                personId: null,
                fname : "",
                lname: "",
                roleId: "",
                deleted: false,
            });
            const departmentListData = await LoadDepartmentListData()
            console.log('departmentListData')
            console.log(departmentListData)
            await LoadRoleListData(departmentListData[0].Id)
            await Promise.resolve(SetDataLoaded(Loaded))
        }
    }

    const handleSubmit=async (e)=>{
        e.preventDefault();
        console.log("Submitted Pressed")
        const payload = {
            personId : PersonData.personId,
            fname : PersonData.fname,
            lname : PersonData.lname,
            roleId : PersonData.roleId,
        }
        await Promise.resolve( 
            (
                async ()=>{
                    if(payload.personId !== null){
                        await PersonAPI.put(`${payload.personId}`,payload);
                    }else{
                        delete payload.personId;
                        await PostPerson(payload);
                        console.log("posted")
                    }
                    
                    history("/main/PersonView");
                }
            )()
        )

    }

    const LoadPersonData = async (id)=>{
        return new Promise((resolve, reject)=>{
            resolve( PersonAPI.get(`GetById/${id}`).then(result=>result.data))
        })
        .then((result)=>{
            return new Promise((resolve, reject)=>{
                console.log("getPerson: ", result)
                if(Mounted.current) SetPersonData(result);
                resolve(result);
            })
        })
    }

    const LoadDepartmentListData = async ()=>{
        let allDepartments = await DepartmentAPI.get(' ').then(result=>(result.data));
        allDepartments = TranslatePropertyKeys(allDepartments, 'department');
        console.log('allDepartments')
        console.log(allDepartments)
        await Promise.resolve((()=>{
            if(Mounted.current) {
            SetDepartmentList(allDepartments)
        }
    })())
    return allDepartments;

    }

    const LoadRoleListData = async (departmentId)=>{
        GetDepartmentRoleData(departmentId)
        .then((result)=>Promise.resolve( //see if we can remmove promise and directly SetRole
                SetRoleListData(result)
                )
            )          
    }

    const GetDepartmentRoleData = async (departmentId)=>{
        let departmentRoleData = await Promise.resolve(DepartmentAPI.get(`GetDeptRoles/${departmentId}`).then(result=>(result.data)))
        departmentRoleData = TranslatePropertyKeys(departmentRoleData, 'role');
        return departmentRoleData;
    }

    const handleDepartmentSelectorChange= async (e,label_fortesting)=>{
        SetRoleListData(null);
        const UnassignedOption = {
            Id: 0,
            Name: 'Unassigned',
            DepartmentId: 1,
            Deleted: false,
        }
        const selectedDepartmentId = e.target.value;
        const departmentSpecificRoles = await GetDepartmentRoleData(selectedDepartmentId)
        await Promise.resolve((()=>{
            SetRoleListData(
                    
                [
                    UnassignedOption,
                    ...departmentSpecificRoles,
                ]
                );
                SelectedDepartmentId.current =selectedDepartmentId;
                SetPersonData((prevState)=>({ //setting role back to 'unassigned' after department id changed
                    ...prevState,
                    roleId: 0
                }))
        })())

    }


    const handleFormFieldChange=(e, label_fortesting)=>{
        console.log(`Running HandleFormFieldChange: Label: ${label_fortesting} and e.value ${e.target.value}`);

        const value = e.target.value;
        const state_key = e.target.dataset.statekey;
        SetPersonData((prevState)=>({
            ...prevState,
            [state_key]: value,
        }))
        console.log(`Value Change for: ${label_fortesting}, value = ${value}`);
    }
    
    const handleOnClickDelete = async () =>{
        await DeletePerson(PersonData.personId);
        await Promise.resolve(history("/main/PersonView"));
    }


    const renderForm =()=>{
        if(DataLoaded==Loaded){
            const FirstName = Object.keys(PersonData)[1];
            const LastName = Object.keys(PersonData)[2];
            const RoleId = Object.keys(PersonData)[3];
            
            console.log('SelectedDepartmentId.current')
            console.log(SelectedDepartmentId.current)
            console.log("RenderForm: Loaded")
            return(
                <React.Fragment>

                <TextField placeHolder = "Type Here" label = "First Name" statekey={FirstName} onChange={handleFormFieldChange} value ={PersonData.fname} />
                
                <TextField placeHolder = "Type Here" label = "Last Name" statekey={LastName} onChange={handleFormFieldChange} value ={PersonData.lname} />
                
                <SelectField options ={DepartmentListData} label = "Department Id" statekey={null} onChange={handleDepartmentSelectorChange} value = {SelectedDepartmentId.current}/>

                <SelectField options = {RoleListData} label = "Role Id" statekey={RoleId} onChange={handleFormFieldChange} value ={PersonData.roleId} />


                <SubmitButton label="Save"/>
                {PersonData.personId && <GeneralButton label ="Delete" handleOnClick = {handleOnClickDelete}/>}

                </React.Fragment>
            )
        }else{
            console.log("RenderForm: Loading");
            return <div>Loading......................................</div>
        }
    }

    return (
        
            <form onSubmit={handleSubmit}>
                {renderForm()}
                
                
            </form>
        
    )


}

export default PersonForm2;