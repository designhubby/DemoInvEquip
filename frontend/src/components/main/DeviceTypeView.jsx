import React,{useEffect, useState, useRef} from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { useParams } from "react-router";

import TranslatePropertyKeys from "../../util/translatePropertyKeys";

import { Constants } from './../common/constants';
import {GeneralButton, TextField, SelectField, SubmitButton} from "../common/form";
import { Modal } from "../common/Modal";
import VendorForm from "../subcomponents/VendorViewComponents/VendorForm";

import { GetAllDeviceTypeDtos } from "../../service/DataServiceDeviceType";
import { DeviceTypeViewTable } from './../subcomponents/DeviceTypeViewComponents/DeviceTypeViewTable';
import DeviceTypeForm from "../subcomponents/DeviceTypeViewComponents/DeviceTypeForm";


function DeviceTypeView(){
    
    

    const [deviceTypeArray, setDeviceTypeArray] = useState([]);
    const [searchFilter, setSearchFilter] = useSearchParams();
    const [dataLoaded, setDataLoaded] = useState(false);
    const navigate = useNavigate();
    const [popUpToggle, setPopUpToggle] = useState(false);
    const [deviceTypeIdForModal, setDeviceTypeIdForModal] = useState(1);

    async function GetData(){
        //get all devicetypes from API
        let data = await GetAllDeviceTypeDtos();
        data = TranslatePropertyKeys(data, "deviceType");
        await Promise.resolve(setDeviceTypeArray(data));

        await Promise.resolve(setDataLoaded(true));

    }

    useEffect(()=>{
        GetData();
    },[])

    function reload(){window.location.reload();}

    function handleOnClickNew(){
        setDeviceTypeIdForModal("new");
        setPopUpToggle(!popUpToggle);
    }

    function handleCancel(){
        setPopUpToggle(false);
    }

    function handleSave(){
        setPopUpToggle(false);
        reload();
    }

    async function handleSearchTermChange(e){
        const value = e.target.value;
        searchFilter.set("deviceTypeName", value);
    }

    function onHandleEditClick(actionkey){
        console.log(`Edit Function ${actionkey}`)
        console.log(`PopUpToggle ${popUpToggle}`)
        setDeviceTypeIdForModal(actionkey);
        setPopUpToggle(true);
        
        return null;
    }

    function onHandleDeleteClick(actionkey){
        console.log(`Delete Function ${actionkey}`)
        return null;
    }
    
    function btnDisplayStatus(actionkey){
        console.log(`Display Function ${actionkey}`)
        return "Enable";
    }

    function RenderView(){
        const searchTerm = searchFilter.get("deviceTypeName");
        const funct = {
            handleSearchTermChange: handleSearchTermChange,
            btnDisplayStatus: btnDisplayStatus,
            onHandleEditClick: onHandleEditClick,
            onHandleDeleteClick: onHandleDeleteClick,
        }
        let results;
        if(searchTerm){
            //filter results to searchTerm
            console.log('search branch')
            results = deviceTypeArray.filter(indiv=> (indiv.Name.toLowerCase().indexOf(searchTerm.toLowerCase())>-1));
        }else{
            //return deviceTypeArrayDto
            console.log('no search branch')
            console.log(deviceTypeArray)
            results = deviceTypeArray;
        }
        //paginate

        return(
            //Table
            <React.Fragment>
                <h1>DeviceType List</h1>
                <button className = "btn btn-primary" type = "button" onClick = {()=>handleOnClickNew()}>New Device Type</button>
                <DeviceTypeViewTable funct={funct} data = {results} />
                <Modal handleClose = {handleCancel} show = {popUpToggle}>
                    {()=> <DeviceTypeForm deviceTypeId = {deviceTypeIdForModal} handleSave = {handleSave} handleCancel = {handleCancel} />}
                    
                </Modal>
            </React.Fragment>
        )
    }

    return(
        <React.Fragment>
            {dataLoaded && RenderView()}
        </React.Fragment>
    )

}

export default DeviceTypeView;