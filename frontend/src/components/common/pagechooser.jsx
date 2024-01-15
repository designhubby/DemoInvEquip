import React from "react";
import PropTypes from "prop-types";
import _ from 'lodash';

export const Pagechooser = ({entriesPerPage, allEntries, currentPage, handlePageChange}) =>{

    const numberOfPages = Math.ceil(allEntries.length / entriesPerPage);
    const numberArray = _.range(1,numberOfPages+1);

    if(numberOfPages===1){
        return null;
    }

    const activeIndicator = (indivPageNumber)=>{
        if(currentPage === indivPageNumber){
            return "page-item active"
        }else{
            return "page-item"
        }
    }

    return(
        <React.Fragment>
            <nav>
                <ul className = "pagination">
                    {numberArray.map((indivPageNumber)=>(
                        <li key={indivPageNumber} className = {activeIndicator(indivPageNumber)}>
                            <a className="page-link" onClick={()=>handlePageChange(indivPageNumber)}>{indivPageNumber}</a>
                        </li>
                    ))}
                </ul>
            </nav>
        </React.Fragment>
    )

}

Pagechooser.propTypes ={
    entriesPerPage: PropTypes.number.isRequired,
    allEntries: PropTypes.array.isRequired,
    currentPage: PropTypes.number.isRequired,
    handlePageChange : PropTypes.func.isRequired,
};