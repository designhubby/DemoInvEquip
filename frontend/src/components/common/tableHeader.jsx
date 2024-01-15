import React, {Component} from 'react';
import { render } from '@testing-library/react';

const renderColumn = (columnName, onSortClick) =>
     {
        if(columnName.visible && columnName.datakey){
            return renderLabelColumn(onSortClick,columnName);
        }else if(columnName.action){
            return renderButtonColumn(columnName);
        }
        
    }
const renderButtonColumn = (columnName)=>{
    return <td key = {columnName.label}>{columnName.label}</td>
}
const renderLabelColumn = (onSortClick, columnName) =>{
    return <td className = "clickable" key = {columnName.datakey} onClick = {()=>onSortClick(columnName.datakey)}> {columnName.label}</td>
}



export const TableHeader = ({columnNames, onSortClick}) =>{
    
    return (
        <React.Fragment>
            <thead>
                <tr>
                    {columnNames.map(
                        (columnName, index) => //index will be out of bounds as more column labels than data columns
                            renderColumn(columnName, onSortClick)
                        )
                    }
                </tr>
            </thead>
        </React.Fragment>
    )

}