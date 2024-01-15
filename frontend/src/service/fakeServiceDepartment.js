
import { getAllRoles, getRole, Roles } from './fakeServiceRole';

const Departments = [
    {
        Id : 1,
        Name : "department01",
    },
    {
        Id : 2,
        Name : "department02",
    },
    {
        Id : 3,
        Name : "department03",
    },
    {
        Id : 4,
        Name : "department04",
    }
]

export  const getAllDepartments = async () =>{
    return new Promise((resolve,reject) => {
        console.log(`getallDepartments: Starting`)
        setTimeout(()=>{
            resolve(Departments.filter(d => d));
        },1)
    })
}

export  const getDepartmentById = async (id) => (Promise.resolve(Departments.find(d => d.Id == id)));

export  const getDepartmentsRoleByDepartmentId = async (id) =>{
    return new Promise((resolve, reject)=>{
        setTimeout(async ()=>{
            
            const allRoles =  await getAllRoles()
            .then((result)=>result.filter((r)=> r.DepartmentId == id));
            console.log("fakeServiceDepartment -> getDepartmentsRoleByDepartmentId: ", allRoles, "id: " , id)
            resolve(
                //return roles where roles.departmentid = department.id
                allRoles
            )
        },2000)
    })
    

}