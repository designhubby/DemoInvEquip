
const DeviceDateDto = [
    {
        personDeviceId:     1,
        deviceId:           2,
        deviceName:         "TestDevice00002",
        hwModelName:        "TestHwModel02",
        deviceType:         "TestDeviceType02",
        startDate:          "2000-04-01T00:00:00",
        endDate:            "2000-05-01T00:00:00"
    },
    {
        personDeviceId:   2,
        deviceId:         3,
        deviceName:       "TestDevice00003",
        hwModelName:      "TestHwModel03",
        deviceType:       "TestDeviceType03",
        startDate:        "2000-06-01T00:00:00",
        endDate:          "2000-07-01T00:00:00"
    },
    {
        personDeviceId:   3,
        deviceId:         2,
        deviceName:       "TestDevice00002",
        hwModelName:      "TestHwModel02",
        deviceType:       "TestDeviceType02",
        startDate:        "2000-06-01T00:00:00",
        endDate:          "2000-07-01T00:00:00"
    },
    {
        personDeviceId:      4,
        deviceId:            3,
        deviceName:      "TestDevice00003",
        hwModelName:     "TestHwModel03",
        deviceType:      "TestDeviceType03",
        startDate:       "2000-04-01T00:00:00",
        endDate:         null,
    },
    {
        personDeviceId:       5,
        deviceId:             3,
        deviceName:       "TestDevice00003",
        hwModelName:      "TestHwModel03",
        deviceType:       "TestDeviceType03",
        startDate:        "2021-04-01T00:00:00",
        endDate:          null,
    },

]

export async function ShowCurrentAssignedPCDevice(_personId){ //fake results
    const personId = _personId;
    return new Promise((resolve,reject)=>{
        setTimeout(()=>{
            resolve([DeviceDateDto[3], DeviceDateDto[1]])
        },1000)
    })
}

export async function ShowAvailableDevices(){

    return new Promise((resolve, reject)=>{
        setTimeout(()=>{
            console.log(DeviceDateDto.filter((indiv)=>indiv.endDate == null))
            resolve(DeviceDateDto.filter((indiv)=>indiv.endDate == null));
        },100)
    })

}

export async function PostPersonDeviceAssociation(associationList){
    return new Promise((resolve,reject)=>{
        setTimeout(()=>{
            if(associationList){
                console.log(associationList)
                resolve(true);
            }else{
                resolve(false);
            }
        })
    })
}