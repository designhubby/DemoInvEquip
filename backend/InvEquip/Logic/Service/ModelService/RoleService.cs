using AutoMapper;
using InvEquip.Data.Repository;
using InvEquip.Logic.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Dto;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace InvEquip.Logic.Service
{
    public class RoleService : ServiceBase
    {

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper): base(unitOfWork, mapper)
        {
            _unitofwork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RoleDto>> GetAllRoleDtosAsync()
        {
            IEnumerable<RoleModel> roleModels = await _unitofwork.RoleRepository.GetAllDomainModelsAsync();
            IEnumerable<RoleDto> roleDtos = roleModels.Select((indiv) => _mapper.Map<RoleModel, RoleDto>(indiv));
            return roleDtos;
        }

        public async Task<RoleDto> GetRoleByRoleIdAsync(int roleId)
        {
            RoleModel roleModel = await _unitofwork.RoleRepository.GetDomainModelByIdAsync(roleId);
            if (roleModel == null)
            {
                throw new BadHttpRequestException("No record exists");
            }
            RoleDto roleDto = _mapper.Map<RoleModel, RoleDto>(roleModel);
            return roleDto;
        }

        public async Task<IEnumerable<RoleDepartmentDto>> GetAllRoleDepartmentsAll()
        {
            IEnumerable<RoleModel> roleModel = await _unitofwork.RoleRepository.GetAllDomainModelsAsync();
            IEnumerable<RoleDepartmentDto> roleDepartmentDtos = roleModel.Select(indiv => _mapper.Map<RoleModel, RoleDepartmentDto>(indiv));
            return roleDepartmentDtos;
        }


        public async Task<RoleDepartmentDto> GetRoleDepartmentById(int id)
        {
            RoleModel roleModel = await _unitofwork.RoleRepository.GetDomainModelByIdAsync(id);
            if(roleModel == null)
            {
                throw new BadHttpRequestException("No record exists");
            }
            else
            {
                RoleDepartmentDto result = _mapper.Map<RoleModel, RoleDepartmentDto>(roleModel);
                return result;
            }
        }

        public async Task<RoleDepartmentDto> UpdateRoleDepartmentByDto(RoleDepartmentDto roleDepartmentDto)
        {
            RoleModel roleModel = new RoleModel(roleDepartmentDto.RoleId, roleDepartmentDto.RoleName, roleDepartmentDto.DepartmentId);

            if (roleDepartmentDto.RoleId < 1)
            {
                await _unitofwork.RoleRepository.AddDomainAsync(roleModel);
            }
            else
            {
                await _unitofwork.RoleRepository.UpdateDomainAsync(roleModel);
            }
            roleModel = await _unitofwork.RoleRepository.GetRoleModelByIdWithDepartment(roleModel.RoleId);
            RoleDepartmentDto result = _mapper.Map<RoleModel, RoleDepartmentDto>(roleModel);
            return result;
        }
        public async Task DeleteRoleById(int id)
        {
            await _unitofwork.RoleRepository.DeleteEntityAsyncById(id);
        }


    }
}
