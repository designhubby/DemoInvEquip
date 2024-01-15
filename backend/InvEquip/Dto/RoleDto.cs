using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record RoleDto
    {
        public int RoleId { get; init; }
        public string RoleName { get; init; }
        public int DepartmentId { get; init; }
    }
}
