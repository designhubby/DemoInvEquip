import React, {useEffect, useRef} from 'react';



export const PersonDeviceViewPersonInfo= ({PersonData})=>{

    const FirstName = PersonData.fname;
    const LastName = PersonData.lname;
    const DepartmentName = PersonData.departmentName;
    const RoleName = PersonData.roleName;

    const renderTable = ()=>{
        return(
            <React.Fragment>
                <div>
                    <h1>Person Device Details</h1>
                    
                </div>
                <div className = "row">
                    <div key="labelFirstName" className = "col">
                        Person First Name
                    </div>
                    <div key="FirstName" className = "col">
                        {FirstName}
                    </div>
                    <div key="labelLastName" className = "col">
                        Person Last Name
                    </div>
                    <div key="LastName" className = "col">
                        {LastName}
                    </div>
                </div>
                <div className = "row">
                    <div key="LabelDepartmentName" className = "col">
                        Department Name
                    </div>
                    <div key="DepartmentName" className = "col">
                        {DepartmentName}
                    </div>
                    <div key="LabelRowtName" className = "col">
                        Role Name
                    </div>
                    <div key="RowtName" className = "col">
                        {RoleName}
                    </div>
                </div>
        </React.Fragment>
        )
    }

    return(
        <React.Fragment>
            {renderTable()}
        </React.Fragment>
    )

}
