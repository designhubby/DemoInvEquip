import _ from 'lodash';

let DevicesData=[
  {
    "deviceId": 1,
    "deviceName": "Device0000",
    "deviceTypeId": 1,
    "hwModelId": 1,
    "serviceTag": "A1A",
    "assetNumber": "A0001",
    "notes": "TestNotes",
    "contractId": 1,
    "vendorId": 1
  },
  {
    "deviceId": 2,
    "deviceName": "TestDevice00002",
    "deviceTypeId": 2,
    "hwModelId": 2,
    "serviceTag": "A1A02",
    "assetNumber": "A000102",
    "notes": "TestNotes02",
    "contractId": 2,
    "vendorId": 2
  },
  {
    "deviceId": 3,
    "deviceName": "TestDevice00003",
    "deviceTypeId": 3,
    "hwModelId": 3,
    "serviceTag": "A1A03",
    "assetNumber": "A000103",
    "notes": "TestNotes03",
    "contractId": 3,
    "vendorId": 3
  },
  {
    "deviceId": 4,
    "deviceName": "TestDevice00004",
    "deviceTypeId": 4,
    "hwModelId": 4,
    "serviceTag": "A1A04",
    "assetNumber": "A000104",
    "notes": "TestNotes04",
    "contractId": 4,
    "vendorId": 4
  },
  {
    "deviceId": 5,
    "deviceName": "TestDevice00005",
    "deviceTypeId": 5,
    "hwModelId": 5,
    "serviceTag": "A1A05",
    "assetNumber": "A000105",
    "notes": "TestNotes05",
    "contractId": 5,
    "vendorId": 5
  },
  {
    "deviceId": 6,
    "deviceName": "TestDevice00006",
    "deviceTypeId": 2,
    "hwModelId": 6,
    "serviceTag": "A1A06",
    "assetNumber": "A000106",
    "notes": "TestNotes06",
    "contractId": 6,
    "vendorId": 6
  },
  {
    "deviceId": 7,
    "deviceName": "TestDevice00007",
    "deviceTypeId": 2,
    "hwModelId": 2,
    "serviceTag": "A1A07",
    "assetNumber": "A000107",
    "notes": "TestNotes07",
    "contractId": 2,
    "vendorId": 2
  },
  {
    "deviceId": 8,
    "deviceName": "TestDevice00008",
    "deviceTypeId": 3,
    "hwModelId": 3,
    "serviceTag": "A1A08",
    "assetNumber": "A000108",
    "notes": "TestNotes08",
    "contractId": 3,
    "vendorId": 3
  },
  {
    "deviceId": 9,
    "deviceName": "TestDevice00009",
    "deviceTypeId": 4,
    "hwModelId": 4,
    "serviceTag": "A1A09",
    "assetNumber": "A000109",
    "notes": "TestNotes09",
    "contractId": 4,
    "vendorId": 4
  },
  {
    "deviceId": 10,
    "deviceName": "TestDevice000010",
    "deviceTypeId": 5,
    "hwModelId": 5,
    "serviceTag": "A1A010",
    "assetNumber": "A0001010",
    "notes": "TestNotes010",
    "contractId": 5,
    "vendorId": 5
  },
]

export async function GetAllDeviceData(){

  return new Promise((resolve,reject)=>{
    const allDeviceData = DevicesData;
    setTimeout(()=>{
      resolve(allDeviceData);
    },100)
  })

}

export async function GetDeviceDataById(id){
  return new Promise((resolve, reject)=>{
    var deviceData = DevicesData.find((indiv)=> indiv.deviceId == id);
    setTimeout(()=>{
      resolve(deviceData);
    },100)
  })
}

export async function PostDeviceData(deviceData){

  return new Promise((resolve, reject)=>{
    const foundDevice = _.some(DevicesData,['deviceId' , deviceData.deviceId])
    if(foundDevice){
      var deviceIndex = DevicesData.findIndex((indiv)=>indiv.deviceId == deviceData.deviceId);
      
      console.log('Before Saved Info');
      console.log(DevicesData[deviceIndex]);
      DevicesData[deviceIndex] = deviceData;
      console.log('Saved Info');
      console.log(DevicesData[deviceIndex]);
      resolve("Ok")
    }else{
      DevicesData.push(deviceData);
      resolve("Ok");
    }

  })

}
