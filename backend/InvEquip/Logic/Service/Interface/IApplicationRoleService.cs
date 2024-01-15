using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Dto;

namespace InvEquip.Logic.Service
{
    public interface IApplicationRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllRoleDtos();
        Task<RoleDto> GetRoleByRoleIdAsync(int roleId);
        Task<IEnumerable<RoleDepartmentDto>> GetAllRoleDepartmentsAll();
        Task DeleteRoleById(int id);
        Task<RoleDepartmentDto> GetRoleDepartmentById(int id);
        Task<RoleDepartmentDto> UpdateRoleDepartmentByDto(RoleDepartmentDto roleDepartmentDto);
    }
}
