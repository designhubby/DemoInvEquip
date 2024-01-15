using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.EntityFrameworkCore;
using InvEquip.Data;
using InvEquip.Data.Entity;
using InvEquip.Data.Repository;
using InvEquip.Logic.DomainModel;
using InvEquip.Dto;
using InvEquip.Logic.Service.Extension;
using InvEquip.ExceptionHandling.Exceptions;
using Microsoft.AspNetCore.Http;

namespace InvEquip.Logic.Service
{
    public partial class ApplicationService
    {
        public async Task<IList<DepartmentDto>> ShowListOfAllDepartmentsAsync()
        {
            var ListOfDepartment = await _departmentService.GetListAllDepartmentModelsAsync();

            var ListOfDepartmentDtos = new List<DepartmentDto>();
            foreach (DepartmentModel departmentModel in ListOfDepartment)
            {
                DepartmentDto departmentDto = _mapper.Map<DepartmentModel, DepartmentDto>(departmentModel);
                ListOfDepartmentDtos.Add(departmentDto);
            }
            return ListOfDepartmentDtos;


        }

        public async Task<DepartmentDto> GetDepartmentDtoById(int id)
        {
            DepartmentDto result = await _departmentService.GetDepartmentDataDtosByDepartmentIdAsync(id);
            return result;
        }
        public async Task<IList<RoleDto>> ShowListOfAllRolesByDepartmentId(int departmentId)
        {
            var ListOfRoleModels = await _departmentService.GetListDepartmentsAssociatedRolesByDepartmentId(departmentId);
            IList<RoleDto> ListRoleDtos = new List<RoleDto>();
            foreach (RoleModel roleModel in ListOfRoleModels)
            {
                var roleDto = _mapper.Map<RoleModel, RoleDto>(roleModel);
                ListRoleDtos.Add(roleDto);
            }
            return ListRoleDtos;
        }
        public async Task<DepartmentDto> PostUpdateDepartmentDto(DepartmentDto departmentDto)
        {
            DepartmentDto result = await _departmentService.PostUpdateDepartmentDto(departmentDto);
            return result;
        }
        public async Task<ResultStatus> DeleteDepartmentById(int id)
        {
            return await _departmentService.DeleteDepartmentById(id);
        }
        public bool TestError(int id)
        {
            bool result = id switch
            {
                1 => throw new ExceptionEntityNotExists("This is a TestError"),
                2 => throw new ExceptionEntityHasDependency("This has dependency"),
                3 => throw new BadHttpRequestException("Bad http Req"),
                7=> throw new ExceptionEntityHasDependency("This has dependency"),
                _ => false,
            };
            
            return false;
        }
    }
}
