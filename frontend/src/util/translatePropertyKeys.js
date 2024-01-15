import _ from 'lodash';

const TranslatePropertyKeys = (dataArray, keynameprefix) => {
    const newdata = dataArray.map((indiv) =>{
        Object.assign(indiv,{['Id'] : indiv[keynameprefix+'Id']},{['Name']: indiv[keynameprefix+'Name']});
        delete indiv[keynameprefix+'Id'];
        delete indiv[keynameprefix+'Name'];
        return indiv;
    })
    
    const sortedData = _.orderBy(dataArray,['Name'],['asc']);
    return sortedData;
}
export const TranslatePropertyKeysToDto = (data1, prefix)=>{
    const newClone = Object.assign({},data1);
    Object.assign(newClone, {[prefix+'Id']:newClone['Id']},{[prefix+'Name']:newClone['Name']})
    delete newClone['Id']
    delete newClone['Name']
    return newClone;
}

export default TranslatePropertyKeys;

