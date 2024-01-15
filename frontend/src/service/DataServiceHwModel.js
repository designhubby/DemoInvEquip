import axios from "axios";

const controllerURL= '/HwModel';

export const GetAllHwModelListDtos = async () =>{
    return axios.get(`${controllerURL}`).then(result => result.data);
}
export const GetAllHwModelDtos = async () =>{
    return axios.get(`${controllerURL}`).then(result => result.data);
}

export const GetHwModelListDtoById = async (id) =>{
    return axios.get(`${controllerURL}/${id}`).then(result => result.data);
}
export const GetHwModelDataById = async (id)=>{
    return axios.get(`${controllerURL}/GetHwModelDataById/${id}`).then(response => response.data);
}

export const PostHWModel = async(hwModelDto)=>{
    Object.assign(hwModelDto, {['hwModelId'] : hwModelDto['Id']}, {['hwModelName']: hwModelDto['Name']});
    delete hwModelDto.Id;
    delete hwModelDto.Name;
    console.log(`hwModelDto`);
    console.log(hwModelDto);
    return axios.post(`${controllerURL}/PostHWModelByDto`, hwModelDto).then(response => response.data);
}
export const UpdateHWModel = async (hwModelDto)=>{
    Object.assign(hwModelDto, {['hwModelId'] : hwModelDto['Id']}, {['hwModelName']: hwModelDto['Name']});
    delete hwModelDto.Id;
    delete hwModelDto.Name;
    console.log(`hwModelDto`);
    console.log(hwModelDto);
    return axios.put(`${controllerURL}`,hwModelDto).then(response => response.data);
}

export const DeleteHwModelByIdAsync = async (id)=>{
    return axios.delete(`${controllerURL}/${id}`).then(result => result.data);
}