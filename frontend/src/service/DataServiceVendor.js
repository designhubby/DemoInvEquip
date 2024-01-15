import axios from "axios";

const baseUrl = {
    baseURL:`${process.env.REACT_APP_BaseURL}/Vendor/`, 
}
const controllerURL ='/Vendor';

export const GetAllVendorDataDtos = async ()=>{
    return axios.get(`${controllerURL}/GetAllVendorData`).then(results=>results.data);
}

export const GetVendorDataDtoByVendorId = async (id)=>{
    return axios.get(`${controllerURL}/GetVendorDataDtoByVendorId/${id}`).then(results => results.data);
}

export const GetContractDataDtosOwnByVendorByVendorId = async(vendorId) =>{

    return axios.get(`${controllerURL}/GetContractDataDtosOwnByVendorByVendorId/${vendorId}`).then(results=>results.data);

}

export const GetContractDataDtosOwnByVendorByContractId = async (contractId)=>{
    return axios.get(`${controllerURL}/GetContractDataDtosSibblingOwnByVendorByContractId/${contractId}`).then(results => results.data);
}
export const PostVendorDataByDto = async (vendorDto)=>{
    const _vendorDto = {
        "vendorName": vendorDto.Name,
      }
    return axios.post(`${controllerURL}/PostVendorDataByDto`, _vendorDto).then(results => results.data);
}

export const UpdateVendorDataByDto = async (vendorDto) =>{
    console.log(`update mode`)
    const _vendorDto = {
        "vendorId": parseInt(vendorDto.Id),
        "vendorName": vendorDto.Name,
      }
    return axios.patch(`${controllerURL}/PatchVendorEntity/${_vendorDto.vendorId}`,_vendorDto).then(results => results.data);
}