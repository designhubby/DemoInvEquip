using InvEquip.Data.Entity;

namespace InvEquip.Logic.DomainModel
{
    public class DeviceModel : BaseDomainModel<Device>
    {
        public int DeviceId => _entity.Id;
        public string DeviceName
        {
            get => _entity.DeviceName;
            set => _entity.DeviceName = value;
        }
        public int HwModelId
        {
            get => _entity.HwModelId;
            set => _entity.HwModelId = value;
        }
        public string HwModelName
        {
            get => _entity.HwModel.HwModelName;
        }
        public string ServiceTag
        { 
            get => _entity.ServiceTag;
            set => _entity.ServiceTag = value;
        }
        public string AssetNumber
        {
            get => _entity.AssetNumber;
            set => _entity.AssetNumber = value;
        }
        public string Notes
        {
            get => _entity.Notes;
            set => _entity.Notes = value;
        }
        public int ContractId
        {
            get => _entity.ContractId;
            set => _entity.ContractId = value;
        }

        public int DeviceTypeId => _entity.HwModel.DeviceTypeId;
        public string DeviceTypeName
        {
            get => _entity.HwModel.DeviceType.DeviceTypeName;
        }

        public int VendorId => _entity.Contract.VendorId;
        public DeviceModel(Device entity) : base(entity)
        {

        }
        public DeviceModel(int DeviceId, string DeviceName, int HwModelId, string ServiceTag, string  AssetNumber, string Notes, int ContractId) :base()
        {
            _entity = new Device()
            {
                Id = DeviceId,
                DeviceName = DeviceName,
                HwModelId = HwModelId,
                ServiceTag = ServiceTag,
                AssetNumber = AssetNumber,
                Notes = Notes,
                ContractId = ContractId,
            };
        }
        public DeviceModel(string DeviceName, int HwModelId, string ServiceTag, string AssetNumber, string Notes, int ContractId) : base()
        {
            _entity = new Device()
            {
                DeviceName = DeviceName,
                HwModelId = HwModelId,
                ServiceTag = ServiceTag,
                AssetNumber = AssetNumber,
                Notes = Notes,
                ContractId = ContractId,
            };
        }

        public void ChangeDeviceNameTo(string name)
        {
            _entity.DeviceName = name;
        }

        public void ChangeDeviceServiceTagTo(string serviceTag)
        {
            _entity.ServiceTag = serviceTag;
        }

        public void ChangeAssetNumberTo(string assetNumber)
        {
            _entity.AssetNumber = assetNumber;
        }

        public void ChangeNotesTo(string notes)
        {
            _entity.Notes = notes;
        }

        public void ChangeHwModelTo(HwModelModel hwModelModel)
        {
            _entity.HwModel = hwModelModel._entity;
        }

        

    }
}