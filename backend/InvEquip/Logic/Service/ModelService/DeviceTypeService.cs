using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using InvEquip.Data.Entity;
using InvEquip.Data.Repository;
using InvEquip.Dto;
using InvEquip.Logic.DomainModel;

namespace InvEquip.Logic.Service
{
    public class DeviceTypeService : ServiceBase
    {
        public DeviceTypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public async Task<bool> CheckEntityExists(int id)
        {
            bool results = await _unitofwork.DeviceTypeRepository.CheckEntityExists(id);
            return results;
        }
        public async Task<IEnumerable<DeviceTypeDataDto>> GetDeviceTypeDataDtos()
        {
            IEnumerable<DeviceTypeModel> deviceTypeModels =await  _unitofwork.DeviceTypeRepository.GetAllDomainModelsAsync();
            IEnumerable<DeviceTypeDataDto> deviceTypeDataDtos = deviceTypeModels.Select((indiv) => _mapper.Map<DeviceTypeModel, DeviceTypeDataDto>(indiv));
            return deviceTypeDataDtos;
       
        }

        public async Task<DeviceTypeDataDto> GetDeviceTypeDataDtoById(int id)
        {
            DeviceTypeModel deviceTypeModel = await _unitofwork.DeviceTypeRepository.GetDomainModelByIdAsync(id);
            DeviceTypeDataDto result = _mapper.Map<DeviceTypeModel, DeviceTypeDataDto>(deviceTypeModel);
            return result;
        }

        public async Task<IEnumerable<HwModelDataDto>> GetHwModelDataDtosByDeviceTypeId(int deviceTypeId)
        {
            DeviceTypeModel deviceTypeModel = await _unitofwork.DeviceTypeRepository.GetDomainModelByIdAsync(deviceTypeId);
            IEnumerable<HwModelDataDto> hwModelDataDtos = deviceTypeModel.DeviceTypeOwnedHwModelModels.Select(indiv => _mapper.Map<HwModelModel, HwModelDataDto>(indiv));
            return hwModelDataDtos;
        }

        public async Task<ResultStatus> DeleteDeviceTypeById(int id)
        {
            bool entityExists = await CheckEntityExists(id);
            if (entityExists)
            {
                //Dependency or success
                
                ResultStatus result = await _unitofwork.DeviceTypeRepository.DeleteEntityAsyncById(id);
                return result;
            }
            else
            {
                return ResultStatus.NotFound;
            }
        }
        public async Task<bool> HasDependencies(int id)
        {
            bool hasDependencies = await _unitofwork.DeviceTypeRepository.HasDependenciesByIdAsync(id);
            return hasDependencies;
            //check each property for existence of contents
        }

        public async Task<DeviceTypeDataDto> CreateDeviceTypeFromDeviceTypeDto(DeviceTypeDataDto deviceTypeDataDto)
        {
            DeviceTypeModel deviceTypeModel = new DeviceTypeModel(deviceTypeDataDto.DeviceTypeName);
            await _unitofwork.DeviceTypeRepository.AddDomainAsync(deviceTypeModel);
            DeviceTypeDataDto resultDto = _mapper.Map<DeviceTypeModel, DeviceTypeDataDto>(deviceTypeModel);
            return resultDto;
        }
        public async Task<bool> UpdateDeviceTypeFromDeviceTypeDto(DeviceTypeDataDto deviceTypeDataDto)
        {
            DeviceTypeModel deviceTypeModel = new DeviceTypeModel(deviceTypeDataDto.DeviceTypeId, deviceTypeDataDto.DeviceTypeName);
            try
            {
                await _unitofwork.DeviceTypeRepository.UpdateDomainAsync(deviceTypeModel);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
