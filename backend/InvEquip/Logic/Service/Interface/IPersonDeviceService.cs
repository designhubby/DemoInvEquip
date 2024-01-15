using InvEquip.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service
{
    public interface IPersonDeviceService
    {
        Task<bool> AssignPersonToDevice(int personId, int deviceId, DateTime startDate);
        Task<IList<DeviceDateDto>> GetPersonsAssociatedDevicesWithDates(int personId);
        Task<DeviceDateDto> GetPersonsCurrentAssociatedDeviceWithType(int personId, int deviceTypeId);
        Task<bool> UnAssociate_PersonFromDevice_By_PersonDeviceIdNowList(int[] personDeviceId);
        Task<bool> UnAssociate_PersonFromDevice_By_PersonDeviceId(int personDeviceId, DateTime endDate);
        Task<bool> UnAssociate_PersonFromDevice_By_PersonId_And_DeviceId(int personId, int deviceId, DateTime endDate);
        Task<bool> ValidateDeviceIsAvailableForStartDate(int deviceModelId);
    }
}