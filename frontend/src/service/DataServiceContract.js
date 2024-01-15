import axios from "axios";

const controllerURL= '/Contract';

export const GetAllContractsListDto = async () =>{
    return axios.get(`${controllerURL}/GetAllContractsListDto`).then(result => result.data);
}

export const GetContractDataDtoById = async (id)=>{
    return axios.get(`${controllerURL}/${id}`).then(result => result.data);
}
export const GetAllContractDevicesByContractId = async(contractId)=>{
    return axios.get(`${controllerURL}/GetAllContractDevicesByContractId/${contractId}`).then(response => response.data);
}

export const PostContractDataByDto = async (contractDataDto)=>{
    Object.assign(contractDataDto,{['contractId']:contractDataDto['Id']});
    Object.assign(contractDataDto,{['contractName']: contractDataDto['Name']});
    delete contractDataDto['Id'];
    delete contractDataDto['Name'];
    return axios.post(`${controllerURL}`, contractDataDto).then(result=>result.data);
}

export const PutContractDataByDto = async (contractDataDto)=>{
    Object.assign(contractDataDto,{['contractId']:contractDataDto['Id']});
    Object.assign(contractDataDto,{['contractName']: contractDataDto['Name']});
    delete contractDataDto['Id'];
    delete contractDataDto['Name'];
    return axios.put(`${controllerURL}`, contractDataDto).then(result=>result.data);
}

export const DeleteContractDataById = async (id)=>{
    return axios.delete(`${controllerURL}/${id}`).then(result=>result.data);
}