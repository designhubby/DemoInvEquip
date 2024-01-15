using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Logic.DomainModel;
using InvEquip.Dto;

namespace InvEquip.Logic.Service
{
    public partial class ApplicationService
    {
        public async Task<IEnumerable<DeviceDto>> GetAllDeviceDtosAsync()
        {
            var allDeviceDtos = await _deviceService.GetAllDeviceDtosAsync();
            return allDeviceDtos;
        }
        public async Task<IEnumerable<DeviceDataDto>> GetAllDeviceDataDtosAsync()
        {
            var allDeviceDataDtos = await _deviceService.GetAllDeviceDataDtosAsync();
            return allDeviceDataDtos;
        }
        public async Task<DeviceDataDto> GetDeviceDataDtoByIdAsync(int id)
        {
            var deviceDataDto = await _deviceService.GetDeviceDataDtoByIdAsync(id);
            return deviceDataDto;
        }
        public async Task PutDeviceDataDto(DeviceDataPostDto deviceDataPostDto)
        {
            await _deviceService.PutDeviceDataDto(deviceDataPostDto);
        }
        public async Task<DeviceDataPostDto> PostDeviceDataDto(DeviceDataPostDto deviceDataPostDto)
        {
            DeviceDataPostDto result = await _deviceService.PostDeviceDataDto(deviceDataPostDto);
            return result;
        }
        public async Task<ResultStatus> DeleteDeviceDataByIdAsync(int id)
        {
            ResultStatus resultStatus = await _deviceService.DeleteDeviceDataByIdAsync(id);
            return resultStatus;
            
        }
    }
}
