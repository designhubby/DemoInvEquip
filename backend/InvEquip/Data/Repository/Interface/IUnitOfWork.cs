using InvEquip.Data.Entity;
using InvEquip.Logic.DomainModel;
using System.Threading.Tasks;

namespace InvEquip.Data.Repository
{
    public interface IUnitOfWork
    {
        GenericRepository<Department, DepartmentModel> DepartmentRepository { get; }
        PersonRepository PersonRepository { get; }
        RoleRepository RoleRepository { get; }
        GenericRepository<Vendor, VendorModel> VendorRepository { get; }
        PersonDeviceRepository PersonDeviceRepository { get; }
        GenericRepository<Device, DeviceModel> DeviceRepository { get; }
        GenericRepository<Contract, ContractModel> ContractRepository { get; }
        GenericRepository<HwModel, HwModelModel> HwModelRepository { get; }
        GenericRepository<DeviceType, DeviceTypeModel> DeviceTypeRepository { get; }
        //BaseRepository<Webuser> WebUserRepository { get; }
        Task<int > SaveAsync();
    }
}