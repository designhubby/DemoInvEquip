import React,{useEffect, useState, useRef} from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { Table } from "../../common/table"; 
import { Constants } from './../../common/constants';
import _ from 'lodash';

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
        label: "Edit",
        actionKey: "Id",
        visible: true,
        type: Constants.button,
        action : (funct, actionkey)=>(
            <button type = "button" className = "btn btn-primary" onClick = {()=>funct.onHandleEditClick(actionkey)}>Edit</button>
        )
    },
    {
        label: "Delete",
        actionKey: "Id",
        visible: true,
        type: Constants.button,
        action : (funct, actionkey)=>(
            <button type = "button" className = "btn btn-primary" onClick = {()=>funct.onHandleDeleteClick(actionkey)}>{funct.btnDisplayStatus(actionkey)}</button>
        )
    },

]

export const VendorViewTable = ({funct, data})=>{

    const _datakey = columnNames[2].datakey;

    const [sortColumn, setSortColumn] = useState(_datakey);
    const [sortDirectionAsc, setSortDirectionAsc] = useState(true);

    function onSortClick(e){
         const newSortColumn = e;
         if(newSortColumn == sortColumn){
             console.log(`block 1`)
            setSortDirectionAsc(!sortDirectionAsc);

         }else{
            console.log(`block 2`)
            setSortColumn(newSortColumn)
            setSortDirectionAsc(!sortDirectionAsc);
         }

    }
    function dataSorter(data){

        const sortedData = _.orderBy(data,[sortColumn],[(()=>(sortDirectionAsc ?"asc" : "desc"))()]);
        return sortedData;
    }
    return (
        <>
        <Table columnNames = {columnNames} data = { dataSorter(data)} onSortClick = {onSortClick} onEdit = {funct}/>
        </>
    )
}