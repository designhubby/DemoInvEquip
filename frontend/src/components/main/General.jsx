import React, {useState, useEffect, useRef } from "react";
import { useSearchParams, useNavigate, useParams, Link } from "react-router-dom";
import { DeviceType } from "../common/constants";

export const General = () =>{

    //People
    //Devices
    //PeopleDevices


    return(
        <>

<div className="card">   
        <ul className="list-group list-group-flush" >
			<li className="list-group-item">
                
                    <Link className="navbar-brand" to="/main/Dashboard">Dashboard View</Link>
                
            </li>            
            <li className="list-group-item">
                
                    <Link className="navbar-brand" to="/main/Signin">Signin View</Link>
                
            </li>
            <li className="list-group-item">
                
                    <Link className="navbar-brand" to="/main/UserInfo">User Info View</Link>
                
            </li>            
            <li className="list-group-item">
                
                    <Link className="navbar-brand" to="/main/ErrorTestView">ErrorTest</Link>
                
            </li>
            <li className="list-group-item">
                
                    <Link className="navbar-brand" to="/main/PersonView">People</Link>
                
            </li>
            <li className="list-group-item">
                
                    <Link className="navbar-brand" to="/main/HwModelListView">HwModel List</Link>
                
            </li>   
            <li className="list-group-item">
                
                    <Link className="navbar-brand" to="/main/DeviceListView/?action=view">Device List</Link>
                
            </li>
            <li className="list-group-item">
                
                    <Link className="navbar-brand" to="/main/VendorView">Vendor View</Link>
                
            </li>
            <li className="list-group-item">
                
                    <Link className="navbar-brand" to="/main/ContractView">ContactView</Link>
                
            </li>
            <li className="list-group-item">
                
                    <Link className="navbar-brand" to="/main/DeviceTypeView2">Device Type View NEW</Link>
                
            </li>
            <li className="list-group-item">
                
                    <Link className="navbar-brand" to="/main/DepartmentView">Department Type View</Link>
                
            </li>
            <li className="list-group-item">
                
                    <Link className="navbar-brand" to="/main/RoleView">Role Type View</Link>
                
            </li>
            </ul>
		</div>
        </>
    )

}
