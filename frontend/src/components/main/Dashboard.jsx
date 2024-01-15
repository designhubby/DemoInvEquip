import React, {useState, useRef, useEffect} from "react";
import {ReactComponent as DeviceType1} from "../../images/dashboard/noun-category-2659310.svg"
import {ReactComponent as DeviceType2} from "../../images/dashboard/noun-category-2996693.svg"
import {ReactComponent as Department1} from "../../images/dashboard/noun-department-4480336.svg"
import {ReactComponent as Department2} from "../../images/dashboard/noun-department-4480358.svg"
import {ReactComponent as Role1} from "../../images/dashboard/noun-role-objective-3151583.svg"
import {ReactComponent as Role2} from "../../images/dashboard/noun-role-objective-3151591.svg"
import {ReactComponent as Person1} from "../../images/dashboard/noun-person-1993798.svg"
import {ReactComponent as Person2} from "../../images/dashboard/noun-person-2005149.svg"
import {ReactComponent as Device1} from "../../images/dashboard/noun-devices-1729147.svg"
import {ReactComponent as Device2} from "../../images/dashboard/noun-devices-1845273.svg"
import {ReactComponent as Vendor1} from "../../images/dashboard/noun-vendor-2351010.svg"
import {ReactComponent as Vendor2} from "../../images/dashboard/noun-vendor-2357425.svg"
import {ReactComponent as Contract1} from "../../images/dashboard/noun-contract-1188608.svg"
import {ReactComponent as Contract2} from "../../images/dashboard/noun-contract-1188611.svg"
import {ReactComponent as HWModel1} from "../../images/dashboard/noun-flow-1534860.svg"
import {ReactComponent as HWModel2} from "../../images/dashboard/noun-flow-1534863.svg"
import { DashboardButton } from "../common/dashboardButtons";

export function Dashboard(){
    const [mouseOver, setMouseOver] = useState({
        name : false,
        department: false,
        role: false,
        person:false,
        device:false,
        vendor: false,
        contract: false,

    });

    const handleMouseOver = (e)=>{
        console.log(`handlemouseover`)
        console.log(e.currentTarget.dataset.name + " " + e.type)
        const name = e.currentTarget.dataset.name;
        let value = "";
        if(e.type == 'mouseleave'){
            value = false;
        }
        if(e.type == 'mouseenter'){
            value = true;
        }
        
        
        const newState = Object.assign({},{...mouseOver}, {[name]: value});
        setMouseOver(newState);
        console.log(newState)
    }

    const handleMouseOverName = (e)=>{
        console.log(`handlemouseoverName`)
        console.log(e)
        console.log(e.currentTarget.dataset.name);
    }

    const PersonBtnArray = [
        <DashboardButton buttonON = {<Role2/>} buttonOFF = {<Role1/>} Url = "/main/RoleView"/>,
        <DashboardButton buttonON = {<Department2/>} buttonOFF = {<Department1/>} Url ="/main/DepartmentView"/>
    ]
    const DeviceBtnArray = [
        <DashboardButton buttonON = {<Contract2/>} buttonOFF = {<Contract1/>} Url = "/main/ContractView"/>,
        <DashboardButton buttonON = {<DeviceType1/>} buttonOFF = {<DeviceType2/>} Url = "/main/DeviceTypeView2"/>,
        <DashboardButton buttonON = {<Vendor1/>} buttonOFF = {<Vendor2/>} Url = "/main/VendorView"/>,
        <DashboardButton buttonON = {<HWModel1/>} buttonOFF = {<HWModel2/>} Url = "/main/HwModelListView"/>,
    ]

    const rowPrinter = (btnArray)=>{
        
        return btnArray.map((indiv, index)=>(
            <div className = "row mb-2" key={index}>
                <div className = "col">
                    {indiv}
                </div>
            </div>
        ))
    }

    useEffect(()=>{

    },[])

    return(


<>
<div className ="container pt-2 bg-dark">
        <div className = "row row-cols-auto align-items-center">
            <div className = "col">
                {rowPrinter(PersonBtnArray)}
            </div>
            <div className = "col">
                <DashboardButton buttonON = {<Person1/>} buttonOFF = {<Person2/>} Url = "/main/PersonView"/>
            </div>
            <div className = "col">
                <DashboardButton buttonON = {<Device1/>} buttonOFF = {<Device2/>} Url = "/main/DeviceListView/?action=view"/>
            </div>
            <div className = "col">
                {rowPrinter(DeviceBtnArray)}
            </div>
        </div>

</div>
</>

    )
}