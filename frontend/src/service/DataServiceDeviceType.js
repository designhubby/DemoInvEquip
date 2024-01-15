import axios from "axios";

const baseURL = {
    baseURL:`${process.env.REACT_APP_BaseURL}/DeviceType/`, 
}
const controllerURL = '/DeviceType'


export const GetHwModelDataDtosByDeviceTypeId = async (deviceTypeId)=>{
    return axios.get(`${controllerURL}/GetHwModelDataDtosByDeviceTypeId/${deviceTypeId}`).then(results=>results.data);
}

export const GetAllDeviceTypeDtos = async () =>{
    return axios.get(`${controllerURL}/GetAllDeviceTypeDtos`).then(results => results.data);
}

export const GetDeviceTypeDtoByDeviceTypeId = async (id) =>{
    return axios.get(`${controllerURL}/GetDeviceTypeDtoByDeviceTypeId/${id}`).then(results => results.data);
}

export const PostDeviceTypeDto = async (deviceTypeDto)=>{
    const submitObj = {
        deviceTypeId : deviceTypeDto.Id,
        deviceTypeName : deviceTypeDto.Name
    }
    return axios.post(`${controllerURL}/PostDeviceTypeDto`, submitObj).then(results => results.data);
}

export const UpdateDeviceTypeDto = async (deviceTypeDto)=>{
    const submitObj = {
        deviceTypeId : deviceTypeDto.Id,
        deviceTypeName : deviceTypeDto.Name
    }
    return axios.put(`${controllerURL}/UpdateDeviceTypeDto`, submitObj).then(results => results.data);
}
export const DeleteDeviceTypeById = async (id)=>{
    return axios.delete(`${controllerURL}/${id}`).then(results => results.data);
}