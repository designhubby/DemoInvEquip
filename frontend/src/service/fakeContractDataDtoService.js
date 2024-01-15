
let ContractDataDto = [
    {
      "contractId": 1,
      "contractName": "Unassigned",
      "startDate": "1980-01-01T00:00:00",
      "endDate": "1980-01-02T00:00:00",
      "vendorId": 1
    },
    {
      "contractId": 2,
      "contractName": "TestContract02",
      "startDate": "1980-01-01T00:00:00",
      "endDate": "1980-01-02T00:00:00",
      "vendorId": 2
    },
    {
      "contractId": 3,
      "contractName": "TestContract03",
      "startDate": "1980-01-01T00:00:00",
      "endDate": "1980-01-02T00:00:00",
      "vendorId": 3
    },
    {
      "contractId": 4,
      "contractName": "TestContract04",
      "startDate": "1980-01-01T00:00:00",
      "endDate": "1980-01-02T00:00:00",
      "vendorId": 4
    },
    {
      "contractId": 5,
      "contractName": "TestContract05",
      "startDate": "1980-01-01T00:00:00",
      "endDate": "1980-01-02T00:00:00",
      "vendorId": 5
    },
    {
      "contractId": 6,
      "contractName": "TestContract06",
      "startDate": "1980-01-01T00:00:00",
      "endDate": "1980-01-02T00:00:00",
      "vendorId": 6
    },
    {
      "contractId": 7,
      "contractName": "TestContract07",
      "startDate": "1980-01-01T00:00:00",
      "endDate": "1980-01-02T00:00:00",
      "vendorId": 2
    },
    {
      "contractId": 8,
      "contractName": "TestContract08",
      "startDate": "1980-01-01T00:00:00",
      "endDate": "1980-01-02T00:00:00",
      "vendorId": 3
    },
    {
      "contractId": 9,
      "contractName": "TestContract09",
      "startDate": "1980-01-01T00:00:00",
      "endDate": "1980-01-02T00:00:00",
      "vendorId": 4
    },
    {
      "contractId": 10,
      "contractName": "TestContract010",
      "startDate": "1980-01-01T00:00:00",
      "endDate": "1980-01-02T00:00:00",
      "vendorId": 5
    },
]


export async function GetAllContractDataDtos(){
  return new Promise((resolve, reject)=>{
      setTimeout(()=>{
          resolve(ContractDataDto);
      },100)
  })
}


export async function GetContractDataDtosByVendorId(vendorId){
  return new Promise((resolve, reject)=>{
    const filteredDtos = ContractDataDto.filter((indiv)=>indiv.vendorId==vendorId);
    setTimeout(()=>{
      resolve(filteredDtos);
    },100)
  })
}

export async function GetSiblingContractDataDtosByContractId(contractId){
  return new Promise((resolve, reject)=>{
    const contractEntity = ContractDataDto.find((indiv)=>indiv.contractId == contractId);

    const vendorId = contractEntity && contractEntity.vendorId 
    const contractEntitySiblings = GetContractDataDtosByVendorId(vendorId);
    setTimeout(()=>{
      resolve(contractEntitySiblings)
    })
  })
}