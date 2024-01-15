using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Data.Entity
{
    public class HwModel : BaseEntity
    {
        public string HwModelName { get; set; }
        public int DeviceTypeId { get; set; }
        public virtual DeviceType DeviceType { get; set; }

        public int VendorId { get; set; }
        public virtual Vendor Vendor { get;set;}   
        public virtual IList<Device> Devices { get; set; }
    }
}
