let DeviceTypeDataDtos = [
    {
      "deviceTypeId": 1,
      "deviceTypeName": "Unassigned"
    },
    {
      "deviceTypeId": 2,
      "deviceTypeName": "TestDeviceType02"
    },
    {
      "deviceTypeId": 3,
      "deviceTypeName": "TestDeviceType03"
    },
    {
      "deviceTypeId": 4,
      "deviceTypeName": "TestDeviceType04"
    },
    {
      "deviceTypeId": 5,
      "deviceTypeName": "TestDeviceType05"
    },
    {
      "deviceTypeId": 6,
      "deviceTypeName": "TestDeviceType06"
    }
  ];

export async function GetAllDeviceTypesDto(){

    return new Promise((resolve, reject)=>{
        setTimeout(()=>{
            resolve(DeviceTypeDataDtos);
        },100)
    })
}


