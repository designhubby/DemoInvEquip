using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Data.Entity
{
    public class Person : BaseEntity
    {
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual IList<PersonDevice>? PersonDevice { get; set; }
    }
}
