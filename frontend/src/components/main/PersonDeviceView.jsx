import React, {Component, useEffect,useRef, useState} from 'react';
import { useParams } from 'react-router';
import {useSearchParams, useNavigate } from "react-router-dom";
import PersonDeviceAPI, {GetDeviceByFilteredQuery, GetPersonAssociatedDevices, UnassociatePersonDeviceByIds} from '../../service/DataServicePersonDevice';
import { ShowCurrentAssignedPCDevice } from '../../service/fakePersonDeviceDtoService';
import  {getPersonDetailsById} from '../../service/DataServicePerson'
import {getPersonWithFriendlyDetails} from '../../service/fakeServicePerson';
import { PersonDeviceViewPersonInfo } from '../subcomponents/PersonDeviceViewComponents/PersonDeviceViewPersonInfo';
import { PersonDeviceViewTable } from './../subcomponents/PersonDeviceViewComponents/PersonDeviceViewTable';
import { DeviceListAddTable } from './../subcomponents/DeviceViewComponents/DeviceListAddTable';
import { DeviceView } from './DeviceListView';
import { PersonDeviceActions } from './../subcomponents/PersonDeviceViewComponents/PersonDeviceActions';
import { act } from 'react-dom/test-utils';

export const PersonDeviceView = (props)=>{

    //use Search Params to get PersonId / [filter] device type 
    //Data Retrieve: from (ShowCurrentAssignedPCDevice) as Array
    //Display: Table: | Device Name | Device Type | Device Model | btn: UnAssociate |
    //btn calls API to unassociate device, reloads Data Retreive

    const TestDeviceType02 = 2;
    const TestDeviceType03 = 3;
    const {idPerson} = useParams();
    const [searchParams, setSearchParams] = useSearchParams();

    const nagivate = useNavigate();

    const [deviceFilterType, setDeviceFilterType] = useState();
    const [PersonDeviceDto, setPersonDeviceDto] = useState();
    const [PersonData, setPersonData] = useState();
    const [DataLoaded, setDataLoaded] = useState(false);
    const [showAddComponent, setShowAddComponent] = useState(true);
    const [EndAssociationObj, setEndAssociationObj]= useState(new Set());
    const [btnStatusForDeviceAssociation, setBtnStatusForDeviceAssociation] = useState();
    const [btnVisibilityForDeviceAssociation, setBtnVisibilityForDeviceAssociation] = useState(new Set());


    useEffect(()=>{
        LoadSearchParams();
        LoadPersonDeviceDtoData();
    },[])

    const LoadPersonDeviceDtoData = async()=>{
        const PersonDeviceData = await GetPersonAssociatedDevices(idPerson);
        //find all the PersonDeviceDtos.PersonDeviceId where endDate == null
        const PersonDeviceDtosFiltered =  PersonDeviceData.filter(indiv=>indiv.endDate == null);
        const PersonDeviceIdsFiltered = PersonDeviceDtosFiltered.map(indiv=>indiv.personDeviceId);

        setBtnVisibilityForDeviceAssociation(new Set(PersonDeviceIdsFiltered));

        console.log(PersonDeviceData);
        const PersonData = await getPersonDetailsById(idPerson);
        setPersonDeviceDto(PersonDeviceData);
        setPersonData(PersonData);
        setDataLoaded(true);
    }

    const LoadSearchParams = ()=>{
        const filterType = searchParams.get("filterby");
        if(!filterType){
            setDeviceFilterType(TestDeviceType02)
        }else{
            setDeviceFilterType(filterType);
        }

    }



    const handleOnClickAddDevice = (e, labelfortesting)=>{
        const value = e.target.value;
        console.log('value')
        console.log(value);
        nagivate(`/main/DeviceListView/?action=edit&activerentalfilter=false&retiredfilter=false&personId=${idPerson}`)

    }

    const handleEndAssociationBtn = (actionkey)=>{
        //push 'personDeviceId' to existing 'EndAssociationObj'
        if(EndAssociationObj.has(actionkey)){
            console.log(`branch true`)
            console.log(actionkey)
            EndAssociationObj.delete(actionkey)
            const newSet = new Set(EndAssociationObj);
            setEndAssociationObj(newSet);
        }else{
            console.log(`branch else`)
            console.log(actionkey)
            const newSet= new Set(EndAssociationObj.add(actionkey));
            setEndAssociationObj(newSet);
        }


    }

    const handleEndAssociationBtnDisabled = (actionkey)=>{
        if(btnVisibilityForDeviceAssociation.has(actionkey)){
            return false; //for the 'disable' tag
        }else{
            return true;
        }
    }

    const handleEndAssociationBtnStatus = (actionkey)=>{
        if(EndAssociationObj.has(actionkey)){
            return "Cancel End Rental"
        }else{
            return "End Rental"
        }
    }
    const handleEndAssociationSave = ()=>{
        //send the list of PersonDeviceIds to API controller for ending associations
        const results = Array.from(EndAssociationObj);
        console.log(results);
        UnassociatePersonDeviceByIds(results);
        nagivate('/main/PersonView')

    }
    const buttons = [
        <button type = "button" className = "btn btn-primary" onClick={(e)=>handleOnClickAddDevice(e)}>Add Device to User</button>,
        <button type = "button" className = "btn btn-primary" onClick={()=>handleEndAssociationSave()}>Save</button>
    ]

    const funct = {
        handleEndAssociationBtn : handleEndAssociationBtn,
        handleEndAssociationBtnStatus: handleEndAssociationBtnStatus,
        handleEndAssociationBtnDisabled: handleEndAssociationBtnDisabled,
    }

    const renderView = () =>(
        //Add Device | (unoccupied devices) Device Type => Model Name => Device Name
        DataLoaded && 
            <React.Fragment>
            <PersonDeviceViewPersonInfo PersonData = {PersonData}/>
            <hr/>
            <PersonDeviceViewTable PersonDeviceData = {PersonDeviceDto} onEdit = {funct}  />

            <PersonDeviceActions buttons={buttons}/>
            </React.Fragment>
        
        
    )



    return(
        //render Person Info
        //render PersonDevice List Table
        //When add button pressed, show 'DeviceListAddTable' component
        //When add device button pressed, refresh 'PersonDeviceViewTable' to reflect added device

        <React.Fragment>
            {renderView()}
            
        </React.Fragment>
    )


}

