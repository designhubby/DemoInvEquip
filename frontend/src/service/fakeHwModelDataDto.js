let HwModelDataDto = [
    {
      "hwModelId": 1,
      "hwModelName": "Unassigned",
      "deviceTypeId": 1,
      "vendorId": 1
    },
    {
      "hwModelId": 2,
      "hwModelName": "TestHwModel02",
      "deviceTypeId": 2,
      "vendorId": 2
    },
    {
      "hwModelId": 3,
      "hwModelName": "TestHwModel03",
      "deviceTypeId": 3,
      "vendorId": 3
    },
    {
      "hwModelId": 4,
      "hwModelName": "TestHwModel04",
      "deviceTypeId": 4,
      "vendorId": 4
    },
    {
      "hwModelId": 5,
      "hwModelName": "TestHwModel05",
      "deviceTypeId": 5,
      "vendorId": 5
    },
    {
      "hwModelId": 6,
      "hwModelName": "TestHwModel06",
      "deviceTypeId": 2,
      "vendorId": 2
    },
    {
      "hwModelId": 7,
      "hwModelName": "TestHwModel07",
      "deviceTypeId": 3,
      "vendorId": 3
    }
  ]

export async function GetAllHwModelDataDtos(){
    return new Promise((resolve, reject)=>{
        setTimeout(()=>{
            resolve(HwModelDataDto);
        },100)
    })
}

export async function GetHwModelDataDtosByDeviceType(deviceTypeId){
  return new Promise((resolve,reject)=>{
    const siblingHwModelList = HwModelDataDto.filter((indiv)=>
      indiv.deviceTypeId == deviceTypeId
    )
    setTimeout(()=>{
      console.log(`deviceTypeList`);
      console.log(siblingHwModelList);
      resolve(siblingHwModelList)}
    )
  })
}

