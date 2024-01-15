using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record PersonDeviceIdUnassociateDto
    {
        public int PersonDeviceId { get; init; }
        public DateTime EndDate { get; init; }
    }
}
