using InvEquip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service
{
    public interface IApplicationVendorService
    {
        Task<IEnumerable<VendorDataDto>> GetAllVendorDataDtos();
        Task<IEnumerable<ContractDataDto>> GetContractDataDtosOwnByVendorByVendorId(int vendorId);
        Task<IEnumerable<ContractDataDto>> GetContractDataDtosOwnByVendorByContractId(int contractId);
        Task<VendorDataDto> GetVendorDataDtoByVendorId(int id);
        Task<bool> UpdateVendorDataByDtoAsync(VendorDataDto vendorDataDto);
        Task<VendorDataDto> CreateVendorDataByDtoAsync(VendorDataDto vendorDataDto);
    }
}