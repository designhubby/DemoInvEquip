using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Dto;

namespace InvEquip.Logic.Service
{
    public partial class ApplicationService
    {
        public async Task<IEnumerable<RoleDto>> GetAllRoleDtos()
        {
            IEnumerable<RoleDto> roleDtos = await _roleService.GetAllRoleDtosAsync();
            return roleDtos;
        }

        public async Task<RoleDto> GetRoleByRoleIdAsync(int roleId)
        {
            RoleDto roleDto = await _roleService.GetRoleByRoleIdAsync(roleId);
            return roleDto;
        }
        public async Task<IEnumerable<RoleDepartmentDto>> GetAllRoleDepartmentsAll()
        {
            IEnumerable<RoleDepartmentDto> roleDepartmentDtos = await _roleService.GetAllRoleDepartmentsAll();
            return roleDepartmentDtos;
        }
        public async Task<RoleDepartmentDto> GetRoleDepartmentById(int id)
        {
            RoleDepartmentDto result = await _roleService.GetRoleDepartmentById(id);
            return result;
        }
        public async Task<RoleDepartmentDto> UpdateRoleDepartmentByDto(RoleDepartmentDto roleDepartmentDto)
        {
            RoleDepartmentDto result = await _roleService.UpdateRoleDepartmentByDto(roleDepartmentDto);
            return result;
        }
        public async Task DeleteRoleById(int id)
        {
            await _roleService.DeleteRoleById(id);
        }
    }
}
