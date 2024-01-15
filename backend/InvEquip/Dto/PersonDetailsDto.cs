using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record PersonDetailsDto
    {
        public int PersonId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
    }
}
