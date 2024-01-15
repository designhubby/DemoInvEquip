using InvEquip.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Logic.DomainModel
{
    public class HwModelModel : BaseDomainModel<HwModel>
    {
        public HwModelModel(HwModel entity) : base(entity)
        {
            
        }
        public HwModelModel(string HwModelName, int DeviceTypeId, int VendorId): base()
        {
            _entity = new HwModel()
            {
                Deleted = false,
                HwModelName = HwModelName,
                DeviceTypeId = DeviceTypeId,
                VendorId = VendorId,
            };
        }
        public HwModelModel(int HwModelId, string HwModelName, int DeviceTypeId, int VendorId) : base()
        {
            _entity = new HwModel()
            {
                Deleted = false,
                Id = HwModelId,
                HwModelName = HwModelName,
                DeviceTypeId = DeviceTypeId,
                VendorId = VendorId,
            };
        }
        public int HwModelId
        {
            get => _entity.Id;
        }

        public string HwModelName
        {
            get => _entity.HwModelName;
            set => _entity.HwModelName = value;
        }
        public int DeviceTypeId
        {
            get => _entity.DeviceTypeId;
            set => _entity.DeviceTypeId = value;
        }
        public string DeviceTypeName
        {
            get => _entity.DeviceType.DeviceTypeName;
            set => _entity.DeviceType.DeviceTypeName = value;
        }
        public int VendorId
        {
            get => _entity.VendorId;
            set => _entity.VendorId = value;
        }
        public string VendorName
        {
            get => _entity.Vendor.VendorName;
            set => _entity.Vendor.VendorName = value;
        }
        public void ChangeHwModelName(string name)
        {
            _entity.HwModelName = name;
        }

        public void ChangeDeviceTypeTo(int deviceTypeId)
        {
            _entity.DeviceTypeId = deviceTypeId;
        }
        public void ChangeDeviceTypeTo(DeviceType deviceTypeEntity)
        {
            _entity.DeviceType = deviceTypeEntity;
        }
        public void ChangeDeviceTypeTo(DeviceTypeModel deviceTypeModel)
        {
            _entity.DeviceType = deviceTypeModel._entity;
        }

        public void ChangeVendorTo(int id)
        {
            _entity.VendorId = id;
        }
        public void ChangeVendorTo(Vendor vendorEntity)
        {
            _entity.Vendor = vendorEntity;
        }

        public void ChangeVendorTo(VendorModel vendorModel)
        {
            _entity.Vendor = vendorModel._entity;
        }

        public void AddDevices(Device deviceEntity)
        {
            _entity.Devices.Add(deviceEntity);
        }
        public void AddDevices(DeviceModel deviceModel)
        {
            _entity.Devices.Add(deviceModel._entity);
        }

    }
}
