using InvEquip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service
{
    public interface IApplicationContractService
    {
        Task<IEnumerable<ContractDataDto>> GetAllContractDataDtos();
        Task<IEnumerable<ContractDataDto>> GetContractsOwnedByVendorByVendorIdAsync(int vendorId);
        Task<IEnumerable<ContractListDto>> GetAllContractListDtos();
        Task<ContractDataDto> GetContractDataDtoById(int id);
        Task<ContractDataDtoNew> CreateContractDataByDto(ContractDataDto contractDataDto);
        Task<ResultStatus> UpdateContractDataByDto(ContractDataDto contractDataDto);
        Task<ResultStatus> DeleteContractByIdAsync(int id);
        Task<IEnumerable<DeviceDto>> GetAllContractDevicesByContractId(int contractId);
    }
}
