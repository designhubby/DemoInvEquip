using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record HwModelListDto
    {
        public int HwModelId { get; set; }
        public string HwModelName { get; set; }
        public string DeviceTypeName { get; set; }
        public string VendorName { get; set; }
    }
}
