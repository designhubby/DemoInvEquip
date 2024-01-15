using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvEquip.Data.Entity;
using InvEquip.Logic.DomainModel;
using InvEquip.Dto;

namespace InvEquip.Data.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonDto, PersonModel>()
                .ForMember(dest => dest.PersonId, o=>o.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.FirstName, o => o.MapFrom(src => src.Fname))
                .ForMember(dest => dest.LastName, o => o.MapFrom(src => src.Lname))
                .ForMember(dest => dest.RoleId, o => o.MapFrom(src => src.RoleId));
            CreateMap<PersonModel, PersonDto>()
                .ForMember(dest => dest.PersonId, o=>o.MapFrom(src=>src.PersonId))
                .ForMember(dest => dest.Fname, o => o.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Lname, o => o.MapFrom(src => src.LastName))
                .ForMember(dest => dest.RoleId, o => o.MapFrom(src => src.RoleId));
            CreateMap<PersonModel, PersonDetailsDto>()
                .ForMember(dest => dest.PersonId, o => o.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.Fname, o => o.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Lname, o => o.MapFrom(src => src.LastName))
                .ForMember(dest => dest.RoleId, o => o.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.RoleName, o => o.MapFrom(src => src.RoleName))
                .ForMember(dest => dest.DepartmentId, o => o.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.DepartmentName, o => o.MapFrom(src => src.DepartmentName));
            CreateMap<HwModelModel, HwModelDataDto>();
            CreateMap<HwModelModel, HwModelListDto>();
            CreateMap<ContractModel, ContractDataDto>();
            CreateMap<ContractModel, ContractDataDtoNew>()
                .ForMember(dest => dest.ContractId, o => o.MapFrom(src => src.ContractId))
                .ForMember(dest => dest.ContractName, o => o.MapFrom(src => src.ContractName))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, o => o.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.VendorId, o => o.MapFrom(src => src.VendorId));
            CreateMap<ContractModel, ContractListDto>();
            CreateMap<VendorModel, VendorDataDto>();
            CreateMap<DeviceTypeModel, DeviceTypeDataDto>();
            CreateMap<DeviceModel, DeviceDataDto>();
            CreateMap<DeviceModel, DeviceDataPostDto>();
            CreateMap<DeviceModel, DeviceDto>()
                .ForMember(dest => dest.DeviceId, o => o.MapFrom(src => src.DeviceId))
                .ForMember(dest => dest.DeviceName, o => o.MapFrom(src => src.DeviceName))
                .ForMember(dest => dest.ServiceTag, o => o.MapFrom(src => src.ServiceTag))
                .ForMember(dest => dest.HwModelName, o => o.MapFrom(src => src.HwModelName))
                .ForMember(dest=>dest.DeviceTypeName, o=>o.MapFrom(src=>src.DeviceTypeName));
            CreateMap<DeviceDto, DeviceModel>()
                .ForMember(dest => dest.DeviceId, o => o.MapFrom(src => src.DeviceId))
                .ForMember(dest => dest.DeviceName, o => o.MapFrom(src => src.DeviceName))
                .ForMember(dest => dest.HwModelName, o => o.MapFrom(src => src.HwModelName))
                .ForMember(dest => dest.DeviceTypeName, o => o.MapFrom(src => src.DeviceTypeName))
                .ForMember(dest => dest.ServiceTag, o => o.MapFrom(src => src.ServiceTag));
            CreateMap<Device, DeviceDto>()
                .ForMember(dest => dest.DeviceId, o => o.MapFrom(src => src.Id))
                .ForMember(dest => dest.DeviceName, o => o.MapFrom(src => src.DeviceName))
                .ForMember(dest => dest.ServiceTag, o => o.MapFrom(src => src.ServiceTag))
                .ForMember(dest => dest.HwModelName, o => o.MapFrom(src => src.HwModel.HwModelName))
                .ForMember(dest => dest.DeviceTypeName, o=>o.MapFrom(src=>src.HwModel.DeviceType.DeviceTypeName));
            CreateMap<PersonDeviceModel, DeviceDateDto>()
                .ForMember(dest => dest.PersonDeviceId, o => o.MapFrom(src => src.PersonDeviceId))
                .ForMember(dest => dest.DeviceId, o => o.MapFrom(src => src.DeviceId))
                .ForMember(dest => dest.DeviceName, o => o.MapFrom(src => src.DeviceName))
                .ForMember(dest => dest.HwModelName, o => o.MapFrom(src => src.HwModelName))
                .ForMember(dest => dest.DeviceType, o => o.MapFrom(src => src.DeviceTypeName))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, o => o.MapFrom(src => src.EndDate));
            CreateMap<PersonDeviceModel, PersonDateDto>()
                .ForMember(dest => dest.PersonDeviceId, o => o.MapFrom(src => src.PersonDeviceId))
                .ForMember(dest => dest.Fname, o => o.MapFrom(src => src.Fname))
                .ForMember(dest => dest.Lname, o => o.MapFrom(src => src.Lname))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, o => o.MapFrom(src => src.EndDate));
            CreateMap<DepartmentModel, DepartmentDto>()
                .ForMember(dest => dest.DepartmentId, o => o.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.DepartmentName, o => o.MapFrom(src => src.DepartmentName));
            CreateMap<RoleModel, RoleDepartmentDto>();
            CreateMap<RoleModel, RoleDto>()
                .ForMember(dest => dest.RoleId, o => o.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.RoleName, o => o.MapFrom(src => src.RoleName))
                .ForMember(dest =>dest.DepartmentId, o=>o.MapFrom(src => src.DepartmentId));
            CreateMap<PersonDeviceListDto, PersonDeviceModel>()
                .ForMember(dest => dest.DeviceId, o => o.MapFrom(src => src.DeviceId))
                .ForMember(dest => dest.PersonId, o => o.MapFrom(src => src.PersonId))
                .ForMember(dest => dest.StartDate, o => o.MapFrom(src => src.DateTimeStart));
            //CreateMap<Webuser, WebuserDto>();
        }
    }
}
