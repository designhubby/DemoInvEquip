export const Roles = [
    {
        Id: 1,
        RoleName: "Unassigned",
        DepartmentId: 1,
        Deleted: false,
    },
    {
        Id: 2,
        RoleName: "TestRole02",
        DepartmentId: 1,
        Deleted: false,
    },
    {
        Id: 3,
        RoleName: "TestRole03",
        DepartmentId: 3,
        Deleted: false,
    },
    {
        Id: 4,
        RoleName: "TestRole04",
        DepartmentId: 4,
        Deleted: false,
    },
    {
        Id: 5,
        RoleName: "TestRole05",
        DepartmentId: 2,
        Deleted: false,
    },
    
]

export async function getAllRoles(){
    return new Promise((resolve, reject)=>{
        const newArray =  Roles.map((role)=>(
            {
               Id: role.Id,
               Name: role.RoleName,
               DepartmentId: role.DepartmentId,
               Deleted: false,
           }
        ));
        setTimeout(()=>{
            resolve(newArray);
        },1000)
    })
};
export async function getRole(id) {

    return new Promise((resolve,reject)=>{
        const tempData = Roles.find( (r) => r.Id == id)
        setTimeout(()=>{
            resolve(tempData);
        },1000)
    })
    };