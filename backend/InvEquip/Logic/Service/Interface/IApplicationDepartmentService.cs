using InvEquip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service
{
    public interface IApplicationDepartmentService
    {
        Task<IList<DepartmentDto>> ShowListOfAllDepartmentsAsync();
        Task<IList<RoleDto>> ShowListOfAllRolesByDepartmentId(int departmentId);
        Task<DepartmentDto> GetDepartmentDtoById(int id);
        Task<DepartmentDto> PostUpdateDepartmentDto(DepartmentDto departmentDto);
        Task<ResultStatus> DeleteDepartmentById(int id);
        bool TestError(int id);
    }
}
