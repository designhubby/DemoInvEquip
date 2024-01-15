using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record PersonDto
    {
        public int PersonId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int RoleId { get; set; }
    }
}
