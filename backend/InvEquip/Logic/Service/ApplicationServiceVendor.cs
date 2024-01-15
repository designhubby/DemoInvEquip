using InvEquip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service
{
    public partial class ApplicationService
    {
        public async Task<IEnumerable<VendorDataDto>> GetAllVendorDataDtos()
        {
            IEnumerable<VendorDataDto> vendorDataDtos = await _vendorService.GetAllVendorDataDtos();
            return vendorDataDtos;
        }

        public async Task<IEnumerable<ContractDataDto>> GetContractDataDtosOwnByVendorByVendorId(int vendorId)
        {
            IEnumerable<ContractDataDto> contractDataDto = await _vendorService.GetContractDataDtoByVendorByVendorId(vendorId);
            return contractDataDto;
        }

        public async Task<IEnumerable<ContractDataDto>> GetContractDataDtosOwnByVendorByContractId(int contractId)
        {
            IEnumerable<ContractDataDto> contractModels = await _vendorService.GetContractDataDtoByVendorByContractIdAsync(contractId);
            return contractModels;
        }
        public async Task<VendorDataDto> GetVendorDataDtoByVendorId(int id)
        {
            VendorDataDto vendorDataDto = await _vendorService.GetVendorDataDtoByVendorId(id);
            return vendorDataDto;
        }
        public async Task<VendorDataDto> CreateVendorDataByDtoAsync(VendorDataDto vendorDataDto)
        {
            VendorDataDto result = await _vendorService.CreateVendorDataByDtoAsync(vendorDataDto);
            return result;
        }
        public async Task<bool> UpdateVendorDataByDtoAsync(VendorDataDto vendorDataDto)
        {
            bool result = await _vendorService.UpdateVendorDataByDtoAsync(vendorDataDto);
            return result;
        }

    }
}
