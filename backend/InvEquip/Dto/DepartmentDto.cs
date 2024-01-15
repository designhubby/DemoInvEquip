using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record DepartmentDto
    {
        public int DepartmentId { get; init; }
        public string DepartmentName { get; init; }
    }
}
