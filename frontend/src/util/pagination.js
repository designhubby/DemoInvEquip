import _ from 'lodash';
import PropTypes from 'prop-types';

export const paginate = (allEntries, entriesPerPage, currentPage)=>{

    //console.log(`entriesPerPage: ${entriesPerPage} currentPage: ${currentPage}`)
    //console.dir(allEntries);
    const startIndex = entriesPerPage * (currentPage-1);
    const subsetOfEntries = _(allEntries).slice(startIndex).take(entriesPerPage).value();
    //console.dir(subsetOfEntries)
    return(
        subsetOfEntries
    )
}

paginate.propTypes ={
    allEntries : PropTypes.array.isRequired,
    entriesPerPage : PropTypes.number.isRequired,
    currentPage : PropTypes.number.isRequired,
}