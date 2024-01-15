using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record DeviceTypeDataDto
    {
        public int DeviceTypeId { get; set; }
        public string DeviceTypeName { get; set; }
    }
}
