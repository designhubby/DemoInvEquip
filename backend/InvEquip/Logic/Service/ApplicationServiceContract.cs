using InvEquip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service
{
    public partial class ApplicationService
    {
        public async Task<IEnumerable<ContractDataDto>> GetAllContractDataDtos()
        {
            var allContractDtos = await _contractService.GetAllContractDataDtos();
            return allContractDtos;
        }
        public async Task<IEnumerable<ContractListDto>> GetAllContractListDtos()
        {
            IEnumerable<ContractListDto> contractListDtos = await _contractService.GellAllContractListDtos();
            return contractListDtos;
        }
        public async Task<ContractDataDto> GetContractDataDtoById(int id)
        {
            ContractDataDto contractDataDto = await _contractService.GetContractDataDtoById(id);
            return contractDataDto;
        }
        public async Task<IEnumerable<DeviceDto>> GetAllContractDevicesByContractId(int contractId)
        {
            IEnumerable<DeviceDto> deviceDtos = await _contractService.GetAllContractDevicesByContractId(contractId);
            return deviceDtos;
        }
        public async Task<IEnumerable<ContractDataDto>> GetContractsOwnedByVendorByVendorIdAsync(int vendorId)
        {
            IEnumerable<ContractDataDto> contractDataDtos = await _contractService.GetContractsOwnedByVendorByVendorIdAsync(vendorId);
            return contractDataDtos;
        }
        public async Task<ContractDataDtoNew> CreateContractDataByDto(ContractDataDto contractDataDto)
        {
            ContractDataDtoNew result = await _contractService.CreateContractDataByDto(contractDataDto);
            return result;
        }
        public async Task<ResultStatus> UpdateContractDataByDto(ContractDataDto contractDataDto)
        {
            ResultStatus result = await _contractService.UpdateContractDataByDtoAsync(contractDataDto);
            return result;

        }
        public async Task<ResultStatus> DeleteContractByIdAsync(int id)
        {
            ResultStatus result = await _contractService.DeleteContractByIdAsync(id);
            return result;
        }

    }
}
