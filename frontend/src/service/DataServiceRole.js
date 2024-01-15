import axios from "axios";
import { TranslatePropertyKeysToDto } from './../util/translatePropertyKeys';


const baseURL = {
    baseURL : 'https://localhost:44337/api/Role',
}

const controllerURL ='/Role';

export const GetAllRoleDtos= async ()=> {
    return axios.get(`${controllerURL}/RoleDepartmentsAll`).then(results => results.data);
}
export const GetRoleById = async (id) => {
    return axios.get(`${controllerURL}/GetRoleById/${id}`).then(results => results.data);
}
export const PostRoleDto = async(roleDto) =>{
    const submitDto = TranslatePropertyKeysToDto(roleDto,'role');/* 
    const translatedDto = {
        roleId : 0,
        roleName : roleDto.Name,
        departmentId: roleDto.DepartmentId,
    } */
    console.log(`submitDto`);
    console.log(submitDto);
    return axios.post(`${controllerURL}/CreateRoleDepartment`,submitDto).then(results =>results.data);
}
export const PutRoleDto = async(roleDto)=>{
    const submitDto = TranslatePropertyKeysToDto(roleDto,'role');
    console.log(`submitDto`);
    console.log(submitDto);
    return axios.put(`${controllerURL}/UpdateRoleDepartmentByDto`, submitDto).then(results =>results.data);

}
export const DeleteRoleById = async (id)=>{
    return axios.delete(`${controllerURL}/${id}`).then(results=>results.data);
}

export default axios.create(baseURL);