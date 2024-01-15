import _ from 'lodash';
import { DeviceType } from '../components/common/constants';

const DeviceDtos = [
    {
        "deviceId": 1,
        "deviceName": "Device0000",
        "hwModelName": "Unassigned",
        "deviceTypeName": "Unassigned",
        "serviceTag": "A1A"
    },
    {
        "deviceId": 2,
        "deviceName": "TestDevice00002",
        "hwModelName": "TestHwModel02",
        "deviceTypeName": "TestDeviceType02",
        "serviceTag": "A1A02"
    },
    {
        "deviceId": 3,
        "deviceName": "TestDevice00003",
        "hwModelName": "TestHwModel03",
        "deviceTypeName": "TestDeviceType03",
        "serviceTag": "A1A03"
    },
    {
        "deviceId": 4,
        "deviceName": "TestDevice00004",
        "hwModelName": "TestHwModel04",
        "deviceTypeName": "TestDeviceType04",
        "serviceTag": "A1A04"
    },
    {
        "deviceId": 5,
        "deviceName": "TestDevice00005",
        "hwModelName": "TestHwModel05",
        "deviceTypeName": "TestDeviceType05",
        "serviceTag": "A1A05"
    },
    {
        "deviceId": 6,
        "deviceName": "TestDevice00006",
        "hwModelName": "TestHwModel06",
        "deviceTypeName": "TestDeviceType02",
        "serviceTag": "A1A06"
    },
    {
        "deviceId": 7,
        "deviceName": "TestDevice00007",
        "hwModelName": "TestHwModel02",
        "deviceTypeName": "TestDeviceType02",
        "serviceTag": "A1A07"
    },
    {
        "deviceId": 8,
        "deviceName": "TestDevice00008",
        "hwModelName": "TestHwModel03",
        "deviceTypeName": "TestDeviceType03",
        "serviceTag": "A1A08"
    },
    {
        "deviceId": 9,
        "deviceName": "TestDevice00009",
        "hwModelName": "TestHwModel04",
        "deviceTypeName": "TestDeviceType04",
        "serviceTag": "A1A09"
    },
    {
        "deviceId": 10,
        "deviceName": "TestDevice000010",
        "hwModelName": "TestHwModel05",
        "deviceTypeName": "TestDeviceType05",
        "serviceTag": "A1A010"
    },
    {
        "deviceId": 11,
        "deviceName": "TestDevice000011",
        "hwModelName": "TestHwModel06",
        "deviceTypeName": "TestDeviceType02",
        "serviceTag": "A1A011"
    },
    {
        "deviceId": 12,
        "deviceName": "TestDevice000012",
        "hwModelName": "TestHwModel02",
        "deviceTypeName": "TestDeviceType02",
        "serviceTag": "A1A012"
    },
    {
        "deviceId": 13,
        "deviceName": "TestDevice000013",
        "hwModelName": "TestHwModel03",
        "deviceTypeName": "TestDeviceType03",
        "serviceTag": "A1A013"
    },
    {
        "deviceId": 14,
        "deviceName": "TestDevice000014",
        "hwModelName": "TestHwModel04",
        "deviceTypeName": "TestDeviceType04",
        "serviceTag": "A1A014"
    },
    {
        "deviceId": 15,
        "deviceName": "TestDevice000015",
        "hwModelName": "TestHwModel05",
        "deviceTypeName": "TestDeviceType05",
        "serviceTag": "A1A015"
    },
    {
        "deviceId": 16,
        "deviceName": "TestDevice000016",
        "hwModelName": "TestHwModel06",
        "deviceTypeName": "TestDeviceType02",
        "serviceTag": "A1A016"
    },
]

export async function GetAllDevices(){

    return new Promise((resolve, reject)=>{
        const devices = DeviceDtos.filter(r=>r);
        setTimeout(()=>{
            resolve(devices);
        },100)
    })

}

export async function GetDevicesFiltered(devicetype, retired, activerental){
    // dt: 1 ,2 ,3
    // retired: true / false
    // activerental: bool
    let results;
    console.log(`Filters: DeviceType = ${devicetype} Retired = ${retired} activeRental = ${activerental}`)
    if(devicetype && devicetype.toLowerCase() != "all"){
        results = DeviceDtos.filter((indiv)=> indiv.deviceTypeName.toLowerCase() == devicetype.toLowerCase());

    }else{
        results = DeviceDtos;
    }
    if(activerental && results.length >1 ){
        results = results.slice(0,2)
    }

    return new Promise((resolve,reject)=>{
        setTimeout(()=>{
            resolve(results)
        },100)
    })

}