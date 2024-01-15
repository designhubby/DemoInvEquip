import { getDepartmentById } from "./fakeServiceDepartment";
import { getRole } from "./fakeServiceRole";

export const People = [
    {
        _id : 3,
        fname : "FirstNameTest03",
        lname: "LastNameTest03",
        roleId: 3,
        deleted: false,
    },
    {
        _id : 1,
        fname : "FirstNameTestab",
        lname: "LastNameTest",
        roleId: 1,
        deleted: false,
    },
    {
        _id : 2,
        fname : "FirstNameTest02",
        lname: "LastNameTest02ab",
        roleId: 2,
        deleted: false,
    },
    {
        _id : 4,
        fname : "FirstNameTest04",
        lname: "LastNameTest04",
        roleId: 4,
        deleted: false,
    },
]

export  function getAllPeople() {
    return new Promise((resolve,reject)=>{
        const allPeople = People.filter(p => p);
        setTimeout(()=>{
            resolve(allPeople);
        },100)
    })
     
};

export async function getPerson(id){
    return  ( new Promise((resolve, reject)=>{
        setTimeout(()=>{
            resolve(People.find((person) => person._id == id));
        },2000)
        })

    )
}

export async function getPersonWithFriendlyDetails(id){
    //get role -> roleid
    //get department -> department(find roleid).name
    const person = People.find((person)=> person._id == id);
    const role = await getRole(id);
    console.log(`Role id: ${role.DepartmentId}`)
    const department = await getDepartmentById(role.DepartmentId);
    console.log(department)
    const departmentName = department.Name;
    const roleName = role.RoleName;
    const personDetails ={
        _id : person._id,
        fname : person.fname,
        lname: person.lname,
        roleId: person.roleId,
        roleName: roleName,
        departmentName : departmentName,

    }
    return new Promise((resolve,reject)=>{
        setTimeout(()=>{
            resolve(personDetails)
        })
    })
}

export async function getPeopleWhere(name){

    return (
            new Promise((resolve, reject)=>{
                const filteredData = (data, query) => (
                     data.filter((indivPerson)=>
                        indivPerson.fname.toLowerCase().indexOf(query.toLowerCase()) !== -1 ||
                        indivPerson.lname.toLowerCase().indexOf(query.toLowerCase()) !== -1
                    )
                )
                const filteredPeople = filteredData(People, name);
                resolve(filteredPeople);
            })
        )
}

    
