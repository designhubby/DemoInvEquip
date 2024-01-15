using InvEquip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service
{
    public interface IApplicationDeviceTypeService
    {
        Task<bool> CheckEntityExists(int id);
        Task<IEnumerable<DeviceTypeDataDto>> GetDeviceTypeDataDtos();
        Task<IEnumerable<HwModelDataDto>> GetHwModelsDataDtoByDeviceTypeIdAsync(int deviceTypeId);
        Task<DeviceTypeDataDto> CreateDeviceTypeFromDeviceTypeDto(DeviceTypeDataDto deviceTypeDataDto);
        Task<bool> UpdateDeviceTypeByDto(DeviceTypeDataDto deviceTypeDataDto);
        Task<DeviceTypeDataDto> GetDeviceTypeDataDtoById(int id);
        Task<ResultStatus> DeleteById(int id);
    }
}