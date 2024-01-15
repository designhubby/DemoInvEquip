using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Data.Entity
{
    public class Department : BaseEntity
    {
        public Department() { }
        public string DepartmentName { get; set; }
        public virtual IList<Role> Roles { get; set; }
    }
}
