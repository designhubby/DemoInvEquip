import React, {useState, useRef} from 'react';
import { Constants } from '../../common/constants.js';
import { Table } from './../../common/table';



const ColumnNames=[
    {
        label: "Device Name",
        datakey: "deviceName",
        visible:true,
        type: Constants.text,
    },
    {
        label: "HwModel Name",
        datakey: "hwModelName",
        visible:true,
        type:Constants.text,
    },
    {
        label: "Device Type",
        datakey: "deviceType",
        visible:true,
        type:Constants.text,
    },
    {
        label: "Start Date",
        datakey: "startDate",
        visible:true,
        type:Constants.date,
    },
    {
        label: "End Date",
        datakey: "endDate",
        visible:true,
        type:Constants.date,
    },
    {
        label: "End Association",
        datakey: "personDeviceId",
        actionKey: "personDeviceId",
        visible:true,
        type:Constants.button,
        action: (funct, actionkey)=>
            <button type = {Constants.button} className = 'btn btn-primary' onClick = {()=>funct.handleEndAssociationBtn(actionkey)} disabled = {funct.handleEndAssociationBtnDisabled(actionkey)}>{funct.handleEndAssociationBtnStatus(actionkey)}</button>
        
    },
    
]

export const PersonDeviceViewTable = ({PersonDeviceData, onSortClick, onEdit})=>{



    return(
        <React.Fragment>
              <Table columnNames = {ColumnNames} data = {PersonDeviceData} onSortClick = {onSortClick} onEdit = {onEdit}/>
        </React.Fragment>
    )

}