import axios from "axios";


const baseURL = {
    baseURL:"https://localhost:44337/api/Department/"
}
const controllerURL= '/Department';


/* const axiosIntercept = axios.create(baseURL);
axiosIntercept.interceptors.response.use(response=>{
    if(response.config?.parse){
        console.log("Parsing Response")
    }
    return response;
}, error =>{
    if(error.response.status === 404){
        console.log(`Error 404 ${error.response.status}`);
        console.log(error.response)
        throw error.response;
    }
    if(error.response.status === 405){
        console.log(`Error 405 ${error.response.status}`);
        throw error.response;
    }
    return Promise.reject(error.status);
}) */


export async function ErrorTestDeleteAPI(id){
    console.log(`ErrorTestDeleteAPI ${id}`)
    
    return axios.delete(`${controllerURL}/ErrorTest/${id}`, {parse : true}).then(results => results.data)
}