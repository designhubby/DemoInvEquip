let VendorDataDtos =[
    {
      "vendorId": 1,
      "vendorName": "Unassigned"
    },
    {
      "vendorId": 2,
      "vendorName": "TestVendor02"
    },
    {
      "vendorId": 3,
      "vendorName": "TestVendor03"
    },
    {
      "vendorId": 4,
      "vendorName": "TestVendor04"
    },
    {
      "vendorId": 5,
      "vendorName": "TestVendor05"
    },
    {
      "vendorId": 6,
      "vendorName": "TestVendor06"
    }
  ]

  export async function GetAllVendorDataDtos(){
      return new Promise((resolve, reject)=>{
          setTimeout(()=>{
              resolve(VendorDataDtos);
          },100)
      })
  }


