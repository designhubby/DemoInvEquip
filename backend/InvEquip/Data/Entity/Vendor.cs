using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Data.Entity
{
    public class Vendor:BaseEntity
    {
        public string VendorName { get; set; }
        public virtual IList<Contract> Contracts { get; set; }
        public virtual IList<HwModel> HwModels { get; set; }
        
    }
}
