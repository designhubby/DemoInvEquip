using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvEquip.Logic.DomainModel;
using InvEquip.Data.Entity;
using InvEquip.Data.Repository;
using InvEquip.Dto;

namespace InvEquip.Logic.Service
{
    public class HwModelService : ServiceBase
    {
        public HwModelService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<HwModelDataDto>> GetAllHwModelDataDtosAsync()
        {
            var allHwModels = await _unitofwork.HwModelRepository.GetAllDomainModelsAsync();
            IEnumerable<HwModelDataDto> hwModelDataDto = allHwModels.Select((indiv) => _mapper.Map<HwModelModel, HwModelDataDto>(indiv));
            return hwModelDataDto;
        }

        public async Task<IEnumerable<HwModelListDto>> GetAllHwModelListDtosAsync()
        {
            var allHwModels = await _unitofwork.HwModelRepository.GetAllDomainModelsAsync();
            IEnumerable<HwModelListDto> hwModelListDtos = allHwModels.Select(indiv => _mapper.Map<HwModelModel, HwModelListDto>(indiv));
            return hwModelListDtos;
        }

        public async Task<HwModelDataDto> GetHwModelDataDtoByIdAsync(int id)
        {
            HwModelModel hwModelModel = await _unitofwork.HwModelRepository.GetDomainModelByIdAsync(id);
            HwModelDataDto hwModelDataDto = _mapper.Map<HwModelModel, HwModelDataDto>(hwModelModel);
            return hwModelDataDto;
        }

        public async Task<HwModelDataDto> CreateHwModelFromDto(HwModelDataDto hwModelDataDto)
        {
            HwModelModel hwModelModel = new HwModelModel(hwModelDataDto.HwModelName, hwModelDataDto.DeviceTypeId, hwModelDataDto.VendorId);
            await _unitofwork.HwModelRepository.AddDomainAsync(hwModelModel);
            HwModelDataDto result =  _mapper.Map<HwModelModel, HwModelDataDto>(hwModelModel);
            return result;


        }

        public async Task<ResultStatus> UpdateHwModelByDto(HwModelDataDto hwModelDataDto)
        {
            bool entityExists = await _unitofwork.HwModelRepository.CheckEntityExists(hwModelDataDto.HwModelId);
            if (entityExists)
            {
                HwModelModel hwModelModel = new HwModelModel(hwModelDataDto.HwModelId, hwModelDataDto.HwModelName, hwModelDataDto.DeviceTypeId, hwModelDataDto.VendorId);
                await _unitofwork.HwModelRepository.UpdateDomainAsync(hwModelModel);
                return ResultStatus.Success;
            }
            else
            {
                //Bad request Error
                return ResultStatus.NotFound;
            }
        }

        public async Task<ResultStatus> DeleteById(int id)
        {
            bool exists = await _unitofwork.HwModelRepository.CheckEntityExists(id);
            if (exists)
            {
                ResultStatus result = await _unitofwork.HwModelRepository.DeleteEntityAsyncById(id);
                return result;  
            }
            else
            {
                return ResultStatus.NotFound;
            }


        }

        public async Task<HwModelListDto> GetHwModelDtoByIdAsync(int id)
        {
            HwModelModel hwModelModel = await _unitofwork.HwModelRepository.GetDomainModelByIdAsync(id);
            HwModelListDto hwModelListDto = _mapper.Map<HwModelModel, HwModelListDto>(hwModelModel);
            return hwModelListDto;
        }
    }
}
