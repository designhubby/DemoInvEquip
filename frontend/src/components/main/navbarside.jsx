import React, { useEffect, useState } from 'react';
import * as FaIcons from "react-icons/fa";
import * as AiIcons from 'react-icons/ai';
import * as IoIcons from 'react-icons/io';
import { NavbarsideData } from '../subcomponents/Navbarside/NavbarsideData';
import '../subcomponents/Navbarside/Navbarside.css'
import { IconContext } from 'react-icons';
import { Link } from "react-router-dom";



export function NavBarSide(){

    const [sidebar, setSidebar] = useState(false);
    const showSidebar = () => setSidebar(!sidebar);


    return(
        <>
        <IconContext.Provider value={{ color: '#fff' }}>
            <div className = 'navbar'>
                <Link to='#' className='menu-bars'>
                    {!sidebar ? <FaIcons.FaBars onClick = {showSidebar}/>: <AiIcons.AiOutlineClose onClick = {showSidebar}/>} 
                </Link>
            </div>
            <nav className={sidebar ? 'nav-menu active' : 'nav-menu'}>
                <ul className = 'nav-menu-items'>
                    <li className='navbar-toggle'>
                        <Link to='#' className ='menu-bars'>
                            
                        </Link>
                    </li>
                    {NavbarsideData.map((indiv, index) =>(
                        <li key={index} onClick = {showSidebar} className = {indiv.cName}>
                            <Link to = {indiv.path} className = 'menu-bars'>
                                {indiv.icon}<span>{indiv.title}</span>
                            </Link>
                        </li>
                        )
                    )}
                </ul>
            </nav>
        </IconContext.Provider>
        </>
    )
}