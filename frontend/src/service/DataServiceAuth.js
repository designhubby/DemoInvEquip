import axios from "axios";

const controllerURL= '/AuthManagement';

export async function WebLogin(webuserLoginDto){
    console.log(`webLogin DataService`);
    console.log(webuserLoginDto)
    return axios.post(`${controllerURL}/Login`, webuserLoginDto, { withCredentials: true, headers:{'Accept': 'application/json',
    'Content-Type': 'application/json' }}).then(response=> response.data);
}

export async function WebRegister(webuserDto){
    console.log(`webuserdto send`);
    console.log(webuserDto);
    return axios.post(`${controllerURL}/Register`,webuserDto).then(response => response.data);
}
export async function WebLogout(){
    console.log(`Weblogout service happening`)
    return axios.post(`${controllerURL}/Logout`,null, { withCredentials: true, headers:{'Accept': 'application/json',
    'Content-Type': 'application/json' }}).then(response => response.data);
}

export async function GetAuthenticationStatus(){
    console.log(`DataService running AuthenticationStatus`);
    return axios.post(`${controllerURL}/AuthenticationStatus`, null, { withCredentials: true, headers:{'Accept': 'application/json',
    'Content-Type': 'application/json' }}).then(response => response.data);
}