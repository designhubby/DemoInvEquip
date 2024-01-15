import React,{version , useEffect, useState, useRef} from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import PropTypes from "prop-types";

import TranslatePropertyKeys from "../../util/translatePropertyKeys";
import { useToasts } from "react-toast-notifications";
import { Constants } from './../common/constants';
import { Modal } from "../common/Modal";
import { SingleDataTypeViewTable } from './../subcomponents/SingleDataTypeViewComponents/SingleDataTypeViewTable';
import { TextField } from "../common/form";
import { confirmAlert } from 'react-confirm-alert'; // Import
import 'react-confirm-alert/src/react-confirm-alert.css'; // Import css


function SingleDataTypeView({GetAllDataTypeDtos, GetAllDataTypeDtosParameter, translatePrefixString, dataTypeLabel, columnNames,  apiDeleteId, formDetail, searchFields, extendedMethods, extendedSearchTypes, DisableNewButton, children}){
    
    
    const {addToast} = useToasts();
    const [dataTypeArray, setDataTypeArray] = useState([]);
    const [searchFilter, setSearchFilter] = useSearchParams();
    const [dataLoaded, setDataLoaded] = useState(false);
    const navigate = useNavigate();
    const [popUpToggle, setPopUpToggle] = useState(false);
    const [dataTypeIdForModal, setDataTypeIdForModal] = useState(1);

    async function GetData(){
        //get all devicetypes from API
        console.log(`GetData`)
        let data = GetAllDataTypeDtosParameter ? await GetAllDataTypeDtos(GetAllDataTypeDtosParameter): await GetAllDataTypeDtos();
        data = TranslatePropertyKeys(data, translatePrefixString);
        await Promise.resolve(setDataTypeArray(data));

        await Promise.resolve(setDataLoaded(true));

    }

    useEffect(()=>{
        GetData();
    },[])

    function reload(){window.location.reload();}

    function handleOnClickNew(){
        setDataTypeIdForModal("new");
        setPopUpToggle(!popUpToggle);
    }

    function handleCancel(){
        setPopUpToggle(false);
    }

    function handleSave(objectDto, error){
        setPopUpToggle(false);
        console.log(`objectDto`)
        console.log(objectDto)
        if(error){
            //do nothing
        }else{
            //modify the data array for the specific record id
            if(objectDto.Id >0){
                console.log(`update branch`)
                console.log(`objectDto`)
                console.log(objectDto)

                const tempIndex = dataTypeArray.findIndex(indiv=>indiv.Id == objectDto.Id);
                console.log(`tempIndex`);
                console.log(tempIndex);
                const _newArr = [...dataTypeArray]
                _newArr[tempIndex] = objectDto; // ids chg but names not chg
                console.log(`_newArr`)
                console.log(_newArr)
                setDataTypeArray(_newArr);
                
            }else{
                console.log('reload branch')
                reload();
            }
        }

        
    }

    async function handleSearchTermChange(e){
        const value = e.target.value;
        console.log(`value`)
        console.log(value)
        searchFilter.set("dataTypeName", value);
        setSearchFilter(searchFilter);
    }

    function onHandleEditClick(actionkey){
        console.log(`Edit Function ${actionkey}`)
        console.log(`PopUpToggle ${popUpToggle}`)
        setDataTypeIdForModal(actionkey);
        setPopUpToggle(true);
        
        return null;
    }
//Delete Functions
    const [showDeleteModal, setShowDeleteModal] = useState(false);
    const handleCloseDeleteModal = ()=>{
        setExtendedSearchFields(true)
    }

    const DeleteDialogOptions =(_actionKey)=>{
        return{

        title: 'Confirm to submit',
        message: 'Are you sure to do this.',
        buttons: [
                {
                label: 'Yes',
                onClick: ()=>onHandleDeleteConfirm(_actionKey)
                },
                {
                label: 'No',
                }
            ]
        }
    }
    function onHandleDeleteClick(actionkey){
        console.log(`actionkey`)
        console.log(actionkey)
        confirmAlert(DeleteDialogOptions(actionkey));
    }

    async function onHandleDeleteConfirm(actionkey){
        console.log(`Delete Function ${actionkey}`)
        try{
            await apiDeleteId(actionkey); //should there be a variable to consume the results for error to catch?
            const data = dataTypeArray.filter(indiv => indiv.Id != actionkey);
            setDataTypeArray(data);
            addToast("Deleted", {appearance : 'info', placement : 'top-center'})
            handleCancel(); //Is this extraneous?

        }catch(error){
            console.log(`error`);
            console.error(error);
            // console.log(`error.status`);
            console.error(error.status);
            addToast(error.data.title ?? "undefined error", {appearance : 'error', placement : 'top-center'})
            
        }

    }

    
    function btnDisplayStatus(actionkey){
        //console.log(`Display Function ${actionkey}`)
        return "Delete";
    }
//
    const [extendedSearchFields, setExtendedSearchFields] = useState();


    const handleOnExtendedSearchFieldsChange = (e)=>{
        const name = e.target.dataset.statekey;
        const value = e.target.value;
        console.log(`name` + " " + `value`)
        console.log(name + " " + value)
        //setSelectedSearchDepartmentId(value);
        if(value == 0){
            setExtendedSearchFields(null);
        }else{
            const _extendedSearchField = 
                {[name]: value}
            
            if(extendedSearchFields){
                //find object with same key in array
                //SAME: replace value
                //NOT: add key / value
                const matchingIndex = extendedSearchFields.findIndex(indiv=>{
                    const fieldKey = Object.keys(indiv);
                    return fieldKey == name;
                })
                console.log(`matchingIndex`)
                console.log(matchingIndex)
                if(matchingIndex > -1){
                    extendedSearchFields[matchingIndex] = {[name] : value};
                }
                const newArray = [...extendedSearchFields];
                console.log(`existing Exists`);
                console.log(newArray);
                setExtendedSearchFields(newArray);
            }else{
                console.log(`nonexisting`);
                console.log(_extendedSearchField);
                setExtendedSearchFields([_extendedSearchField]);
            }
        }
    }

    function FilterByExtendedSearchFields(_dataTypeArray){
        /*
            [
                {fieldOne : value},
            ],
            [
                {fieldTwo : value},
            ]
        */
        let results = [];
        console.log('extendedSearchFields');
        console.log(extendedSearchFields);
        if(extendedSearchFields){
            _dataTypeArray.forEach(indivRecordEntry=>{

                if(extendedSearchFields.every(indivSearchField=>{
                        const [searchKey, searchValue] = Object.entries(indivSearchField).flat();
                        const isNumber = parseFloat(indivRecordEntry[searchKey]) == indivRecordEntry[searchKey]
                        if(isNumber){
                            if(indivRecordEntry[searchKey]== searchValue){
                                //if(indivRecordEntry[searchKey].toLowerCase().indexOf(searchValue.toLowerCase()) > -1){
                            
                            return true;  
                            }
                        }else{
                            if(indivRecordEntry[searchKey].toLowerCase().indexOf(searchValue.toLowerCase()) > -1){
                                return true;  
                            }
                        }
                    })
                  ){
                      results.push(indivRecordEntry)
                    }
            })
            return results;
        }
        return _dataTypeArray;
    }

    function showExtendedSearchFieldCurrentValue(fieldname){
        if(extendedSearchFields){
            const [fieldset] = extendedSearchFields.filter(indiv=>{
                const key = Object.keys(indiv);
                if(key == fieldname){
                    return true;
                }
            })
            const fieldValue = fieldset[fieldname];
            console.log(fieldValue)
            return fieldValue;

        }else{
            console.log(`0`)
            return 0;
        }
    }

    function RenderView(){
        const searchTerm = searchFilter.get("dataTypeName");
        const funct = {
            handleSearchTermChange: handleSearchTermChange,
            btnDisplayStatus: btnDisplayStatus,
            onHandleEditClick: onHandleEditClick,
            onHandleDeleteClick: onHandleDeleteClick,
            handleOnExtendedSearchFieldsChange: handleOnExtendedSearchFieldsChange,
            extendedSearchFields: extendedSearchFields,
            showExtendedSearchFieldCurrentValue:showExtendedSearchFieldCurrentValue,
            extendedMethods: extendedMethods,
        }
        const singleDataTypeObjs = {
            handleSave: handleSave,
            handleCancel: handleCancel,
            onHandleDeleteClick: onHandleDeleteClick,
            dataTypeIdForModal: dataTypeIdForModal,

        }
        let results = [];

        //extended searchfield
        let extendedSearchFieldResults = FilterByExtendedSearchFields(dataTypeArray);

        if(searchTerm){
            //filter results to searchTerm
            console.log('search branch')
            if(searchFields){
                searchFields.forEach(indivSearchField => {
                    const tempResult = extendedSearchFieldResults.filter(indiv=> (indiv[indivSearchField].toLowerCase().indexOf(searchTerm.toLowerCase())>-1));
                    results.push(...tempResult);
                })
            }else{
                results = extendedSearchFieldResults.filter(indiv=> (indiv.Name.toLowerCase().indexOf(searchTerm.toLowerCase())>-1));

            }
        }else{
            //return deviceTypeArrayDto
            console.log('no search branch')
            results = extendedSearchFieldResults;
        }


        //paginate

        return(
            //Table

            <React.Fragment>
                <h1>{dataTypeLabel} List</h1>
                {!DisableNewButton && <button className = "btn btn-primary" type = "button" onClick = {()=>handleOnClickNew()}>New {dataTypeLabel}</button>}
                {children && (children(funct) ?? null)}
                <TextField placeHolder ={dataTypeLabel} label ={`Search ${dataTypeLabel}`} statekey ={null} onChange = {handleSearchTermChange} value = {searchTerm} key ={dataTypeLabel}/>
                
                <SingleDataTypeViewTable funct={funct} data = {results} columnNames={columnNames} searchMode={searchTerm+extendedSearchFields ?? ""}/>

                <Modal handleClose = {handleCancel} show = {popUpToggle}>
                    {(modalInject)=> formDetail ? formDetail(singleDataTypeObjs, modalInject) : null}
                    
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

export default SingleDataTypeView;

SingleDataTypeView.propTypes ={
    searchFields: PropTypes.array,
    extendedSearchTypes: PropTypes.object,
};