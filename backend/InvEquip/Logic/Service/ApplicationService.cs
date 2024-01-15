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
using InvEquip.Logic.Service.Helper;

namespace InvEquip.Logic.Service
{
    public partial class ApplicationService : IApplicationPersonService, IApplicationDepartmentService, IApplicationDeviceService, IApplicationHwModelService, IApplicationContractService, IApplicationVendorService, IApplicationDeviceTypeService, IApplicationRoleService
    {
        IUnitOfWork _uow;
        PersonDeviceService _personDeviceService;
        PersonService _personService;
        DepartmentService _departmentService;
        DeviceService _deviceService;
        HwModelService _hwModelService;
        ContractService _contractService;
        VendorService _vendorService;
        DeviceTypeService _deviceTypeService;
        RoleService _roleService;
        //AuthService _authService;
        IMapper _mapper;

        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mapper = mapper;
            _personDeviceService = new PersonDeviceService(_uow, mapper);
            _personService = new PersonService(_uow, mapper);
            _departmentService = new DepartmentService(_uow, mapper);
            _deviceService = new DeviceService(_uow, mapper);
            _hwModelService = new HwModelService(_uow, mapper);
            _contractService = new ContractService(_uow, mapper);
            _vendorService = new VendorService(_uow, mapper);
            _deviceTypeService = new DeviceTypeService(_uow, mapper);
            _roleService = new RoleService(_uow, mapper);
            //_authService = new AuthService(_uow, mapper);
        }

        public async Task testMethod()
        {
            var dm = await _uow.RoleRepository.GetDomainModelByIdAsync(1);
            await _uow.SaveAsync();
        }
        public string testRun()
        {
            Console.WriteLine("Testing");
            return "One";
        }



    }







}

