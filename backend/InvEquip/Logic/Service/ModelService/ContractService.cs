using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvEquip.Data.Entity;
using InvEquip.Data.Repository;
using InvEquip.Dto;
using InvEquip.Logic.DomainModel;

namespace InvEquip.Logic.Service
{
    internal class ContractService : ServiceBase
    {
        internal ContractService(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork, mapper)
        {

        }

        internal async Task<IEnumerable<ContractDataDto>> GetAllContractDataDtos()
        {
            IEnumerable<ContractModel> contractModels = await _unitofwork.ContractRepository.GetAllDomainModelsAsync();
            IEnumerable<ContractDataDto> contractDataDtos = contractModels.Select((indiv)=>_mapper.Map<ContractModel, ContractDataDto>(indiv));
            return contractDataDtos;
        }

        internal async Task<ContractDataDto> GetContractDataDtoById(int id)
        {
            ContractModel contractModel = await _unitofwork.ContractRepository.GetDomainModelByIdAsync(id);
            ContractDataDto contractDataDto = _mapper.Map<ContractModel, ContractDataDto>(contractModel);
            return contractDataDto;
        }

        internal async Task<IEnumerable<DeviceDto>> GetAllContractDevicesByContractId(int contractId)
        {
            ContractModel contractModel = await _unitofwork.ContractRepository.GetDomainModelByIdAsync(contractId);
            IEnumerable<DeviceDto> deviceDtos = contractModel.GetOwnedDevices().Select(indivDevice => _mapper.Map<Device, DeviceDto>(indivDevice));
            return deviceDtos;
        }

        internal async Task<IEnumerable<ContractListDto>> GellAllContractListDtos()
        {
            IEnumerable<ContractModel> contractModels = await _unitofwork.ContractRepository.GetAllDomainModelsAsync();
            IEnumerable<ContractListDto> contractListDtos = contractModels.Select(indiv => _mapper.Map<ContractModel, ContractListDto>(indiv));
            return contractListDtos;
        }

        internal async Task<ContractDataDtoNew> CreateContractDataByDto(ContractDataDto contractDataDto)
        {

            ContractModel contractModel = new ContractModel(contractDataDto.ContractName, contractDataDto.StartDate ?? DateTime.Now , contractDataDto.EndDate, contractDataDto.VendorId);
            await _unitofwork.ContractRepository.AddDomainAsync(contractModel);
            ContractDataDtoNew result = _mapper.Map<ContractModel, ContractDataDtoNew>(contractModel);
            return result;
        }

        internal async Task<ResultStatus> DeleteContractByIdAsync(int id)
        {
            ResultStatus result = await _unitofwork.ContractRepository.DeleteEntityAsyncById(id);
            return result;
        }

        internal async Task<ResultStatus> UpdateContractDataByDtoAsync(ContractDataDto contractDataDto)
        {
            bool entityExists = await _unitofwork.ContractRepository.CheckEntityExists(contractDataDto.ContractId);
            if (entityExists)
            {
                ContractModel contractModel = new ContractModel(contractDataDto.ContractId, contractDataDto.ContractName, contractDataDto.StartDate, contractDataDto.EndDate, contractDataDto.VendorId);
                await _unitofwork.ContractRepository.UpdateDomainAsync(contractModel);
                return ResultStatus.Success;
            }
            else
            {
                return ResultStatus.NotFound;
            }
        }

        internal async Task<IEnumerable<ContractDataDto>> GetContractsOwnedByVendorByVendorIdAsync(int vendorId)
        {
            IEnumerable<ContractModel> contractModels = await _unitofwork.ContractRepository.GetAllDomainModelsWhereAsync(c => c.VendorId == vendorId);
            IEnumerable<ContractDataDto> contractDataDtos = contractModels.Select((indiv) => _mapper.Map<ContractModel, ContractDataDto>(indiv));
            return contractDataDtos;
        }
    }
}
