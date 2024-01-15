using InvEquip.Dto;
using InvEquip.Logic.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        IApplicationRoleService _applicationRoleService;

        public RoleController(IApplicationRoleService applicationRoleService)
        {
            _applicationRoleService = applicationRoleService;
        }

        [HttpGet("GetAllRoles", Name = "GetAllRoles")]
        public async Task<ActionResult> GetAllRoles()
        {
            IEnumerable<RoleDto> roleDtos = await _applicationRoleService.GetAllRoleDtos();
            return Ok(roleDtos);
        }
        [HttpGet("GetRoleById/{roleId}", Name = "GetRoleById")]
        public async Task<ActionResult> GetRoleById(int roleId)
        {
            RoleDto roleDto = await _applicationRoleService.GetRoleByRoleIdAsync(roleId);
            return Ok(roleDto);

        }
        [HttpGet("RoleDepartmentsAll", Name = "RoleDepartmentsAll")]
        public async Task<ActionResult> RoleDepartmentsAll()
        {
            IEnumerable<RoleDepartmentDto> result = await _applicationRoleService.GetAllRoleDepartmentsAll();
            return Ok(result);
        }
        [HttpGet("RoleDepartmentsById/{id}", Name = "RoleDepartmentsById")]
        public async Task<ActionResult<RoleDepartmentDto>> RoleDepartmentsById(int id)
        {
            RoleDepartmentDto result = await _applicationRoleService.GetRoleDepartmentById(id);
            return Ok(result);
        }
        [HttpPost ("CreateRoleDepartment", Name = "CreateRoleDepartment")]
        public async Task<ActionResult<RoleDepartmentDto>> CreateRoleDepartment(RoleDepartmentDto roleDepartmentDto)
        {
            string actionName = nameof(RoleDepartmentsById);
            RoleDepartmentDto result = await _applicationRoleService.UpdateRoleDepartmentByDto(roleDepartmentDto);

            var routeName = new
            {
                id = result.RoleId,
            };
            return CreatedAtAction(actionName, routeName, result);
        }
        [HttpPut ("UpdateRoleDepartmentByDto", Name = "UpdateRoleDepartmentByDto")]
        public async Task<ActionResult> UpdateRoleDepartmentByDto(RoleDepartmentDto roleDepartmentDto)
        {
            string actionName = nameof(RoleDepartmentsById);
            var routeName = new
            {
                id = roleDepartmentDto.DepartmentId,
            };

            RoleDepartmentDto result =  await _applicationRoleService.UpdateRoleDepartmentByDto(roleDepartmentDto);
            return CreatedAtAction(actionName, routeName, result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoleById(int id)
        {
            await _applicationRoleService.DeleteRoleById(id);
            return NoContent();
        }



    }
}
