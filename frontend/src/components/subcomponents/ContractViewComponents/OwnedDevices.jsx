import React, {useState, useRef,useEffect} from 'react';
import { useSearchParams, useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { Constants } from './../../common/constants';
import SingleDataTypeView from './../../main/SingleDataTypeView';
import { TextField,SubmitButton, GeneralButton, SelectField,ReactDatePicker } from './../../common/form';
import moment from 'moment';


import DataTypeForm from './../DataTypeComponents/DataTypeForm';
import { GetAllVendorDataDtos } from './../../../service/DataServiceVendor';
import { GetAllContractsListDto,GetContractDataDtoById, GetAllContractDevicesByContractId, PostContractDataByDto,PutContractDataByDto,DeleteContractDataById } from './../../../service/DataServiceContract';

import { SingleDataTypeViewTable } from './../SingleDataTypeViewComponents/SingleDataTypeViewTable';
import { Modal } from '../../common/Modal';
import DeviceForm from '../DeviceForm';
import { OwnedDeviceTblColumnsColumnNames } from './OwnedDeviceTblColumns';
import { ContractViewTopPanel } from './ContractViewTopPanel';
import TranslatePropertyKeys, { TranslatePropertyKeysToDto } from './../../../util/translatePropertyKeys';
import { PersonDeviceDeviceHistory } from './../PersonDeviceViewComponents/PersonDeviceDeviceHistory';





//Top Panel
//Title: Contract Name
//Start / End Dates
//Search Field?
//searchParams : device name, DeviceTypeName, Service Tag, HwModel (inside: DeviceDto)
//Table: device name, DeviceTypeName, Service Tag, HwModel


export function OwnedDevices({contractId}){

    const navigate = useNavigate();
    //Search only a subset of Fields
    const searchableFields = ['Name','deviceTypeName','serviceTag','hwModelName']

    const {idContract} = useParams();
    const [searchParams, setSearchParams] = useSearchParams();
    const contractIdProp = contractId;
    //features:  device name, DeviceTypeName, Service Tag, HwModel (inside: DeviceDto)
 
    //Data:
    const [contractInfo, setContractInfo] = useState();
    const [allDevices, setAllDevices] = useState([]);

    
    const [filterableFields, setFilterableFields] = useState(searchableFields);
    const [dataLoaded, setDataLoaded] = useState(false);

    //Render Status:
    const [searchBox, setSearchBox] = useState('');
    const [currentDeviceId, setCurrentDeviceId] = useState(``);
    const ModalContent_Enum = {
        DeviceEdit : 'DeviceEdit',
        DeviceHistory  : 'DeviceHistory',
    }
    
    const [showModalContent, setShowModalContent] = useState("");
    const [popUpToggle, setPopUpToggle] = useState(false);



    //load data: contractid -> DeviceDto
    async function LoadData(){
        const contractIdSearchParams = searchParams.get('contractId');
        
        
        let contractId;
        let allDevicesByContractId;
        if(idContract){
            contractId = idContract;
        }else if (contractIdSearchParams){
            contractId = contractIdSearchParams;
        }else{
            contractId = contractIdProp;
        }
        const contractDto = await GetContractDataDtoById(contractId);
        await Promise.resolve(setContractInfo(contractDto));
        allDevicesByContractId = await GetAllContractDevicesByContractId(contractId);
        allDevicesByContractId = TranslatePropertyKeys(allDevicesByContractId,'device')
        await Promise.resolve(setAllDevices(allDevicesByContractId));
        await Promise.resolve(setDataLoaded(true));

         
    }

    useEffect(()=>{
        LoadData();
    },[])

    const onHandleSearchChange = (e)=>{
        console.log('is this running')
        const value = e.target.value;
        setSearchBox(value);
        searchParams.set("dataTypeSearchTerm", value);
        setSearchParams(searchParams);

    }

    const handleCancel =()=>{
        setPopUpToggle(false);
    }
    const onHandleEditClick = (deviceId)=>{
        setCurrentDeviceId(deviceId);
        setShowModalContent(ModalContent_Enum.DeviceEdit);
        setPopUpToggle(true);
    }
    const onHandleShowHistoryClick=(deviceId)=>{
        console.log("any happening here?")
        console.log(deviceId);
        setCurrentDeviceId(deviceId);
        setShowModalContent(ModalContent_Enum.DeviceHistory);
        setPopUpToggle(true);
        //navigate(`/main/PersonDeviceViewDeviceHistory/${deviceId}`);
    }

    const renderView = ()=>{

        const funct = {
            onHandleEditClick:onHandleEditClick,
            onHandleShowHistoryClick: onHandleShowHistoryClick,
        }
        const contractTopPanellines = [
            [//Line 1
                ['Contract Name: '],[contractInfo.contractName]
            ],
            [//Line 2
                ['Vendor Name: '],[contractInfo.vendorName]
            ]
        ]
        //Custom Search Algorithm: if searchterm exists (reason why standard template wasn't used)
        //allows limiting the search to a subset of available fields
        const searchTerm = searchParams.get("dataTypeSearchTerm");
        
        let results = allDevices;

        if(searchTerm){
            //filter output by filter term
            results = results.filter(indivDto =>{
                const allFieldKeys = Object.keys(indivDto);
                if(filterableFields.every(indivfilterableFields=>allFieldKeys.includes(indivfilterableFields))){ //check if each declared searchable Fields (filterableFields) exist within the Dto's available Fields (allFieldKeys) 
                    return filterableFields.some(indivSearchField =>  indivDto[indivSearchField].includes(searchTerm))
                }else{
                    console.log(`alt search branch`)
                    return indivDto
                }
            })
        }else{
            //show all data
            console.log(`no search branch`)
            results = results
        }
        return(
            <>
            <ContractViewTopPanel title = "Contract Detail" lines = {contractTopPanellines}/>
            <TextField placeHolder = {'Search'} label = {'Search Devices'} onChange = {onHandleSearchChange} value = {searchBox}/>
            <SingleDataTypeViewTable funct={funct} data = {results} columnNames={OwnedDeviceTblColumnsColumnNames}/>
{/* Modal for showing 1.) Device Edit Form 2.) User History*/}
            <Modal handleClose = {handleCancel} show = {popUpToggle}> 
            {
                showModalContent == ModalContent_Enum.DeviceEdit ? ()=><DeviceForm deviceId = {currentDeviceId}/> : ()=><PersonDeviceDeviceHistory deviceId = {currentDeviceId}/>
            }
            </Modal>
            </>
        )
    }

    return(
        <>
        {dataLoaded && renderView()}
        </>
    )


}