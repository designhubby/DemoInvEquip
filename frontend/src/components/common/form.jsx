import React, {Component, useState, useEffect} from 'react';
import ReactDatePicker, { CalendarContainer } from 'react-datepicker';
import  TranslatePropertyKeys  from './../../util/translatePropertyKeys';



export const TextField = ({name, disabled, placeHolder, label, statekey, onChange, value}) => (
    
    <React.Fragment>
        <div className ="row mb-3">
            <label className = "col-sm-2 col-form-label" htmlFor ={label}>{label}</label>
            <div className="col-sm-4">
            <input key={statekey} disabled ={disabled ?? false} className ="form-control" type = "text" id = {label} data-statekey={statekey ?? name} name = {name ?? ""} placeholder = {placeHolder ?? null} value = {value ?? ""} onChange={(e)=>onChange(e)} />
            </div>
        </div>
    </React.Fragment>

)

export const EmailField = ({placeHolder, label, onChange, value}) =>{
    <div className="row mb-3">
        <label className="col-sm-2 col-form-label" htmlFor={label}>{label}</label>
        <div className="col-sm-10">
            <input className ="form-control" type = "email" id = {label} placeholder = {placeHolder ?? null} value = {value} onChange={(e)=>onChange(e,label)} />
        </div>
    </div>

}

export const SelectField = ({options, onChange, value, label, statekey, labelkey})=>{

    const renderOptions=()=>{
        if(options){
            //console.log(`Render Options block: branch1`)
            //console.dir(options)
            return options.map(indivOpt => <option key = {indivOpt.Id} value = {indivOpt.Id}> {indivOpt.Name}</option>)
        }else{
            
            //console.log(`Render Options block: branch2`);
            return <option key ={0} value={0}>Loading</option>
        }
    }

    return(
        <React.Fragment>
            <div className="row mb-3">
                <label className = "col-sm-2 col-form-label" htmlFor = {label}>{label}</label>
                <div className="col-sm-3">
                    <select className="form-select" id = {label} data-statekey = {statekey} data-labelkey = {labelkey}  value = {value} onChange={(e)=>onChange(e, options[e.target.selectedIndex])}>
                        {renderOptions()}
                    </select>
                </div>
            </div>
    </React.Fragment>)

}
export const DatePicker = ({label, selected, onChange,isClearable,statekey, excludeDateIntervals})=>{
    console.log(selected);
    return(
        <>
        <div className="row mb-3">
            <label className = "col-sm-2 col-form-label" htmlFor = {label}>{label}</label>
            <div className="col-sm-3">

                <ReactDatePicker selected={selected} dateFormat="MMM-dd-yyyy" 
                    onChange={(date)=>onChange(
                            {//object here mimics the structure of event data sent from html objs
                                target:{
                                    value:date,
                                    dataset:
                                    {
                                        statekey:statekey
                                    }
                                }
                            }
                        )} 
                    excludeDateIntervals = {excludeDateIntervals}
                    isClearable = {isClearable}/>
                </div>
            </div>
        </>
    )
}

export const SubmitButton = ({label}) =>{
    const [btndisable, setBtndisable] = useState(false);
    const handleOnClick = ()=>{
        setTimeout(()=>setBtndisable(true), 5000)
    }
    return(
    <React.Fragment>
        <div className="row mb-3">
            <div className="col-sm-2"/>
            <div className="col-sm-2">  
                <button type="submit" className="btn btn-primary" disabled={btndisable} onClick={()=>handleOnClick()}>{label}</button>
            </div>
            
        </div>
    </React.Fragment>)
}

export const GeneralButton = ({label, handleOnClick, data})=>{
    return(
        <React.Fragment>
            <div className = "row mb-3">
                <div className = "col-sm-2"/>
                <div className = "col-sm-2">
                    <button className="btn btn-primary" type="button" onClick={()=>handleOnClick(data??null)} >{label}</button>
                </div>
            </div>

        </React.Fragment>
    )
}

export const PasswordField = ({show, data})=>{
    return(
        <>
        
        </>
    )
}


export const DepartmentSelectField = ({apiDepartmentData, onChange, currentValue, label, statekey, labelkey})=>{
    const [departmentsAll, setDepartmentsAll] = useState([]);
    const [dataLoaded, setDataLoaded] = useState(true);

    useEffect(()=>{
        GetData();
    },[]);

    async function GetData(){
        const departmentsDtoArray = await apiDepartmentData();
        let translatedKeysArray = TranslatePropertyKeys(departmentsDtoArray, "department")
        const blankPlaceHolder = {
            Id : 0,
            Name : "Choose"
        }
        translatedKeysArray.unshift(blankPlaceHolder)
        await Promise.resolve(setDepartmentsAll(translatedKeysArray));
        await Promise.resolve(setDataLoaded(true));
    } 
    return(
        <>{dataLoaded && 
            <SelectField options ={departmentsAll} onChange={onChange} value={currentValue} label={label} statekey={statekey} labelkey  = {labelkey}/>
        }
        </>
    )
}

export const RoleSelectField = ({apiRoleData, onChange, currentValue, label, statekey, departmentId})=>{

    const [rolesAllData, setRolesAllData] = useState([]);
    const [dataLoaded, setDataLoaded] = useState(true);

    useEffect(()=>{
        getData();
    },[departmentId])

    const unassignedEntry =   {
        "roleId": 0,
        "roleName": "Unassigned",
        "departmentId": 0
      };

    const getData = async ()=>{
        let data =[];
        if(departmentId){
            console.log(`Role SelectField: departmentId provided`)
            console.log(departmentId);
            data = await apiRoleData(departmentId);
        }else{
            console.log(`Role SelectField: no departmentId provided`)
            data  = await apiRoleData();
        }
        data.unshift(unassignedEntry);
        const translatedKeysArray = TranslatePropertyKeys(data, "role")
        await Promise.resolve(setRolesAllData(translatedKeysArray));
        await Promise.resolve(setDataLoaded(true));

    }
    return(
        <>
        {dataLoaded && <SelectField options ={rolesAllData} onChange={onChange} value={currentValue} label={label} statekey={statekey}/>}
        </>
    )

}