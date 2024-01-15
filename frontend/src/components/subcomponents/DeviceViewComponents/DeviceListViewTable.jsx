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
        visible: true,
        label: "Device Owner History",
        actionKey:"deviceId",
        type: Constants.button,
        action: (onEdit, actionkey) =>
            <button type = "button" onClick = {(e)=> onEdit.handleOnDeviceIdChange(actionkey)} className = "btn btn-primary">View Device History</button>
        

    },
    {
        label: "Edit Device",
        datakey: "deviceId",
        link: "/form/DeviceDataForm/",
        type: "link",
        visible: true,

    },
    {
        visible: true,
        label: "Retire Device",
        actionKey:"deviceId",
        type: Constants.button,
        action: (onEdit, actionkey) =>
            <button type = "button" onClick = {(e)=> onEdit.handleOnDeviceDelete(actionkey)} className = {onEdit.displayRetireLabel()}>Retire Device</button>
        

    },
]

export const DeviceListViewTable = ({DeviceListData, onSortClick, onEdit}) =>(

    <Table columnNames ={DeviceListColumns} data = {DeviceListData} onSortClick={onSortClick} onEdit={onEdit}/>

)