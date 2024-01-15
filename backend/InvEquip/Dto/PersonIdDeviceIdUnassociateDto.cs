using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record PersonIdDeviceIdUnassociateDto
    {
        public int PersonId { get; init; }
        public int DeviceId { get; init; }
        public DateTime EndDate { get; init; }
    }
}
