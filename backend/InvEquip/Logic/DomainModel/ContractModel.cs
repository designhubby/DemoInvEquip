using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Data.Entity;

namespace InvEquip.Logic.DomainModel
{
    public class ContractModel : BaseDomainModel<Contract>
    {
        public ContractModel(Contract entity) : base(entity)
        {
            
        }
        public ContractModel(string ContractName, DateTime? StartDate, DateTime? EndDate, int VendorId)
        {
            _entity = new Contract()
            {
                Deleted = false,
                ContractName = ContractName,
                StartDate = StartDate,
                EndDate = EndDate,
                VendorId = VendorId,
            };
        }
        public ContractModel(int ContractId, string ContractName, DateTime? StartDate, DateTime? EndDate, int VendorId)
        {
            _entity = new Contract()
            {
                Id = ContractId,
                Deleted = false,
                ContractName = ContractName,
                StartDate = StartDate,
                EndDate = EndDate,
                VendorId = VendorId,
            };
        }

        public int ContractId => _entity.Id;
        
        public string ContractName
        {
            get => _entity.ContractName;
            set => _entity.ContractName = value;
        }
        public DateTime? StartDate
        {
            get => _entity.StartDate;
            set => _entity.StartDate = value;
        }
        public DateTime? EndDate
        {
            get => _entity.EndDate;
            set => _entity.EndDate = value;
        }
        public int VendorId
        {
            get => _entity.VendorId;
            set => _entity.VendorId = value;
        }
        public string VendorName => _entity.Vendor.VendorName;

        public void ChangeContractNameTo(string name)
        {
            _entity.ContractName = name;
        }
        public void ChangeVendorTo(Vendor vendor)
        {
            _entity.Vendor = vendor;
        }
        public void ChangeVendorTo(VendorModel vendorModel)
        {
            _entity.Vendor = vendorModel._entity;
        }
        public void ChangeVendorTo(int id)
        {
            _entity.VendorId = id;
        }

        public void ChangeStartDate(DateTime dateTime)
        {
            _entity.StartDate = dateTime;
        }

        public void ChangeEndDate(DateTime dateTime)
        {
            _entity.EndDate = dateTime;
        }
        public IEnumerable<Device> GetOwnedDevices()
        {
            return _entity.Devices;
        }
        public void AddDevice(Device device)
        {
            _entity.Devices.Add(device);
        }
        public void AddDevice(DeviceModel deviceModel)
        {
            _entity.Devices.Add(deviceModel._entity);
        }

        public void RemoveDevice(Device device)
        {
            _entity.Devices.Remove(device);
        }
        public void RemoveDevice(DeviceModel deviceModel)
        {
            _entity.Devices.Remove(deviceModel._entity);
        }
    }
}
