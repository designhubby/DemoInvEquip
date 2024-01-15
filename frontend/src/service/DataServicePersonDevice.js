import axios from "axios";


const baseUrl = {
    baseURL:`${process.env.REACT_APP_BaseURL}/PersonDevice/`, 
}

const controllerURL = '/PersonDevice';

export const GetDevicesAssociatedPersons = async (deviceId)=>{
    return axios.get(`${controllerURL}/GetDevicesAssociatedPersons/${deviceId}`).then(response =>response.data);
}

export const GetPersonAssociatedDevices = async (personId)=>{
    return axios.get(`${controllerURL}/GetPersonAssociatedDevices/${personId}`).then(response =>response.data);
}

export const GetDeviceByFilteredQuery = async (deviceTypeId, retired, activeRental, hwModelId)=>{
    const _deviceTypeId = deviceTypeId ? `deviceTypeId=${deviceTypeId}` : "";
    const _retired = retired ? `&retired=${retired}` : "";
    const _activeRental = activeRental ? `&activeRental=${activeRental}` : "";
    const _hwModelId = hwModelId ? `&hwModelId=${hwModelId}` : "";
    console.log(`GetDeviceByFilteredQuery?${_deviceTypeId}${_retired}${_activeRental}`);
    return axios.get(`${controllerURL}/GetDeviceByFilteredQuery?${_deviceTypeId}${_retired}${_activeRental}${_hwModelId}`).then(results=>results.data);
    //return axios.create(baseUrl).get(`GetDeviceByFilteredQuery?deviceTypeId=${deviceTypeId}&retired=${retired}&activeRental=${activeRental}`).then(results=>results.data);

}

export const PostPersonDevice = async (personDeviceLinkArray)=>{
    console.log(personDeviceLinkArray);
    return axios.post(`${controllerURL}/AssociatePersonDeviceByPdDto`, personDeviceLinkArray).then(results=> results);
}

export const UnassociatePersonDeviceByIds = async (personDeviceIdArray) =>{
    console.log(personDeviceIdArray);
    const result = axios.patch(`${controllerURL}/UnassociatePersonFromDeviceByList`,personDeviceIdArray).then(result=>result.data);
}

export default axios.create(baseUrl);