import React, {useEffect, useState, useRef, Component} from 'react';
import { useParams } from 'react-router';
import {useSearchParams, useNavigate } from "react-router-dom";
import Translate_ModelNames from "../../util/translatePropertyKeys";
import {GetContractDataDtosOwnByVendorByVendorId, GetContractDataDtosOwnByVendorByContractId} from '../../service/DataServiceVendor';

import {GetHwModelDataDtosByDeviceTypeId, GetAllDeviceTypeDtos} from '../../service/DataServiceDeviceType';

import {GetDeviceDataById, PutDeviceData,PostDeviceData} from "../../service/DataServiceDevice";

import {GetAllVendorDataDtos} from '../../service/DataServiceVendor';

import { TextField,EmailField, SelectField, SubmitButton,GeneralButton } from "../common/form";
import { Constants, DeviceType } from '../common/constants';


const DeviceForm = ({deviceId}) =>{

    const navigate = useNavigate();

    const [searchParams, setSearchParams] = useSearchParams();
    const {idDevice} = useParams();
    const [DeviceData, setDeviceData] = useState({});
    const [OriDeviceData, setOriDeviceData] = useState({});
    const [DeviceTypesData, setDeviceTypesData] = useState();
    const [HwModelsData, setHwModelsData] = useState();
    const [ContractsData, setContractsData] = useState();
    const [VendorsData, setVendorsData] = useState();
    const HwModelCleared = useRef(false);
    const [DataLoaded, setDataLoaded] = useState(false);


    const blankDeviceData =   {
        "deviceId": 0,
        "deviceName": "",
        "deviceTypeId": "",
        "hwModelId": 1,
        "serviceTag": "",
        "assetNumber": "",
        "notes": "",
        "contractId": 1,
        "vendorId": "",
      }

    //Device Name (text)
    //Device Model ( Device Type -> Device Model (searchable?))
    //Service Tag (text)
    //Asset Number (text)
    //Notes (text)
    //Contract Name - Start Date - End Date
    /*

    */

    useEffect(()=>{
        //if idDevice == 'new' then, else
        if(deviceId){
            LoadDeviceData(deviceId);
        }else{
            LoadDeviceData(idDevice);
        }
            

    },[])




    const LoadDeviceData = async (idDevice)=>{
        let _deviceData =Object.assign({}, blankDeviceData);
        let _DeviceTypesData = [];
        let _VendorsData = [];
        let _hwModelsData=[];
        let _ContractsData = [];

        ([_DeviceTypesData, _VendorsData] = await Promise.all([GetAllDeviceTypeDtos(),GetAllVendorDataDtos()]));
        
        if(!isNaN(idDevice)){
            _deviceData =   await GetDeviceDataById(idDevice);
            ([_hwModelsData, _ContractsData] = await Promise.all([GetHwModelDataDtosByDeviceTypeId(_deviceData.deviceTypeId),GetContractDataDtosOwnByVendorByContractId(_deviceData.contractId)]))
        }else{

            ([_hwModelsData, _ContractsData] = await Promise.all(
                    [
                        GetHwModelDataDtosByDeviceTypeId(DeviceType.UnassignedPlaceHolder), 
                        GetContractDataDtosOwnByVendorByVendorId(_VendorsData[Constants.VendorUnassignedArrayLocationForVendorId1].vendorId)
                    ]
                )
            )
        }
        


        _hwModelsData = Translate_ModelNames(_hwModelsData,'hwModel')
        _DeviceTypesData = Translate_ModelNames(_DeviceTypesData,'deviceType')
        _ContractsData = Translate_ModelNames(_ContractsData,'contract')
        _VendorsData=Translate_ModelNames(_VendorsData,'vendor')
        
        setDeviceData(_deviceData);
        setOriDeviceData(_deviceData);
        setHwModelsData(_hwModelsData);
        setDeviceTypesData(_DeviceTypesData);
        setVendorsData(_VendorsData);
        setContractsData(_ContractsData);
        setDataLoaded(true);
    }





    const handleOnFormChange = (e, label_fortesting)=>{
        const value = e.target.value;
        const StateKey = e.target.dataset.statekey;
        setDeviceData((prevState)=>({
            ...prevState,
            [StateKey]:value,
        }))
        console.log(`Changed Key: ${StateKey}, Changed Value: ${value}`);
    }

    const handleOnSelector_DeviceType_Change = async (e, label_fortesting)=>{
        const value = e.target.value;

        console.log(`DeviceType value`);
        console.log(value);
        let HwModelList = await GetHwModelDataDtosByDeviceTypeId(value)
        HwModelList.unshift({
            "hwModelId": 0,
            "hwModelName": "Unassigned",
        })
        console.log(`HwModelList1`)
        console.log(HwModelList)
        HwModelList = Translate_ModelNames(HwModelList, 'hwModel');
        console.log(`HwModelList2`)
        console.log(HwModelList)
        setDeviceData((prevState)=>({
            ...prevState,
            deviceTypeId : value,
            hwModelId:0,
        }))
        setHwModelsData(HwModelList);
        return null;
    }

    const handleOnSelector_Vendor_Change = async (e, label_fortesting) =>{
        const value = e.target.value;
        //use vendorid to retrieve owned contracts
        let contractDataDtos = await GetContractDataDtosOwnByVendorByVendorId(value);
        contractDataDtos.unshift({
            contractId:1,
            contractName:"Unassigned",
            vendorId: 1,
        })
        contractDataDtos = Translate_ModelNames(contractDataDtos, 'contract');
        setDeviceData((prevState)=>({
            ...prevState,
            vendorId : value,
        }
        ));
        setContractsData(contractDataDtos);


    }
    const handleOnClickReset= async ()=>{
        
        setDeviceData(OriDeviceData);
        if(DeviceData.deviceId){
            const [HwModelList, contractDataDtos] = await Promise.all([GetHwModelDataDtosByDeviceTypeId(OriDeviceData.deviceId), GetContractDataDtosOwnByVendorByVendorId(OriDeviceData.vendorId)])
            setHwModelsData(HwModelList);
            setContractsData(contractDataDtos);

        }


    }

    const postBackData = (e)=>{
        e.preventDefault();
        const DevicePostBackData = {
            "deviceId": DeviceData.deviceId,
            "deviceName": DeviceData.deviceName,
            "hwModelId": DeviceData.hwModelId,
            "contractId": DeviceData.contractId,
            "serviceTag": DeviceData.serviceTag,
            "assetNumber": DeviceData.assetNumber,
            "notes": DeviceData.notes,
        }
        console.log('DevicePostBackData');
        console.log(DevicePostBackData);
        let apiresults;
        if(DevicePostBackData.deviceId >0){
            console.log('put request sent')
            apiresults = PutDeviceData(DevicePostBackData);
        }else{
            console.log('post request sent')
            apiresults = PostDeviceData(DevicePostBackData);
        }
        
        console.log(apiresults);
        navigate('/main/DeviceListView/?action=view')
        
    }


    


    const renderForm = () =>{

        if(DataLoaded){
            const KeyDeviceName = Object.keys(DeviceData)[1];
            const KeyDeviceTypeId = Object.keys(DeviceData)[2];
            const KeyHwModelId = Object.keys(DeviceData)[3]
            const KeyServiceTag = Object.keys(DeviceData)[4];
            const KeyAssetNumber = Object.keys(DeviceData)[5];
            const KeyNotes = Object.keys(DeviceData)[6];
            const KeyContract = Object.keys(DeviceData)[7];
            const KeyVendor = Object.keys(DeviceData)[8];

            return(
                <React.Fragment>
                    <TextField placeHolder ="Device Name" label="Device Name"  statekey={KeyDeviceName} onChange={handleOnFormChange} value = {DeviceData.deviceName} key="123"/>
                    <SelectField options = {DeviceTypesData} label = "Device Type" onChange={handleOnSelector_DeviceType_Change} value = {DeviceData.deviceTypeId} key ="5464"/>
                    <SelectField options = {HwModelsData} onChange = {handleOnFormChange} label = "HwModels" value={DeviceData.hwModelId} statekey = {KeyHwModelId} key="HwModelSelector"/>
                    <TextField placeHolder ="ServiceTag" label = "ServiceTag" statekey={KeyServiceTag} onChange={handleOnFormChange} value = {DeviceData.serviceTag} key ="ServiceTag"/>
                    <TextField placeHolder ="Asset Number" label = "Asset Number" statekey={KeyAssetNumber} onChange={handleOnFormChange} value = {DeviceData.assetNumber} key ="AssetTag"/>
                    <TextField placeHolder ="Notes" label = "Notes" statekey={KeyNotes} onChange={handleOnFormChange} value = {DeviceData.notes} key ="Notes"/>  
                    <SelectField options = {VendorsData} label ="Vendors" value= {DeviceData.vendorId} onChange={handleOnSelector_Vendor_Change} key="vendorid"/>
                    <SelectField options = {ContractsData} label = "Contracts" value = {DeviceData.contractId} onChange = {handleOnFormChange} statekey= {KeyContract} key="contractid"/>
                    <SubmitButton label = "Save"/>
                    <GeneralButton label = "Reset" handleOnClick = {handleOnClickReset}/>
                    <GeneralButton label ="Cancel" handleOnClick={()=>navigate(-1)} />
                </React.Fragment>
            )
        }else{
            <div>Loading....</div>
        }
    }


    return(
        <React.Fragment>
            <h1>Device Data Form</h1>
            <form onSubmit={postBackData}>
            {renderForm()}
            </form>

        </React.Fragment>
    )

}

export default DeviceForm;