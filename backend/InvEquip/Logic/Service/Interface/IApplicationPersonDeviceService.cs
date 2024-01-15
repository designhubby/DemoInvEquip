using InvEquip.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service
{
    public interface IApplicationPersonDeviceService
    {
        Task<DeviceDateDto> ShowCurrentAssignedPCDevice(int personId);
        Task<IList<DeviceDateDto>> ShowPersonsAssignDeviceWithDate(int personId);
        Task<IEnumerable<DeviceDto>> ShowUnassignedDevicesByType(int deviceTypeId);
        Task<IEnumerable<DeviceDateDto>> Show_Person_Actively_Associated_Devices(int personId);
        Task<bool> UnAssociateDevice_Now_By_PersonDeviceId(int personDeviceId);
        Task<bool> UnAssociateDevice_Now_By_PersonDeviceIdList(int[] personDeviceId);
        Task UnAssociateDevice_By_PersonId_And_DeviceId(PersonDeviceIdUnassociateDto personDeviceIdUnassociateDto);
        Task<bool> AttemptAssignPersonToDevice(int personId, int deviceId, DateTime startDate);
        Task<bool> AttemptAssignPersonToDevice(IEnumerable<PersonDeviceListDto> personDeviceListDtos);
        Task<IEnumerable<DeviceDto>> ShowDevicesByQueryObject(int? deviceTypeId, bool retired, bool activeRental, int? contractId, int? hwModelId);
        Task<IEnumerable<PersonDateDto>> ShowDevicesAssignPersonWithDate(int deviceId);
    }
}