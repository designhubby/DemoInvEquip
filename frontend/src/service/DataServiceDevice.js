import axios from "axios";

const baseURL = {
    baseURL:`${process.env.REACT_APP_BaseURL}/Device/`, 
}

const controllerURL= '/Device';

export const GetDeviceDataById = async(id) =>{
    return axios.get(`${controllerURL}/GetDeviceDataById/${id}`).then(result=>result.data);
}

export const PostDeviceData = async(deviceData)=>{
    return axios.post(`${controllerURL}/PostDeviceData`,deviceData).then(result=>result.data);
}

export const PutDeviceData = async(deviceData)=>{
    return axios.put(`${controllerURL}/PutDeviceData`,deviceData).then(result=>result.data);
}
export const DeleteDeviceDataById = async(id)=>{
    console.log("deleting DeleteDeviceDataById")
    console.log(id)
    return axios.delete(`${controllerURL}/${id}`).then(response => response.data);
}