using InvEquip.Data.Repository;
using InvEquip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvEquip.Logic.DomainModel;
using InvEquip.Data.Entity;

namespace InvEquip.Logic.Service
{
    public class DeviceService : ServiceBase
    {
        public DeviceService(IUnitOfWork uow, IMapper mapper): base(uow, mapper)
        {
        }

        public async Task<IEnumerable<DeviceDto>> GetAllDeviceDtosAsync()
        {
            var allDevicesModel = await _unitofwork.DeviceRepository.GetAllDomainModelsAsync();
            var allDevicesDto = allDevicesModel.Select((indivModel) => _mapper.Map<DeviceModel, DeviceDto>(indivModel));
            return allDevicesDto;
        }

        internal async Task<IEnumerable<DeviceDataDto>> GetAllDeviceDataDtosAsync()
        {
            
                var allDevicesModel = await _unitofwork.DeviceRepository.GetAllDomainModelsAsync();
                var allDevicesDataDto = allDevicesModel.Select((indiv) =>
                {
                    return _mapper.Map<DeviceModel, DeviceDataDto>(indiv);
                });
                return allDevicesDataDto;
        }

        internal async Task PutDeviceDataDto(DeviceDataPostDto deviceDataPostDto)
        {
            DeviceModel deviceModel = new DeviceModel(deviceDataPostDto.DeviceId, deviceDataPostDto.DeviceName, deviceDataPostDto.HwModelId, deviceDataPostDto.ServiceTag, deviceDataPostDto.AssetNumber, deviceDataPostDto.Notes, deviceDataPostDto.ContractId);
            await _unitofwork.DeviceRepository.UpdateDomainAsync(deviceModel);
        }

        internal async Task<ResultStatus> DeleteDeviceDataByIdAsync(int id)
        {
            bool hasAssociations = await _unitofwork.PersonDeviceRepository.CheckEntityExistsWhere(pd => pd.DeviceId == id && pd.EndDate == null);
            if (!hasAssociations)
            {
                return await _unitofwork.DeviceRepository.NoDependencyCheckDeleteById(id);
                 
            }
            else
            {
                return ResultStatus.NotAllowed;
            }
        }

        internal async Task<DeviceDataPostDto> PostDeviceDataDto(DeviceDataPostDto deviceDataPostDto)
        {
            DeviceModel deviceModel = new DeviceModel(deviceDataPostDto.DeviceName, deviceDataPostDto.HwModelId, deviceDataPostDto.ServiceTag, deviceDataPostDto.AssetNumber, deviceDataPostDto.Notes, deviceDataPostDto.ContractId);
            try
            {
                DeviceModel resultModel =  await _unitofwork.DeviceRepository.AddDomainAsync(deviceModel);
                DeviceDataPostDto resultDto = _mapper.Map<DeviceModel, DeviceDataPostDto>(resultModel);
                return resultDto;
            }
            catch (Exception e)
            {
                return null;
            };
        }
        internal async Task<DeviceDataDto> GetDeviceDataDtoByIdAsync(int id)
        {
            var deviceData = await _unitofwork.DeviceRepository.GetDomainModelByIdAsync(id);
            var deviceDataDto = _mapper.Map<DeviceModel, DeviceDataDto>(deviceData);
            return deviceDataDto;
        }
    }
}
