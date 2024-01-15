using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Data.Entity
{
    public class Webuser: BaseEntity
    {
        public string Email { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Password { get; set; }
    }
}
