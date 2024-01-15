import React, {Component} from 'react'
import { TableHeader } from './tableHeader';
import TableBody from './tableBody';

export const Table = ({columnNames, data,onSortClick,onEdit} ) =>{
    let sortAscending = true;

    return(
        <React.Fragment>
            <table className = "table table-dark table-hover">
            <TableHeader columnNames = {columnNames} onSortClick = {onSortClick}/>
            <TableBody columnNames = {columnNames} data = {data} onEdit= {onEdit}/>
            </table>
        </React.Fragment>


    )
}
