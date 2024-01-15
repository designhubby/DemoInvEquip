import React, {useState, useEffect, useRef } from "react";
import _ from 'lodash';
import { useSearchParams, useNavigate, useParams } from "react-router-dom";
import { useToasts } from "react-toast-notifications";
import { confirmAlert } from 'react-confirm-alert'; // Import
import 'react-confirm-alert/src/react-confirm-alert.css'; // Import css

import {GetDeviceByFilteredQuery, PostPersonDevice} from '../../service/DataServicePersonDevice';
import { DeleteDeviceDataById } from "../../service/DataServiceDevice";

import { DeviceType, Constants } from "../common/constants";
import { Pagechooser } from "../common/pagechooser";
import { DeviceListViewTable } from "../subcomponents/DeviceViewComponents/DeviceListViewTable";
import {DeviceListAddTable} from "../subcomponents/DeviceViewComponents/DeviceListAddTable";
import { DeviceViewTop } from "../subcomponents/DeviceViewComponents/DeviceViewTop";
import { paginate } from './../../util/pagination';
import {GetAllDeviceTypeDtos} from '../../service/DataServiceDeviceType';
import { DeviceFilterPanel } from "../subcomponents/DeviceViewComponents/DeviceFilterPanel";
import { TextField, SelectField,GeneralButton } from './../common/form';
import TranslatePropertyKeys from '../../util/translatePropertyKeys';
import { PersonDeviceDeviceHistory } from './../subcomponents/PersonDeviceViewComponents/PersonDeviceDeviceHistory';
import { Modal } from "../common/Modal";
import { GetAllHwModelDtos } from "../../service/DataServiceHwModel";

