using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Data.Entity
{
    public class DeviceType : BaseEntity
    {
        public string DeviceTypeName { get; set; }
        public virtual IList<HwModel> HwModels { get; set; } 
    }
}