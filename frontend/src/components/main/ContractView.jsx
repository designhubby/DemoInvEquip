
import React, {useState, useRef} from 'react';
import { useSearchParams, useNavigate } from "react-router-dom";
import { useParams } from "react-router";
import { Constants } from './../common/constants';
import SingleDataTypeView from './SingleDataTypeView';
import { TextField,SubmitButton, GeneralButton, SelectField,DatePicker } from './../common/form';
import moment from 'moment';

import DataTypeForm from './../subcomponents/DataTypeComponents/DataTypeForm';
import { useEffect } from 'react';
import { GetAllVendorDataDtos } from '../../service/DataServiceVendor';
import TranslatePropertyKeys from '../../util/translatePropertyKeys';
import { GetAllContractsListDto,GetContractDataDtoById, PostContractDataByDto,PutContractDataByDto,DeleteContractDataById } from './../../service/DataServiceContract';





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
        label: "Vendor Name",
        datakey: "vendorName",
        visible: true,
        type: Constants.text,
    },  
    {
        label: "Start Date",
        datakey: "startDate",
        visible: true,
        type: Constants.date,
    },  
    {
        label: "End Date",
        datakey: "endDate",
        visible: true,
        type: Constants.date,
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
        label: "View Devices",
        actionKey: "Id",
        visible: true,
        type: Constants.button,
        action : (funct, actionkey)=>(
            <button type = "button" className = "btn btn-primary" onClick = {()=>funct.extendedMethods.onHandleViewDevices(actionkey)}>View Devices</button>
        )
    },

]

function ContractView(){

    const navigate = useNavigate();

    //const [dataTypeIdForModal, setDataTypeIdForModal] = useState(1);
    const APIGetAllDto = GetAllContractsListDto;
    const APIGetById = GetContractDataDtoById;
    const APIDeleteById = DeleteContractDataById;
    const APIPostDto = PostContractDataByDto;
    const APIUpdateDto = PutContractDataByDto;
    
    const dataTypeLabel = 'Contracts'
    const translatePrefixString = 'contract' 
    const typeName = "Name";
    const searchFields = ['Name','vendorName']

    // extended elements for Dataform
    

    const statekeyVendorId = "vendorId";
    const [vendorDtos, setVendorDtos] = useState({});

    const [startDate, setStartDate] = useState(new Date());
    const [endDate, setEndDate] = useState(new Date());


    const getData = async ()=>{

        
        //load VendorDto
        let temp_vendorDtos = await GetAllVendorDataDtos();
        const vendorPrefix = "vendor";
        temp_vendorDtos = TranslatePropertyKeys(temp_vendorDtos, vendorPrefix);
        //add dummy "choose one" option
        temp_vendorDtos.unshift({
            Id: 0,
            Name: "Choose a Vendor",
        })
        setVendorDtos(temp_vendorDtos);
        console.log('temp_vendorDtos')
        console.log(temp_vendorDtos)
    }

    useEffect(()=>{
        console.log('useeffect')
        getData();

    },[])

    const onHandleViewDevices = (contractId)=>{
        console.log("any happening here?")
        console.log(contractId);
        navigate(`/main/ContractView/OwnedDevices/${contractId}`);
    }
    const extendedMethods = {
        onHandleViewDevices: onHandleViewDevices
    }

    return (
        <>
        <SingleDataTypeView 
            GetAllDataTypeDtos = {APIGetAllDto} 
            translatePrefixString = {translatePrefixString}
            dataTypeLabel = {dataTypeLabel} 
            columnNames = {columnNames} 
            apiDeleteId = {APIDeleteById}
            searchFields = {searchFields}
            formDetail = {(singleDataTypeObjs, modalinject)=>(
                <DataTypeForm 
                label = {modalinject} 
                translatePrefixString = {translatePrefixString} 
                objectId = {singleDataTypeObjs.dataTypeIdForModal} 
                handleSave = {singleDataTypeObjs.handleSave} 
                handleCancel = {singleDataTypeObjs.handleCancel} 
                apiGetDtoById ={APIGetById} 
                apiPostDto= {APIPostDto} 
                apiUpdateDto= {APIUpdateDto}>
                    {(funct)=>
                        <>
                        
                        <h2>{dataTypeLabel} Detail</h2>

                        <TextField placeHolder = "Example: Contract ABC" label = {`${dataTypeLabel} Name`} onChange = {funct.handleOnChange} statekey = {typeName} value = {funct.objectDto.Name} key ={typeName}/>
                        <SelectField options ={vendorDtos} onChange ={funct.handleOnChange} value={funct.objectDto.vendorId} label={"vendor"} statekey={statekeyVendorId}/>
                        <DatePicker label={"Date Start"} selected = {funct.objectDto.startDate ? moment(funct.objectDto.startDate).toDate() : new Date()} onChange={(date) =>funct.handleOnChange(date)} statekey = {"startDate"} />
                        <DatePicker label={"Date End"} selected = {funct.objectDto.endDate ? moment(funct.objectDto.endDate).toDate() : null} onChange={(date) =>funct.handleOnChange(date)} statekey = {"endDate"} isClearable = {true} excludeDateIntervals ={[{start: new Date(0), end:moment(funct.objectDto.startDate).toDate()}]} />
                        <SubmitButton label = "Save"/> 
                        <GeneralButton label = "Reset" handleOnClick = {funct.handleReset}/>

                        </>
                    }
                </DataTypeForm>
                )}
                extendedMethods = {extendedMethods}
            />
        </>
    )

}

export default ContractView;