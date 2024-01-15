using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record PersonDeviceListDto
    {
        public int PersonId { get; init; }
        public int DeviceId { get; init; }
        public DateTime DateTimeStart { get; init; } = DateTime.Now;

    }
}
