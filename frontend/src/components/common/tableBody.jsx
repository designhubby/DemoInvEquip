import React, {Component} from 'react';
import { Constants } from './constants.js';
import { useNavigate } from 'react-router-dom';


const renderColumnCell = (rowdata, columnNames,onEdit) =>{

    const DateFormater = (datedata)=>{
        if(datedata){
            const dateinfo = new Date(datedata);
            return(
                dateinfo.toDateString()
            )
        }
        return "Actively Associated"

    }


    const renderLink = (data, label)=>{
        
        return(
            <a className="btn btn-info" href={data} role = "button">{label}</a>
        )
    }


    return columnNames.map(columnCell => {
        if(columnCell.visible && columnCell.type == Constants.text){
            return  <td key = {columnCell.label}>{rowdata[columnCell.datakey]}</td>
        }else if(columnCell.visible && columnCell.type == Constants.button){
            return <td key = {columnCell.label}>
                {columnCell.action(onEdit, rowdata[columnCell.actionKey])} </td>
        }else if(columnCell.type == Constants.id){
            return <td key ={columnCell.label}>{columnCell.action(columnCell.roleData, rowdata[columnCell.datakey])}</td>
        }else if(columnCell.type == Constants.date){
            return  <td key = {columnCell.label}>{DateFormater(rowdata[columnCell.datakey])}</td>
        }else if(columnCell.type == Constants.link){
            return <td key={columnCell.label}>
                {renderLink(columnCell.link+rowdata[columnCell.datakey],columnCell.label)}
            </td>
        }
    })
}

const TableBody = ({data, columnNames, onEdit} ) =>{
    return(
        
        <React.Fragment>
            <tbody>
                {data.map((rowdata, index)=>
                    <tr key = {index}>
                        {renderColumnCell(rowdata, columnNames,onEdit)}
                    </tr>
                )}
            </tbody>
        </React.Fragment>
    )
}

export default TableBody;