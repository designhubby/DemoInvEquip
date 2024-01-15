using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record RoleDepartmentDto : RoleDto
    {
        public string DepartmentName { get; init; }
    }
}
