using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Data.Entity
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public virtual IList<Person> People { get; set; }
    }
}
