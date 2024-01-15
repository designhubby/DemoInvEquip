using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Data.Entity
{
    public class Contract :BaseEntity
    {
        public string ContractName { get; set; }
        public virtual IList<Device> Devices { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}