using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.EntityFrameworkCore;
using InvEquip.Data;
using InvEquip.Data.Entity;
using InvEquip.Data.Repository;
using InvEquip.Logic.DomainModel;
using InvEquip.Dto;
using InvEquip.Logic.Service.Extension;
using InvEquip.Data.Repository.QueryObject;

namespace InvEquip.Logic.Service
{
    public partial class ApplicationService : IApplicationPersonDeviceService
    {
        public async Task<IList<DeviceDateDto>> ShowPersonsAssignDeviceWithDate(int personId)
        {
            return await _personDeviceService.GetPersonsAssociatedDevicesWithDates(personId);
        }
        public async Task<IEnumerable<PersonDateDto>> ShowDevicesAssignPersonWithDate(int deviceId)
        {
            return await _personDeviceService.GetDevicesAssignPersonWithDate(deviceId);
        }
        public async Task<bool> AttemptAssignPersonToDevice(int personId, int deviceId, DateTime startDate)
        {
            bool assignmentSuccessful = await _personDeviceService.AssignPersonToDevice(personId, deviceId, startDate);
            return assignmentSuccessful;
        }

        public async Task<bool> AttemptAssignPersonToDevice(IEnumerable<PersonDeviceListDto> personDeviceListDtos)
        {
            
            IEnumerable<PersonDeviceModel> personDeviceModels = personDeviceListDtos.Select(indiv => _mapper.Map<PersonDeviceListDto, PersonDeviceModel>(indiv));
            bool assignmentSuccessful = await _personDeviceService.AssignPersonToDeviceRange(personDeviceModels);


            return assignmentSuccessful;
        }


        public async Task<DeviceDateDto> ShowCurrentAssignedPCDevice(int personId)
        {
            return await _personDeviceService.GetPersonsCurrentAssociatedDeviceWithType(personId, DeviceTypeModel.TestDeviceType02); //Test parameters

        }
        public async Task<IEnumerable<DeviceDto>> ShowUnassignedDevicesByType(int deviceTypeId)
        {
            var FilteredDevices = await _personDeviceService.GetUnassignedDevicesByType(deviceTypeId);
            var deviceDtos = FilteredDevices.Select(fd => _mapper.Map<DeviceModel, DeviceDto>(fd)).ToList();

            return deviceDtos;
        }
        public async Task<IEnumerable<DeviceDateDto>> Show_Person_Actively_Associated_Devices(int personId)
        {
            IEnumerable<PersonDeviceModel> ListOfPersonDevices = await _personDeviceService.Get_Person_Actively_Associated_Devices(personId);
            IEnumerable<DeviceDateDto> deviceDateDtos = await Task.Run(() => ListOfPersonDevices.Select(pd => _mapper.Map<PersonDeviceModel, DeviceDateDto>(pd)));


            return deviceDateDtos;
        }
        public async Task<bool> UnAssociateDevice_Now_By_PersonDeviceId(int personDeviceId)
        {
            DateTime dateTimeNow = DateTime.Now;
            bool success = await _personDeviceService.UnAssociate_PersonFromDevice_By_PersonDeviceId(personDeviceId, dateTimeNow);
            return success;
        }
        public async Task<bool> UnAssociateDevice_Now_By_PersonDeviceIdList(int[] personDeviceId)
        {
            
            bool success = await _personDeviceService.UnAssociate_PersonFromDevice_By_PersonDeviceIdNowList(personDeviceId);
            return success;
        }
        public async Task UnAssociateDevice_By_PersonId_And_DeviceId(PersonDeviceIdUnassociateDto personDeviceIdUnassociateDto)
        {
            var personDeviceId = personDeviceIdUnassociateDto.PersonDeviceId;
            var endDate = personDeviceIdUnassociateDto.EndDate;
            await _personDeviceService.UnAssociate_PersonFromDevice_By_PersonDeviceId(personDeviceId, endDate); ;
        }
        public async Task<IEnumerable<DeviceDto>> ShowDevicesByQueryObject(int? deviceTypeId, bool retired, bool activeRental, int? contractId, int? hwModelId)
        {
            DeviceByQueryObject deviceByTypeQueryObject = new()
            {
                _deviceTypeId = deviceTypeId,
                _deleted = retired,
                _associated = activeRental,
                _contractId = contractId,
                _HwModelId = hwModelId,
            };
            IEnumerable<DeviceDto> resultDto = await  _personDeviceService.GetDevicesByQueryObject(deviceByTypeQueryObject);
            
            return resultDto;

        }


        //public  IEnumerable<DeviceDateDto> Map_PersonDeviceModel_To_DeviceDateDto(IEnumerable<PersonDeviceModel> personDeviceModels)
        //{
        //    IList<DeviceDateDto> deviceDateDtos = new List<DeviceDateDto>();

        //    foreach(PersonDeviceModel personDeviceModel in personDeviceModels)
        //    {
        //        DeviceDateDto deviceDateDto =  _mapper.Map<PersonDeviceModel, DeviceDateDto>(personDeviceModel);
        //        deviceDateDtos.Add(deviceDateDto);
        //    }
        //    return deviceDateDtos;
        //}
    }
}
