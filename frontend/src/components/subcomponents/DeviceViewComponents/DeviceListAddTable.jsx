import React, {useEffect, useState, useRef} from 'react';
import { Constants } from '../../common/constants';
import { Table } from '../../common/table';

const DeviceListColumns=[
    {
        datakey: "deviceId",
        visible: false,

    },
    {
        label: "Device Name",
        datakey: "deviceName",
        type: "text",
        visible: true,

    },
    {
        label: "Hardware Model Name",
        datakey: "hwModelName",
        type: "text",
        visible: true,
    },
    {
        label: "Device Type Name",
        datakey: "deviceTypeName",
        type: "text",
        visible: true,
    },
    {
        label: "Service Tag",
        datakey: "serviceTag",
        type: "text",
        visible: true,
    },
    {
        label: "Add Device",
        datakey: "deviceId",
        actionKey: "deviceId",
        action : (funct, actionkey) =>(
            <button type = {Constants.button} className='btn btn-primary' onClick= {()=> funct.handleDeviceAddFunc(actionkey)}> {funct.handleButtonDisplayFunc(actionkey)}</button> 
         ),
        type: Constants.button,
        visible: true,

    },
]

export const DeviceListAddTable = ({DeviceListData, onSortClick, onAdd, onSubmit, labelSubmit}) =>(
    <React.Fragment>
    <Table columnNames ={DeviceListColumns} data = {DeviceListData} onSortClick={onSortClick} onEdit={onAdd}/>
    <button className= "btn btn-primary" type = "button" onClick = {()=>onSubmit()}>{labelSubmit}</button>
    </React.Fragment>

)