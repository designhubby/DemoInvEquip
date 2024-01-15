using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record HwModelDataDto
    {
        public int HwModelId { get; set; }
        public string HwModelName { get; set; }
        public int DeviceTypeId { get; set; }
        public int VendorId { get; set; }
    }
}
