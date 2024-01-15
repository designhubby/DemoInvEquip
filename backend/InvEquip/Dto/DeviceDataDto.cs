using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record DeviceDataDto
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public int DeviceTypeId { get; set; }
        public int HwModelId { get; set; }
        public string ServiceTag { get; set; }
        public string AssetNumber { get; set; }
        public string Notes { get; set; }
        public int ContractId { get; set; }
        public int VendorId { get; set; }
    }
}
