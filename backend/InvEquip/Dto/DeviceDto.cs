using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public class DeviceDto
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string HwModelName { get; set; }
        public string DeviceTypeName { get; set; }
        public string ServiceTag { get; set; }
    }
}