export function DeviceView(props){

    //Title
    //Search: Filters ( Device Type, Retired, Active Rental ) BackEnd = Query Objects
    //Add, Delete
    //Device Table: ( Edit, View Owners ) 
    ////pagination 

    const {addToast} = useToasts();

    const [searchParams, SetSearchParams] = useSearchParams();
    const navigate = useNavigate();

    //Constants
    const pageActionEnum = {
        view : 'view',
        edit: 'edit'
    }

    //Initial Conditions
    const [pageAction, setPageAction] = useState();
    const [personId,setPersonId] = useState();
    const [popUpToggle, setPopUpToggle] = useState(false);

    //View Data
    const [DeviceList, setDeviceList] = useState();
    const [dataHasLoaded, setDataHasLoaded] =useState(false);
    const [DeviceTypes, setDeviceTypes] = useState();
    const [HwDeviceModels, setHwDeviceModels] = useState({
        hwModelId:0,
        hwModelName: "",
        })
    const [DeviceTypeFilterId, setDeviceTypeFilterId] = useState();
    const [DeviceId, setDeviceId] = useState();
    const [showRetired, setShowRetired] = useState();
    const [showActivelyRented, setShowActivelyRented] = useState();

    //Set Filter
    const [RetiredFilter, setRetiredFilter] = useState(false);
    const [vendorId, setVendorId] = useState('');
    const [ActiveRentalFilter, setActiveRentalFilter] = useState(true);
    const [DeviceTypeFilter, setDeviceTypeFilter] = useState({
        DeviceType : "all", // all , PC , Printer, Monitor, 
    });
    const [DeviceHwModelIdFilter, setDeviceHwModelIdFilter] = useState("");
    const [searchTerm, setSearchTerm] = useState("");

    //Post Data
    const [personDeviceLinkArray, setPersonDeviceLinkArray] = useState([]);

    
    //pagination variables
    const [sortDirection, setSortDirection] = useState("asc");
    const [sortColumnMode, setSortColumnMode] = useState("_id");

    const entriesPerPage = useRef(5);
    const [currentPage, setCurrentPage] = useState(1);


    useEffect(()=>{

        LoadDevices();

    },[])

    useEffect(()=>{

        LoadDevices();

    },[showActivelyRented,showRetired,DeviceHwModelIdFilter])


    const LoadDevices = async () =>{

        const personId = searchParams.get("personId");
        const action = searchParams.get("action");
        setPersonId(personId);
        setPageAction(action);

        const DeviceTypeFilterNameFromSearchParams = searchParams.get("devicetypenamefilter");
        

        console.log('LoadDevices Running');
        console.log('DeviceTypeFilterNameFromSearchParams');
        console.log(DeviceTypeFilterNameFromSearchParams)
        let _DeviceTypes = await GetAllDeviceTypeDtos();

        let _HwDeviceModels = await GetAllHwModelDtos();
        _HwDeviceModels = TranslatePropertyKeys(_HwDeviceModels,'hwModel'); //remove key's prefix
        _HwDeviceModels.unshift({
            Id : 0,
            Name: "All"
        })
        setHwDeviceModels(_HwDeviceModels)

        _DeviceTypes = TranslatePropertyKeys(_DeviceTypes,'deviceType'); //remove key's prefix
        
        const DeviceTypeFilterId = _DeviceTypes.find(indiv => indiv.Name == DeviceTypeFilterNameFromSearchParams)?.Id;
        //const DeviceTypeFilterFromSearchParams = searchParams.get("devicetypefilter");
        console.log(`DeviceTypeFilterId`);
        console.log(DeviceTypeFilterId);
        const RetiredFilterFromSearchParams = searchParams.get("retiredfilter");
        setRetiredFilter(RetiredFilterFromSearchParams);
        const ActiveRentalFilterFromSearchParams = searchParams.get("activerentalfilter");
        setActiveRentalFilter(ActiveRentalFilterFromSearchParams);

        const DeviceHwModelIdSearchParams = searchParams.get("hwmodelidfilter");
        setDeviceHwModelIdFilter(DeviceHwModelIdSearchParams);
        

        let Loader = GetDeviceByFilteredQuery(DeviceTypeFilterId, RetiredFilterFromSearchParams,ActiveRentalFilterFromSearchParams,DeviceHwModelIdSearchParams);

        const devices = await Loader;
        console.log(devices);
        
        _DeviceTypes.unshift({
            Id : "",
            Name :"All",
        })
        
        setDeviceTypeFilterId(DeviceTypeFilterId || 0);
        setDeviceTypes(_DeviceTypes);
        setDeviceList(devices);
        setDataHasLoaded(true);
    }

    const handleSort=(columnName)=>{
        if(sortColumnMode == columnName){
            if(sortDirection == "asc"){
                setSortDirection("desc");
            }else{
                setSortDirection("asc");
            }
        }else{
            setSortColumnMode(columnName);
            setSortDirection("asc");
        }
        return null;
    }

    const handleDeviceAdd=async (actionkey)=>{
        //if actionkey already exists in 'personDeviceLinkArray'(a temporary cart) then remove, else add
        const actionkeyObj = {personId: personId,
            deviceId: actionkey}
        const allreadyadded = personDeviceLinkArray.some(indiv=> indiv.personId == actionkeyObj.personId && indiv.deviceId == actionkeyObj.deviceId)
        if(allreadyadded){
            const newArray = personDeviceLinkArray.filter(indiv=> indiv.personId != actionkeyObj.personId || indiv.deviceId != actionkeyObj.deviceId)
            setPersonDeviceLinkArray(newArray)
        }else{
            setPersonDeviceLinkArray((prev)=> [...prev, actionkeyObj])
        }
        console.log(actionkey)
        console.log(pageActionEnum.view)
        console.log(personDeviceLinkArray);/* 
        const newDeviceList = DeviceList.filter((indiv)=>{
            return indiv.deviceId !== actionkey
        })
        setDeviceList(newDeviceList) */
        //console.log(await PostPersonDeviceAssociation(personDeviceLinkArray));
    }
    const handleButtonDisplay = (id)=>{ //controls whether the button is disable or not, after a device is 'added'
        //if id exists in list of picked id list then show button 'remoove', else 'add' button
        const deviceIdAdded = personDeviceLinkArray.some(indiv => indiv.deviceId == id);
        if(deviceIdAdded){
            return "Remove" 
        }else{
            return "Add"
        }

    }

    const onSubmitAddDevices = async () =>{
        const result = await PostPersonDevice(personDeviceLinkArray);
        navigate(`/main/PersonDeviceView/${personId}`)
    }
    
    const handlePageChange = (indivPageNumber) =>{
        setCurrentPage(indivPageNumber);

    }

    const handleOnFilterDeviceTypeChange = async (e, labelfor_testing)=>{
        const valueId = parseInt(e.target.value);
        setDeviceTypeFilterId(valueId);
        const searchFilterTypeName = DeviceTypes.find(indiv => indiv.Id == valueId)?.Name;
        console.log('searchFilterTypeName')
        console.log(searchFilterTypeName);
        searchParams.set("devicetypenamefilter", searchFilterTypeName);
        SetSearchParams(searchParams);
        //search based on filter
        
        const filteredDeviceList = await GetDeviceByFilteredQuery(valueId, RetiredFilter,ActiveRentalFilter,DeviceHwModelIdFilter);
        setDeviceList(filteredDeviceList);

    }

    const handleOnSearchDeviceNameChange = async (e,labelfor_testing) =>{
        const deviceNameValue = e.target.value;
        setCurrentPage(1);
        setSearchTerm(deviceNameValue);
    }
    const handleOnSearchHwModelChange = async (e,labelfor_testing) =>{
        const index = e.nativeEvent.target.selectedIndex
        var deviceHWModelText =e.nativeEvent.target[index].text;
        const id = e.target.value;
        if(deviceHWModelText == 'All'){
            searchParams.set("hwmodelidfilter", "");
            SetSearchParams(searchParams);
        }else{
            searchParams.set("hwmodelidfilter", id);
            SetSearchParams(searchParams);
        }
        setDeviceHwModelIdFilter(id);
        //const filteredDeviceList = await GetDeviceByFilteredQuery(DeviceTypeFilterId, RetiredFilter,ActiveRentalFilter,DeviceHwModelIdFilter);
        //setDeviceList(filteredDeviceList);
        setCurrentPage(1);
    }
    const handleClickBtnNewDevice= ()=>{
        navigate(Constants.addNewDeviceUrl);
    }

    const handleOnDeviceIdChange = (deviceId) =>{
        setPopUpToggle(!popUpToggle);
        setDeviceId(deviceId);
    }
    //Delete functions:

    const DeleteDialogOptions =(_actionKey)=>{
        return{

        title: 'Confirm to submit',
        message: 'Are you sure to do this.',
        buttons: [
                {
                label: 'Yes',
                onClick: ()=>onDeleteConfirmed(_actionKey)
                },
                {
                label: 'No',
                }
            ]
        }
    }
    const handleOnDeviceDelete = async (actionkey)=>{
        console.log(`actionkey`)
        console.log(actionkey)
        confirmAlert(DeleteDialogOptions(actionkey));
    }
    const onDeleteConfirmed = async (deviceId)=>{
        try{
            console.log("Deleting onDeleteConfirmed")
            const response = await DeleteDeviceDataById(deviceId);
            const newDeviceList = DeviceList.filter(indiv=>indiv.deviceId != deviceId);
            console.log(newDeviceList)
            setDeviceList(newDeviceList);
            addToast("Deleted", {appearance : 'info', placement : 'top-center'})

        }catch(error){
            console.log(`error`);
            console.error(error);
            // console.log(`error.status`);
            console.error(error.status);
            addToast(error.data.title ?? "undefined error", {appearance : 'error', placement : 'top-center'})

        }
    }
    const displayRetireLabel = ()=>{
        if(showRetired){
            return 'btn btn-primary disabled'
        }else{
            return 'btn btn-primary'
        }
    }
    function handleCancel(){
        setPopUpToggle(false);
    }

    function handleShowRetiredDevices(event){
        if (event.target.checked) {
            console.log('Checkbox is checked');
            searchParams.set("retiredfilter", true);
            searchParams.set("activerentalfilter", false);
            
            SetSearchParams(searchParams)
            setShowActivelyRented(false) 
            setShowRetired(true)
            setCurrentPage(1);
          } else {
            console.log('Checkbox is NOT checked');
            searchParams.set("retiredfilter", false);
            SetSearchParams(searchParams)
            setShowRetired(false)
            setCurrentPage(1);
          }
    }

    function handleShowActivelyRented(event){
        if (event.target.checked) {
            console.log('Checkbox is checked');
            searchParams.set("activerentalfilter", true);
            searchParams.set("retiredfilter", false);
            SetSearchParams(searchParams)
            setShowRetired(false)
            setShowActivelyRented(true)
            setCurrentPage(1);
          } else {
            console.log('Checkbox is NOT checked');
            searchParams.set("activerentalfilter", false);
            SetSearchParams(searchParams)
            setShowActivelyRented(false)
            setCurrentPage(1);
          }
    }

    const renderDeviceListView = () =>{
        if(dataHasLoaded){
            let nameFilteredList = DeviceList;
            if(searchTerm.length >0){
                nameFilteredList = DeviceList.filter((indiv)=>{
                if(indiv.serviceTag.toLowerCase().indexOf(searchTerm.toLowerCase()) > -1){
                    return true;
                }
                if(indiv.deviceName.toLowerCase().indexOf(searchTerm.toLowerCase())> -1){
                    return true;
                }
                if(indiv.hwModelName.toLowerCase().indexOf(searchTerm.toLowerCase())> -1){
                    return true;
                }
            })
            }
            const sortedList = _.orderBy(nameFilteredList,sortColumnMode,sortDirection);
            const paginatedlist = paginate(sortedList,entriesPerPage.current, currentPage);
            
            let pageTitle = "";
            if(pageAction == pageActionEnum.view){
                pageTitle = 'Device View';
            }else{
                pageTitle = 'Add Device';
            }
            const functionObject = {
                handleDeviceAddFunc : handleDeviceAdd,
                handleButtonDisplayFunc: handleButtonDisplay, 
            }
            const functionObjectView={
                handleOnDeviceIdChange: handleOnDeviceIdChange,
                handleOnDeviceDelete:handleOnDeviceDelete,
                displayRetireLabel:displayRetireLabel,
            }

            return (
                <React.Fragment>
                    <DeviceViewTop text={pageTitle} />
                    <DeviceFilterPanel>
                        <TextField placeHolder ="Search Device Name or Service Tag" label="Search Device Name" onChange = {handleOnSearchDeviceNameChange} value ={searchTerm} key ="devicename"/>    
                        {console.log(`DeviceTypeFilterId`)}
                        {console.log(DeviceTypeFilterId)}
                        <SelectField options={DeviceTypes} onChange={handleOnFilterDeviceTypeChange} value ={DeviceTypeFilterId} label ="Search Device Type"/>
                        <SelectField options={HwDeviceModels} onChange={handleOnSearchHwModelChange} value ={DeviceHwModelIdFilter} label ="Search Hw Model"/>
                        <GeneralButton label="New Device" handleOnClick={handleClickBtnNewDevice}/>
                    </DeviceFilterPanel>
                    <React.Fragment>
                        {
                        pageAction == pageActionEnum.view &&<> 
                        <div className="form-check form-switch">
                        <input className="form-check-input" type="checkbox" id="flexSwitchCheckDefault" onChange={handleShowRetiredDevices} checked={showRetired}/>
                        <label className="form-check-label" for="flexSwitchCheckDefault">Show Only Retired Devices</label><br/>
                        <input className="form-check-input" type="checkbox" id="flexSwitchCheckDefault" onChange={handleShowActivelyRented}  checked={showActivelyRented}/>
                        <label className="form-check-label" for="flexSwitchCheckDefault">Show Only Actively Rented Devices</label>
                        </div>
                        <DeviceListViewTable DeviceListData = {paginatedlist} onSortClick={handleSort} onEdit = {functionObjectView}/>
                        
                        <Modal handleClose = {handleCancel} show = {popUpToggle}>
                            {(modalInject)=> <PersonDeviceDeviceHistory deviceId = {DeviceId}/>}
                    
                        </Modal>
                        </>
                        
                        }
                    </React.Fragment>
                    <React.Fragment>
                        {
                        pageAction == pageActionEnum.edit &&
                        <DeviceListAddTable DeviceListData = {paginatedlist} onSortClick={handleSort} onAdd={functionObject} onSubmit = {onSubmitAddDevices} labelSubmit = "Submit"/>
                        }
                    </React.Fragment>
                    <Pagechooser entriesPerPage = {entriesPerPage.current} allEntries = {DeviceList} currentPage = {currentPage} handlePageChange = {handlePageChange}/>
                </React.Fragment>
            )
        }else{
            return <div>Loading.......</div>
        }
    }




    return(
        <React.Fragment>
            {renderDeviceListView()}
        </React.Fragment>
    )

}