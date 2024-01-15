using AutoMapper;
using InvEquip.Data.Entity;
using InvEquip.Data.Repository;
using InvEquip.Data.Repository.QueryObject;
using InvEquip.Dto;
using InvEquip.Logic.DomainModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace InvEquip.Logic.Service
{
    public class PersonDeviceService : ServiceBase, IPersonDeviceService
    {


        public PersonDeviceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<bool> AssignPersonToDevice(int personId, int deviceId, DateTime startDate)
        {
            bool isAvailable = await ValidateDeviceIsAvailableForStartDate(deviceId);
            if (isAvailable)
            {
                var personDeviceModel = await _unitofwork.PersonDeviceRepository.AddDomainAsync(new PersonDeviceModel(personId, deviceId, startDate));
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AssignPersonToDeviceRange(IEnumerable<PersonDeviceModel> personDeviceModels)
        {
            bool isAvailable = true;
            foreach (PersonDeviceModel indivPdm in personDeviceModels)
            {
                 isAvailable = await ValidateDeviceIsAvailableForStartDate(indivPdm.DeviceId);
                if (!isAvailable)
                {
                    break;
                }
            }
            
            if (isAvailable)
            {
                bool result = await _unitofwork.PersonDeviceRepository.AddDomainRangeAsync(personDeviceModels);
                return result;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ValidateDeviceIsAvailableForStartDate(int deviceModelId)
        {
            IEnumerable<PersonDeviceModel> personDeviceModels = await _unitofwork.PersonDeviceRepository.GetAllDomainModelsAsync();
            bool available = !(personDeviceModels.Any(pd => pd.DeviceId == deviceModelId && pd.EndDate == null));
            return available;
        }
        //public async Task<bool> CheckIfDeviceIsAvailableDuringDatePeriod(int deviceModelId, DateTime startDate, DateTime endDate)
        //{
        //    bool IsAvailable = true;

        //    //Find all Rentals with device
        //    var PersonDeviceModels = await _unitOfWork.PersonDeviceRepository.GetAllDomainModelsWhereAsync(x => x.DeviceId == deviceModelId);
        //    //Iterate thru all models
        //    foreach(PersonDeviceModel personDeviceModel in PersonDeviceModels)
        //    {
        //        //Is ReqEndDate < RentalStartDate or Is ReqStartDate > RentalEndDate
        //        if (endDate < personDeviceModel.StartDate || startDate > personDeviceModel.EndDate )
        //        {
        //            //current rental time is not in conflict, Next Check
        //        }
        //        else
        //        {
        //            //current rental time is in conflict, mark as conflict, return result
        //            IsAvailable = false;
        //            break;
        //        }
        //    }
        //    return IsAvailable;


        //}
        public async Task<bool> UnAssociate_PersonFromDevice_By_PersonDeviceId(int personDeviceId, DateTime endDate)
        {
            
            PersonDeviceModel _personDeviceModel = await _unitofwork.PersonDeviceRepository.GetDomainModelByIdAsync(personDeviceId);

            if (_personDeviceModel != null)
            {
                bool success = _personDeviceModel.SetEndDate(endDate);
                if (success)
                    await _unitofwork.SaveAsync();
                
                return true;
            }
            return false;
        }
        public async Task<bool> UnAssociate_PersonFromDevice_By_PersonDeviceIdNowList(int[] personDeviceId)
        {
            DateTime endDate = DateTime.Now;
            IEnumerable<PersonDeviceModel> personDeviceModels =  await Task.WhenAll(personDeviceId.Select( indiv =>   _unitofwork.PersonDeviceRepository.GetDomainModelByIdAsync(indiv)));
            if (personDeviceModels != null)
            {
                HashSet<bool> result = personDeviceModels.Select(indiv => indiv.SetEndDate(endDate)).ToHashSet();
                if (!result.Contains(false))
                {

                    await _unitofwork.SaveAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            } else {
                return false;
            };
        }
        public async Task<bool> UnAssociate_PersonFromDevice_By_PersonId_And_DeviceId(int personId, int deviceId, DateTime endDate)
        {
            bool unassociateSuccess = false;
            IEnumerable<PersonDeviceModel> _personDeviceModels = await _unitofwork.PersonDeviceRepository.GetAllDomainModelsWhereAsync(pd => pd.PersonId == personId && pd.DeviceId == deviceId && pd.EndDate == null);
            if (_personDeviceModels != null)
            {
                PersonDeviceModel personDeviceModel = _personDeviceModels.FirstOrDefault();
                unassociateSuccess = personDeviceModel.SetEndDate(endDate);
                await _unitofwork.SaveAsync();
            }
            return unassociateSuccess;
        }

        internal async Task<IEnumerable<PersonDeviceModel>> Get_Person_Actively_Associated_Devices(int personId)
        {
            IEnumerable<PersonDeviceModel> personDeviceModel = await _unitofwork.PersonDeviceRepository.GetAllDomainModelsWhereAsync(pd => pd.PersonId == personId && pd.EndDate == null);
            return personDeviceModel;
        }

        internal async Task<IEnumerable<DeviceModel>> GetAssociatedNonRetiredDevices()
        {
            IEnumerable<DeviceModel> deviceModels = await Task.Run(()=>_unitofwork.PersonDeviceRepository.GetAllAssociatedNonRetiredDevices().AsEnumerable().ToDomainModels<Device, DeviceModel>());
            
            return deviceModels;
        }
        internal async Task<IEnumerable<DeviceDto>> GetAssociatedNonRetiredDevices(DeviceByQueryObject queryObj)
        {

            IEnumerable<DeviceModel> deviceModels = await Task.Run(() => _unitofwork.PersonDeviceRepository.GetAllAssociatedNonRetiredDevices().AddFilters(queryObj.AsExpression()).AsEnumerable().ToDomainModels<Device, DeviceModel>());
            IEnumerable<DeviceDto> deviceDtos = deviceModels.Select(indiv => _mapper.Map<DeviceModel, DeviceDto>(indiv));
            return deviceDtos;
        }


        public async Task<IList<DeviceDateDto>> GetPersonsAssociatedDevicesWithDates(int personId)
        {
            var PersonDeviceModels_By_PersonId = await _unitofwork.PersonDeviceRepository.GetAllDomainModelsWhereAsync(pd => pd.PersonId == personId);
            List<DeviceDateDto> personDeviceModelsByPersonIdDtos = new List<DeviceDateDto>();
            foreach (var pdm in PersonDeviceModels_By_PersonId)
            {
                DeviceDateDto deviceDateDto = _mapper.Map<PersonDeviceModel, DeviceDateDto>(pdm);
                personDeviceModelsByPersonIdDtos.Add(deviceDateDto);
            }
            return personDeviceModelsByPersonIdDtos;

        }
        internal async Task<IEnumerable<PersonDateDto>> GetDevicesAssignPersonWithDate(int deviceId)
        {
            IEnumerable<PersonDeviceModel> _personDeviceModel = await _unitofwork.PersonDeviceRepository.GetAllDomainModelsWhereAsync(pd => pd.DeviceId == deviceId);
            IEnumerable<PersonDateDto> personDateDtos = _personDeviceModel.Select(indiv => _mapper.Map<PersonDeviceModel, PersonDateDto>(indiv));
            return personDateDtos;
        }

        public async Task<DeviceDateDto> GetPersonsCurrentAssociatedDeviceWithType(int personId, int deviceTypeId)
        {
            var ListOfPersonDeviceModel = await _unitofwork.PersonDeviceRepository.GetAllDomainModelsWhereAsync(pd => pd.PersonId == personId && pd.Device.HwModel.DeviceTypeId == deviceTypeId);
            if(ListOfPersonDeviceModel.Count() == 0)
            {
                return null;
            }
            var DescendingListOfPersonDeviceModel = ListOfPersonDeviceModel.OrderByDescending(pdm => pdm.StartDate);
            var latestPersonDeviceModel = DescendingListOfPersonDeviceModel.FirstOrDefault();
            var latestPersonDeviceModelDto = _mapper.Map<PersonDeviceModel, DeviceDateDto>(latestPersonDeviceModel);
            return latestPersonDeviceModelDto;

        }
        internal async Task<IEnumerable<DeviceModel>> GetUnassignedDevicesByType(int deviceTypeId)
        {
            var ListOfPDwithEndDateNullandDeviceTypeGiven = await _unitofwork.PersonDeviceRepository.GetAllDomainModelsWhereAsync(pdm => pdm.Device.HwModel.DeviceTypeId == deviceTypeId && pdm.EndDate == null);
            var distinctSet_Of_AssociatedDevices_By_DeviceId = ListOfPDwithEndDateNullandDeviceTypeGiven.GroupBy(pd => pd.DeviceId).Select(pd => pd.First().DeviceId);
            var ListOf_Available_Devices = await _unitofwork.DeviceRepository.GetAllDomainModelsWhereAsync(d => !distinctSet_Of_AssociatedDevices_By_DeviceId.Contains(d.Id) && d.HwModel.DeviceTypeId == deviceTypeId);
            //var ListOf_Available_Devices = await _unitOfWork.PersonDeviceRepository.GetAllDomainModelsWhereAsync(pdm => !distinctSet_Of_AssociatedDevices_By_DeviceId.Contains(pdm.DeviceId));
            return ListOf_Available_Devices;
        }

        internal async Task<IEnumerable<DeviceDto>> GetUnassignedNonRetiredDevicesByQueryObject(DeviceByQueryObject deviceByTypeQueryObject)
        {
            IEnumerable<DeviceModel> resultEntities = await Task.Run(()=>
                _unitofwork.PersonDeviceRepository
                .GetAllUnassociatedNonRetiredDevices()
                .AddFilters(deviceByTypeQueryObject.AsExpression())
                .AsEnumerable()
                .ToDomainModels<Device,DeviceModel>());
            IEnumerable<DeviceDto> resultDto = resultEntities.Select(indiv => _mapper.Map<DeviceModel, DeviceDto>(indiv));
            return resultDto;

        }
        internal async Task<IEnumerable<DeviceDto>> GetDevicesByQueryObject(DeviceByQueryObject deviceByQryObj)
        {
            bool associated = deviceByQryObj._associated;
            bool retired = deviceByQryObj._deleted;

            if (retired)
            {
                IEnumerable<DeviceModel> deviceModels = await Task.Run(() =>
                _unitofwork.PersonDeviceRepository
                .GetAllDevices()
                .AddFilters(deviceByQryObj.AsExpression())
                .AsEnumerable()
                .ToDomainModels<Device, DeviceModel>().ToList());
                IEnumerable<DeviceDto> deviceDtos = deviceModels.Select((indiv) => _mapper.Map<DeviceModel, DeviceDto>(indiv));
                return deviceDtos;
            }
            
            if (associated)
            {
                IEnumerable<DeviceModel> deviceModels = await Task.Run(() => 
                _unitofwork.PersonDeviceRepository
                .GetAllAssociatedNonRetiredDevices()
                .AddFilters(deviceByQryObj.AsExpression())
                .AsEnumerable()
                .ToDomainModels <Device, DeviceModel>().ToList());
                IEnumerable<DeviceDto> deviceDtos = deviceModels.Select(indiv => _mapper.Map<DeviceModel, DeviceDto>(indiv));
                return deviceDtos;
            }
            else
            {
                IEnumerable<DeviceModel> deviceModels = await Task.Run(() =>
                _unitofwork.PersonDeviceRepository
                .GetAllUnassociatedNonRetiredDevices()
                .AddFilters(deviceByQryObj.AsExpression())
                .AsEnumerable()
                .ToDomainModels<Device, DeviceModel>().ToList());
                IEnumerable<DeviceDto> deviceDtos = deviceModels.Select(indiv => _mapper.Map<DeviceModel, DeviceDto>(indiv));
                return deviceDtos;

            }
        }

        //internal async Task<IList<DeviceDto>> GetUnassignedDevicesByType(int deviceTypeId)
        //{
        //    //list of devices = i, Not Assigned with No records where PersonDevice.EndDate = null or no listing in PersonDevice
        //    //Filter out
        //    //1. All Devices with PersonDevice.EndDate = null
        //    //2. All other devices not in PersonDevice list should be available

        //    //list1 = devices with PersonDevice.EndDate = null and Device.HwModel.DeviceType.typeId=XXX
        //    //list2 = all devices of Device.HwModel.DeviceType.typeId=XXX
        //    //list3 = available devices
        //    //foreach(d in list2)
        //    //if d is NOT in list1{  add d to list3 }
        //    //
        //    bool ResetToFalse = false;
        //    var ListOfPDwithEndDateNullandDeviceTypeGiven = await _unitOfWork.PersonDeviceRepository.GetAllDomainModelsWhereAsync(pdm => pdm.Device.HwModel.DeviceTypeId == deviceTypeId && pdm.EndDate == null);

        //    var ListOfAllDeviceswithEndDateNullandDeviceTypeGiven = new List<DeviceModel>();
        //    foreach (PersonDeviceModel pdm in ListOfPDwithEndDateNullandDeviceTypeGiven)
        //    {
        //        var deviceModel = await _unitOfWork.DeviceRepository.GetDomainModelByIdAsync(pdm.DeviceId);
        //        ListOfAllDeviceswithEndDateNullandDeviceTypeGiven.Add(deviceModel);
        //    }
        //    var ListOfAllDevices = await _unitOfWork.DeviceRepository.GetAllDomainModelsAsync();

        //    var FilteredDevices = new List<DeviceDto>();
        //    bool foundMatch = false;
        //    foreach (DeviceModel dm in ListOfAllDevices)
        //    {
        //        foreach (DeviceModel deviceModelWithNullEndDateAndDeviceTypeGiven in ListOfAllDeviceswithEndDateNullandDeviceTypeGiven)
        //        {
        //            if (dm.DeviceId == deviceModelWithNullEndDateAndDeviceTypeGiven.DeviceId)
        //            {
        //                foundMatch = true;
        //                break;

        //            }
        //        }
        //        if (!foundMatch)
        //        {
        //            var DeviceModelDto = _mapper.Map<DeviceModel, DeviceDto>(dm);
        //            FilteredDevices.Add(DeviceModelDto);
        //            foundMatch = ResetToFalse;
        //        }
        //        foundMatch = ResetToFalse;
        //    };
        //    return FilteredDevices;
        //}
    }
}
