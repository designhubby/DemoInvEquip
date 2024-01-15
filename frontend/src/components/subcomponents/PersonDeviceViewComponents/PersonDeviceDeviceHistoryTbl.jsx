import { Constants } from "../../common/constants"

export const PersonDeviceDeviceHistoryTblColumns = [

    
    {
        label: "PersonDeviceId",
        datakey: "personDeviceId",
        visible: false,
        type: Constants.text,
    },    
    {
        label: "PersonId",
        datakey: "personId",
        visible: false,
        type: Constants.text,
    },
    {
        label: "First Name",
        datakey: "fname",
        visible: true,
        type: Constants.text,
    },
    {
        label: "Last Name",
        datakey: "lname",
        visible: true,
        type: Constants.text,
    },
    {
        label: "Start Date",
        datakey: "startDate",
        visible: true,
        type: Constants.date,
    },  
    {
        label: "End Date",
        datakey: "endDate",
        visible: true,
        type: Constants.date,
    },  
    {
        label: "View Person",
        actionKey: "personId",
        visible: true,
        type: Constants.button,
        action : (funct, actionkey)=>(
            <button type = "button" className = "btn btn-primary" onClick = {()=>funct.onHandleEditClick(actionkey)}>View Person</button>
        )
    },

]