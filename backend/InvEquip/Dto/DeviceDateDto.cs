using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record DeviceDateDto
    {
        public int PersonDeviceId { get; set; }
        public int DeviceId {get;set;}
        public string DeviceName { get; set; }
        public string HwModelName { get; set; }
        public string DeviceType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }
}
