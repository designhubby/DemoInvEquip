import React,{useEffect, useState, useRef} from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { Table } from "../../common/table"; 
import { Constants } from './../../common/constants';
import _ from 'lodash';
import { paginate } from './../../../util/pagination';
import { Pagechooser } from './../../common/pagechooser';



export const SingleDataTypeViewTable = ({funct, data, columnNames, searchMode})=>{
    
    //const columnNumberWithName = columnNames.findIndex(indiv=>indiv.datakey.toLowerCase() == "name")

    //const _datakey = columnNames[columnNumberWithName].datakey;

    const entriesPerPage = useRef(5);
    
    const [currentPage, setCurrentPage] = useState(1);

    const [sortColumn, setSortColumn] = useState("");
    const [sortDirectionAsc, setSortDirectionAsc] = useState(true);

    useEffect(()=>{
        setCurrentPage(1);
    }, [searchMode])
    
    const handlePageChange = (indivPageNumber) =>{
        setCurrentPage(indivPageNumber);

    }

    function onSortClick(e){
         const newSortColumn = e;
         if(newSortColumn == sortColumn){
             console.log(`block 1`)
            setSortDirectionAsc(!sortDirectionAsc);

         }else{
            console.log(`block 2`)
            setSortColumn(newSortColumn)
            setSortDirectionAsc(!sortDirectionAsc);
         }

    }
    function dataSorter(data){

        const sortedData = _.orderBy(data,[sortColumn],[(()=>(sortDirectionAsc ?"asc" : "desc"))()]);
        const paginatedlist = paginate(sortedData,entriesPerPage.current, currentPage);

        return paginatedlist;
    }
    return (
        <>
        <Table columnNames = {columnNames} data = { dataSorter(data)} onSortClick = {onSortClick} onEdit = {funct}/>
        <Pagechooser entriesPerPage = {entriesPerPage.current} allEntries = {data} currentPage = {currentPage} handlePageChange = {handlePageChange}/>
        </>
    )
}