import React, { useEffect, useState } from 'react';
import { Link } from "react-router-dom";

import * as FaIcons from "react-icons/fa";
import { NavBarSide } from './navbarside';

//import { Link } from "react-router-dom";
//remove react-cookie , universal cookie
function NavBar1({SignedInState, HandleAuthenticationLogOut, AuthenticationSetup}){


    return(

    <nav className="navbar navbar-expand-md navbar-dark bg-dark mb-2">
    <div className="container-fluid ">
        
        {/*<a className="navbar-brand" href="#">Top navbar</a> */}
        <NavBarSide className="navbar-brand"/>


        <div>
        <ul className="navbar-nav me-auto mb-2 mb-md-0">
            <li className="nav-item">
            <Link className="nav-link active" onClick={()=>AuthenticationSetup()} aria-current="page" to= "/">Home </Link>
            </li>
            <li className="nav-item">
            {SignedInState.state ? <button type="button" onClick={()=>HandleAuthenticationLogOut()} className="btn-navlink">{SignedInState.state ? "Log Out" : "Log In"}</button> : <Link className="nav-link active" aria-current="page"  to= "/main/Signin">Log In </Link> }
            
            </li>
            {SignedInState.state ? 
            null : 
            <li className="nav-item">
            <Link className="nav-link" aria-current="page" to= "/main/Register">Register </Link>
            </li>
            }
            
            {SignedInState.state ? <li>
                <div className="dropdown">
                    <button className="ms-2 btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                    <i className="bi bi-person-circle"></i>
                    </button>
                    <ul className="dropdown-menu dropdown-menu-dark dropdown-menu-end" aria-labelledby="dropdownMenuButton1">
                        <li><Link className="dropdown-item" aria-current="page"  to= "/main/UserInfo">User Control Panel</Link></li>
                        <li>{SignedInState.state ? <button type="button" onClick={()=>HandleAuthenticationLogOut()} className="dropdown-item">{SignedInState.state ? "Log Out" : "Log In"}</button> : <Link className="nav-link active" aria-current="page"  to= "/main/Signin">Log In </Link> }
                        </li>
                    </ul>
                </div>    
                </li> : 
                null
            }
        </ul>

        </div>
    </div>
    </nav>        

    )

}

 export default NavBar1;