import React, { useEffect, useState } from 'react';
import * as FaIcons from "react-icons/fa";
import * as AiIcons from 'react-icons/ai';
import * as IoIcons from 'react-icons/io';
import * as MdIcons from 'react-icons/md';
import * as BsIcons from 'react-icons/bs';
import * as TiIcons from 'react-icons/ti';
import * as GrIcons from 'react-icons/gr';

export const NavbarsideData =[
    {
        title: 'Home',
        path: '/',
        icon : <AiIcons.AiFillHome/>,
        cName : 'nav-text',
    },
    {
        title: 'Employee',
        path: '/main/PersonView',
        icon : <BsIcons.BsPersonCircle/>,
        cName : 'nav-text',
    },
    {
        title: 'Devices',
        path: '/main/DeviceListView/?action=view',
        icon : <MdIcons.MdImportantDevices/>,
        cName : 'nav-text',
    },
    {
        title: 'Department',
        path: '/main/DepartmentView',
        icon : <AiIcons.AiOutlineAppstore/>,
        cName : 'nav-text',
    },
    {
        title: 'Role',
        path: '/main/RoleView',
        icon : <MdIcons.MdWorkOutline/>,
        cName : 'nav-text',
    },
    {
        title: 'Contracts',
        path: '/main/ContractView',
        icon : <FaIcons.FaFileContract/>,
        cName : 'nav-text',
    },
    {
        title: 'Hardware Models',
        path: '/main/HwModelListView',
        icon : <TiIcons.TiFlowMerge/>,
        cName : 'nav-text',
    },
    {
        title: 'Device Types',
        path: '/main/DeviceTypeView2',
        icon : <FaIcons.FaShapes/>,
        cName : 'nav-text',
    },
    {
        title: 'Vendors',
        path: '/main/VendorView',
        icon : <FaIcons.FaStore/>,
        cName : 'nav-text',
    },
]