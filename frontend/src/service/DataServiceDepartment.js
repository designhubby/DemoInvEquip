import axios from "axios";
import { GetAllRoleDtos } from './DataServiceRole';

const baseURL = {
    baseURL:"https://localhost:44337/api/Department/"
}
const controllerURL= '/Department';



export async function  GetAllDepartmentDtos(){
    return axios.get(`${controllerURL}/GetAllDepartmentDtos`).then(result => result.data);
}

export async function GetDepartmentDtoById(id){
    return axios.get(`${controllerURL}/GetDepartmentDtoById/${id}`).then(result => result.data);
}
export async function PostDepartmentDto(departmentDto){
    const result = {
        departmentId : 0,
        departmentName : departmentDto.Name, 
    }
    return axios.post(`${controllerURL}/PostDepartmentDto`,result).then(result =>result.data);
}
export async function PutDepartmentDto(departmentDto){
    const result = {
        departmentId : departmentDto.Id,
        departmentName : departmentDto.Name, 
    }
    return axios.put(`${controllerURL}/PutDepartmentDto`,result).then(result=>result.data);
}
export async function DeleteDepartmentDto(id){
    console.log("happening in DatadepartmenService")
    return axios.delete(`${controllerURL}/${id}`).then(result => result.data);
}
export async function GetDeptRoles(departmentId){
    if(departmentId){
        return axios.get(`${controllerURL}/GetDeptRoles/${departmentId}`).then(results=>results.data);
    }else{
        return await GetAllRoleDtos();
    }
}
export default axios.create(baseURL);