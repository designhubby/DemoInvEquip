import axios from "axios";
import { TranslatePropertyKeysToDto } from "../util/translatePropertyKeys";

const baseUrl = {
    baseURL:`${process.env.REACT_APP_BaseURL}/Person/`, 
}
const controllerURL ='/Person';

export function getPeopleWhere(data, name){
    return (
        new Promise((resolve, reject)=>{
            const filteredData = (data, query) => (
                 data.filter((indivPerson)=>
                    indivPerson.fname.toLowerCase().indexOf(query.toLowerCase()) !== -1 ||
                    indivPerson.lname.toLowerCase().indexOf(query.toLowerCase()) !== -1
                )
            )
            const filteredPeople = filteredData(data, name);
            resolve(filteredPeople);
        })
    )
}
export async function GetPersonDetailsDto(){
    return axios.get(`${controllerURL}/PersonDetailsDto`).then(results=>results.data);
}

export async function getPersonDetailsById(id){
    const data = await axios.get(`${controllerURL}/GetDetailsById/${id}`).then(result => result.data);
    console.log('getPersonDetailsById')
    console.log(data)
    return data
}

export async function PostPerson(personDto){
    const sendDto = TranslatePropertyKeysToDto(personDto, "person")
    return axios.post(baseUrl.baseURL, sendDto).then(response=>response.data);
}
export async function UpdatePersonByDto(personDto){
    const sendDto = TranslatePropertyKeysToDto(personDto, "person")
    return axios.put(baseUrl.baseURL,sendDto).then(response=>response.data);
}
export async function DeletePerson(id){
    return axios.delete(`${baseUrl.baseURL}${id}`).then(response => response.data);
}


export default axios.create(baseUrl);

