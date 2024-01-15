import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';


export function DashboardButton({buttonON, buttonOFF, Url}){

    const classInfo1 = "dashboard-button-bg-lightblue rounded border border-1 border-primary"
    const classInfo2 = "dashboard-button-bg-lightgrey rounded border border-1"
    //const imgPath = require('../../images/dashboard/' + buttonPath1).default
    const height = 150
    //{toggle ? <img src={imgPath} data-name={name} onMouseLeave={e=>handleMouseOver(e)} className = {classInfo1} height={height} width={height} /> : <img src={require('../../images/dashboard/noun-person-2005149.svg').default} data-name={name} onMouseEnter={e=>handleMouseOver(e)} className = {classInfo2} height={height} width={height} />}

    //        <img src={require('../../images/dashboard/noun-department-4480336.svg').default} data-name={name} className = {classInfo1} height={150} width={150} />

    const [toggle, setToggle] = useState(false);
    const navigate = useNavigate();

    const handleOnMouseOver = (e)=>{
        if(e.type == 'mouseleave'){
            setToggle(false) 
        }
        if(e.type == 'mouseenter'){
            setToggle(true);
        }
    }

    const currentButton = toggle ? buttonON : buttonOFF;
    const classInfo = toggle ? classInfo1 : classInfo2;



    const buttonCreated = React.cloneElement(currentButton, {
        className : classInfo, height : height, width:height, onMouseLeave : e=>handleOnMouseOver(e), onMouseEnter : e=>handleOnMouseOver(e), onClick : ()=>navigate(Url)
     })



    return(
        <> 
        {buttonCreated}

         </>
    )
}