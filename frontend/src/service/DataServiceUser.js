import axios from "axios";

const controllerURL= '/User';

export async function GetUserInfo(){
    console.log(`GetUser Info Service`)
    return axios.get(`${controllerURL}`, { withCredentials: true, headers:{'Accept': 'application/json',
    'Content-Type': 'application/json' } }).then(response => response.data);
}

export async function EditUserInfo(UserDataChangeDto){
    console.log("DataSrv : Edit User Info");
    return axios.post(`${controllerURL}/${UserDataChangeDto.id}`, UserDataChangeDto).then(response => response.data);
}

export async function ChangeUserPassword(UserPasswordChangeDto){
    return axios.post(`${controllerURL}`,UserPasswordChangeDto).then(response => response.data);
}
