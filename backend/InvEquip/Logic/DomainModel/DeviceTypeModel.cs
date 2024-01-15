using InvEquip.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Logic.DomainModel
{
    public class DeviceTypeModel : BaseDomainModel<DeviceType>
    {
        public static int TestDeviceType02 => 2;
        public static int TestDeviceType03 => 3;
        public DeviceTypeModel(DeviceType deviceTypeEntity) : base(deviceTypeEntity)
        {

        }
        public DeviceTypeModel(string DeviceTypeName)
        {
            _entity = new DeviceType()
            {
                DeviceTypeName = DeviceTypeName,
            };
        }
        public DeviceTypeModel(int DeviceTypeId, string DeviceTypeName)
        {
            _entity = new DeviceType()
            {
                Id = DeviceTypeId,
                DeviceTypeName = DeviceTypeName,
            };
        }
        public int DeviceTypeId => _entity.Id;

        public IEnumerable<HwModelModel> DeviceTypeOwnedHwModelModels => _entity.HwModels.ToList().Select((indiv) => new HwModelModel(indiv));
        public string DeviceTypeName
        {
            get => _entity.DeviceTypeName;
            set => _entity.DeviceTypeName = value;
        }

        public void ChangeDeviceTypeNameTo(string name)
        {
            _entity.DeviceTypeName = name;
        }
        public void AddHwModel(HwModel hwModelEntity)
        {
            _entity.HwModels.Add(hwModelEntity);
        }

        public void AddHwModel(HwModelModel hwModelModel)
        {
            _entity.HwModels.Add(hwModelModel._entity);
        }

        public void DeleteHwModel(HwModel hwModelEntity)
        {
            _entity.HwModels.Remove(hwModelEntity);
        }
        public void DeleteHwModel(HwModelModel hwModelModel)
        {
            _entity.HwModels.Remove(hwModelModel._entity);
        }
    }
}
