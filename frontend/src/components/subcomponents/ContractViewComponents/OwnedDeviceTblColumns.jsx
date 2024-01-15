import React, {useState, useRef,useEffect} from 'react';
import { Constants } from '../../common/constants';


export const OwnedDeviceTblColumnsColumnNames =[
    {
        label: "id",
        datakey: "Id",
        visible: false,
        type: Constants.text,
    },
    {
        label: "Device Name",
        datakey: "Name",
        visible: true,
        type: Constants.text,
    },
    {
        label: "hwModelName Name",
        datakey: "hwModelName",
        visible: true,
        type: Constants.text,
    },
    {
        label: "deviceTypeName Name",
        datakey: "deviceTypeName",
        visible: true,
        type: Constants.text,
    },
    {
        label: "serviceTag Name",
        datakey: "serviceTag",
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
        label: "User History",
        actionKey: "Id",
        visible: true,
        type: Constants.button,
        action : (funct, actionkey)=>(
            <button type = "button" className = "btn btn-primary" onClick = {()=>funct.onHandleShowHistoryClick(actionkey)}>Show Device History</button>
        )
    },
]