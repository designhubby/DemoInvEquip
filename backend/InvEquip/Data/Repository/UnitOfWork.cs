using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Data.Repository;
using InvEquip.Data.Entity;
using InvEquip.Logic.DomainModel;

namespace InvEquip.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext _applicationDbContext;
        public PersonRepository _personRepository;
        public GenericRepository<Department, DepartmentModel> _departmentRepository;
        public GenericRepository<Vendor, VendorModel> _vendorRepository;
        public RoleRepository _roleRepository;
        public GenericRepository<Contract, ContractModel> _contractRepository;
        public GenericRepository<DeviceType, DeviceTypeModel> _deviceTypeRepository;
        public GenericRepository<HwModel, HwModelModel> _hwModelRepository;
        public GenericRepository<Device, DeviceModel> _deviceRepository;
        public PersonDeviceRepository _personDeviceRepository;
        //public BaseRepository<Webuser> _webUserRepository;

        public UnitOfWork(ApplicationDbContext DbContext)
        {
            _applicationDbContext = DbContext;
        }

        public PersonRepository PersonRepository
        {
            get
            {
                return _personRepository ?? (_personRepository = new PersonRepository(_applicationDbContext));
            }
        }

        public GenericRepository<Department, DepartmentModel> DepartmentRepository
        {
            get
            {
                return _departmentRepository ?? (_departmentRepository = new GenericRepository<Department, DepartmentModel>(_applicationDbContext));
            }
        }

        public GenericRepository<Vendor, VendorModel> VendorRepository
        {
            get
            {
                return _vendorRepository ?? (_vendorRepository = new GenericRepository<Vendor, VendorModel>(_applicationDbContext));
            }
        }
        public RoleRepository RoleRepository
        {
            get
            {
                return _roleRepository ?? (_roleRepository = new RoleRepository(_applicationDbContext));
            }
        }
        public GenericRepository<Contract, ContractModel> ContractRepository
        {
            get
            {
                return _contractRepository ?? (_contractRepository = new GenericRepository<Contract, ContractModel>(_applicationDbContext));
            }
        }

        public GenericRepository<DeviceType, DeviceTypeModel> DeviceTypeRepository
        {
            get
            {
                return _deviceTypeRepository ?? (_deviceTypeRepository = new GenericRepository<DeviceType, DeviceTypeModel>(_applicationDbContext));
            }
        }
        public GenericRepository<HwModel, HwModelModel> HwModelRepository
        {
            get
            {
                return _hwModelRepository ?? (_hwModelRepository = new GenericRepository<HwModel, HwModelModel>(_applicationDbContext));
            }
        }

        public GenericRepository<Device, DeviceModel> DeviceRepository
        {
            get
            {
                return _deviceRepository ?? (_deviceRepository = new GenericRepository<Device, DeviceModel>(_applicationDbContext));
            }
        }
        public PersonDeviceRepository PersonDeviceRepository
        {
            get
            {
                return _personDeviceRepository ?? (_personDeviceRepository = new PersonDeviceRepository(_applicationDbContext));
            }
        }
        /*public BaseRepository<Webuser> WebUserRepository
        {
            get
            {
                return _webUserRepository ?? (_webUserRepository = new BaseRepository<Webuser>(_applicationDbContext));
            }
        }*/

        public async Task<int > SaveAsync()
        {
            int result = await _applicationDbContext.SaveChangesAsync();
            return result;
        }


    }
}
