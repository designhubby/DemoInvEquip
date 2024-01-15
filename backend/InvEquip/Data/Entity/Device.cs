using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Data.Entity
{
    public class Device : BaseEntity
    {
        public string DeviceName { get; set; }
        public int HwModelId { get; set; }
        public virtual HwModel HwModel { get; set; }
        public string ServiceTag { get; set; }
        public string AssetNumber { get; set; }
        public string Notes { get; set; }
        public int ContractId { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual IList<PersonDevice> PeopleDevices { get; set; }
    }
}
