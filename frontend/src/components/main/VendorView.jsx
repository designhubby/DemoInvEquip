import React,{useEffect, useState, useRef} from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { useParams } from "react-router";

import TranslatePropertyKeys from "../../util/translatePropertyKeys";

import { GetContractDataDtosOwnByVendorByVendorId, GetAllVendorDataDtos,GetVendorDataDtoByVendorId } from "../../service/DataServiceVendor";

import { VendorViewTable } from './../subcomponents/VendorViewComponents/VendorViewTable';
import { Constants } from './../common/constants';
import {GeneralButton, TextField, SelectField, SubmitButton} from "../common/form";
import { Modal } from "../common/Modal";
import VendorForm from "../subcomponents/VendorViewComponents/VendorForm";


function VendorView(){
    
    

    const [vendorArray, setVendorArray] = useState([]);
    const [searchFilter, setSearchFilter] = useSearchParams();
    const [dataLoaded, setDataLoaded] = useState(false);
    const navigate = useNavigate();
    const [popUpToggle, setPopUpToggle] = useState(false);
    const [vendorIdForModal, setVendorIdForModal] = useState(1);

    async function GetData(){
        //get all vendors from API
        let data = await GetAllVendorDataDtos();
        data = TranslatePropertyKeys(data, "vendor");
        await Promise.resolve(setVendorArray(data));

        await Promise.resolve(setDataLoaded(true));

    }

    useEffect(()=>{
        GetData();
    },[])

    function reload(){window.location.reload();}

    function handleOnClickNewVendor(){
        setVendorIdForModal("new");
        setPopUpToggle(!popUpToggle);
    }

    async function onSortChange(e){
        
        const columnName = e.target.value;
        

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
        searchFilter.set("vendorName", value);
    }

    function onHandleEditClick(actionkey){
        console.log(`Edit Function ${actionkey}`)
        console.log(`PopUpToggle ${popUpToggle}`)
        setVendorIdForModal(actionkey);
        setPopUpToggle(true);
        //navigate(`/main/VendorForm/`)
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
        const searchTerm = searchFilter.get("vendorName");
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
            results = vendorArray.filter(indiv=> (indiv.Name.toLowerCase().indexOf(searchTerm.toLowerCase())>-1));
        }else{
            //return vendorDto
            console.log('no search branch')
            console.log(vendorArray)
            results = vendorArray;
        }
        //paginate

        return(
            //Table
            <React.Fragment>
                <h1>Vendor List</h1>
                <button className = "btn btn-primary" type = "button" onClick = {()=>handleOnClickNewVendor()}>New Vendor</button>
                <VendorViewTable funct={funct} data = {results} onSortClick = {onSortChange} />
                <Modal handleClose = {handleCancel} show = {popUpToggle}>
                    {console.log(`popUpToggle ${popUpToggle}`)}
                    <VendorForm vendorId = {vendorIdForModal} handleSave = {handleSave} handleCancel = {handleCancel} />
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

export default VendorView;