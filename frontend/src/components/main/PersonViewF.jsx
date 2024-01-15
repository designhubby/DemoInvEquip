import React, {Component, useEffect, useState, useRef} from 'react';
import PersonTable from '../subcomponents/PersonTable';
import PersonAPI, {getPeopleWhere} from '../../service/DataServicePerson';
import RoleAPI from '../../service/DataServiceRole';
import { getAllPeople, getPerson } from '../../service/fakeServicePerson';
import { getAllRoles, getRole } from '../../service/fakeServiceRole';
import _ from 'lodash';
import { useNavigate } from "react-router-dom";
import { paginate } from './../../util/pagination';
import { Pagechooser } from '../common/pagechooser';
import { TextField } from '../common/form';
import TranslatePropertyKeys from '../../util/translatePropertyKeys';


function PersonViewF(){

    const history = useNavigate();

    const [fullPeopleList, setFullPeopleList] = useState([]);
    const [peopleList, setPeopleList] = useState([]);
    
    const [roleList, setRoleList] = useState([]);
    const [sortDirection, setSortDirection] = useState("asc");
    const [sortColumnMode, setSortColumnMode] = useState("_id");
    const [dataHasLoaded, setDataHasLoaded] =useState(false);

    const entriesPerPage = useRef(6);
    const [currentPage, setCurrentPage] = useState(1);

    let searchTerm = useRef('');



    async function getData(){
        const allRoles = await RoleAPI.get('GetAllRoles').then(result => (result.data));
        const translatedData = TranslatePropertyKeys(allRoles, 'role');
        const allPeople = await PersonAPI.get(' ').then((result)=>(result.data));
        console.log(allPeople);
        setFullPeopleList(allPeople);
        setPeopleList(allPeople);
        setRoleList(translatedData);
        setDataHasLoaded(true);
    }

    useEffect(()=>{
        getData()
    },[])



    const handleSort = (_sortColumnMode) =>{
        console.log(_sortColumnMode + " " + sortDirection);
        if(sortColumnMode == _sortColumnMode){
            if(sortDirection==="asc"){
                console.log("branch1");
                setSortDirection("desc");
                setSortColumnMode(_sortColumnMode);
    
            }else{
                console.log("branch2");
                setSortDirection("asc");
                setSortColumnMode(_sortColumnMode);
            }
        }else{
            setSortDirection("asc");
            setSortColumnMode(_sortColumnMode);
        }

    }

    const handleEditPerson = (actionKey) =>{
        history(`/main/personForm2/${actionKey}`);
    }

    const handleCreateNewEmployee = ()=>{
        history('/main/personForm2/new');
    }

    const handlePageChange = (pageChoosen)=>{
        setCurrentPage(pageChoosen);
    }

    const handleSearchFilter = async (e, testLabel)=>{
        const nameQuery = e.target.value;
        const filteredPeople = await getPeopleWhere(fullPeopleList,nameQuery);
        
        searchTerm.current = nameQuery;
        setCurrentPage(1);
        setPeopleList(filteredPeople);
    }

    const renderPeopleList = () =>{
        
        if(dataHasLoaded){
    
            let orderedPeopleList = _.orderBy(peopleList,[sortColumnMode], [sortDirection])
            let newPeopleList = paginate(orderedPeopleList,entriesPerPage.current,currentPage);
            return (
                <React.Fragment>
                    <PersonTable peopleData = {newPeopleList} roleData = {roleList} onSortClick = {handleSort} onEdit = {handleEditPerson}/>
                    <button className='btn btn-primary' type = 'button' onClick={handleCreateNewEmployee}>New Employee</button>
                    <Pagechooser entriesPerPage={entriesPerPage.current} allEntries={orderedPeopleList} currentPage={currentPage} handlePageChange={handlePageChange}/>
                </React.Fragment>
            );
        }else{
            return <h1>....Loading</h1>
        }
    }

    return (
        <React.Fragment>
            <h1>Employees</h1>
            <form>
                <TextField placeHolder ='Search Name' label ='Search Name' statekey={null} onChange={handleSearchFilter} value = {searchTerm.current}/>
            </form>
            {renderPeopleList()}
        </React.Fragment>
        );

}

export default PersonViewF;
