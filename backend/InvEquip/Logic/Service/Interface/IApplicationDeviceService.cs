using InvEquip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service
{
    public interface IApplicationDeviceService
    {
        Task<IEnumerable<DeviceDto>> GetAllDeviceDtosAsync();
        Task<IEnumerable<DeviceDataDto>> GetAllDeviceDataDtosAsync();
        Task<DeviceDataDto> GetDeviceDataDtoByIdAsync(int id);
        Task PutDeviceDataDto(DeviceDataPostDto deviceDataPostDto);
        Task<DeviceDataPostDto> PostDeviceDataDto(DeviceDataPostDto deviceDataPostDto);
        Task<ResultStatus> DeleteDeviceDataByIdAsync(int id);
    }
}
