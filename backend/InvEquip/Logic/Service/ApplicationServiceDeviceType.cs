
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using InvEquip.Dto;

namespace InvEquip.Logic.Service
{
    public partial class ApplicationService
    {
        public async Task<bool> CheckEntityExists(int id)
        {
            bool result = await _deviceTypeService.CheckEntityExists(id);
            return result;
        }
        public async Task<IEnumerable<DeviceTypeDataDto>> GetDeviceTypeDataDtos()
        {
            IEnumerable<DeviceTypeDataDto> deviceTypeDataDtos = await _deviceTypeService.GetDeviceTypeDataDtos();
            return deviceTypeDataDtos;
        }
        public async Task<DeviceTypeDataDto> GetDeviceTypeDataDtoById(int id)
        {
            DeviceTypeDataDto result = await _deviceTypeService.GetDeviceTypeDataDtoById(id);
            return result;
        }
        public async Task<IEnumerable<HwModelDataDto>> GetHwModelsDataDtoByDeviceTypeIdAsync(int deviceTypeId)
        {
            IEnumerable<HwModelDataDto> hwModelDataDtos = await _deviceTypeService.GetHwModelDataDtosByDeviceTypeId(deviceTypeId);
            return hwModelDataDtos;
        }
        public async Task<DeviceTypeDataDto> CreateDeviceTypeFromDeviceTypeDto(DeviceTypeDataDto deviceTypeDataDto)
        {
            DeviceTypeDataDto result = await _deviceTypeService.CreateDeviceTypeFromDeviceTypeDto(deviceTypeDataDto);
            return result;

        }
        public async Task<bool> UpdateDeviceTypeByDto(DeviceTypeDataDto deviceTypeDataDto)
        {
            bool result = await _deviceTypeService.UpdateDeviceTypeFromDeviceTypeDto(deviceTypeDataDto);
            return result;
        }
        public async Task<ResultStatus> DeleteById(int id)
        {
            ResultStatus resultStatus = await _deviceTypeService.DeleteDeviceTypeById(id);
            return resultStatus;
        }

    }
}
