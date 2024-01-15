using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public class PersonsAssignedDevicesDto
    {
        public int PersonId { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonLastName { get; set; }
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string HwModelName { get; set; }
        public string ServiceTag { get; set; }
    }
}
