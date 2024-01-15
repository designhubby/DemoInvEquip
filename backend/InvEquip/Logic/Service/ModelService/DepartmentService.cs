using AutoMapper;
using InvEquip.Data.Repository;
using InvEquip.Dto;
using InvEquip.ExceptionHandling.Exceptions;
using InvEquip.Logic.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service
{
    public class DepartmentService : ServiceBase
    {

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<DepartmentModel>> GetListAllDepartmentModelsAsync()
        {
            var departmentModels = await _unitofwork.DepartmentRepository.GetAllDomainModelsAsync();
            return departmentModels;
        }

        public async Task<IEnumerable<RoleModel>> GetListDepartmentsAssociatedRolesByDepartmentId(int departmentId)
        {
            var department = await _unitofwork.DepartmentRepository.GetDomainModelByIdAsync(departmentId);
            return department.Roles;
        }

        public async Task<DepartmentDto> GetDepartmentDataDtosByDepartmentIdAsync(int id)
        {
            DepartmentModel departmentModel = await _unitofwork.DepartmentRepository.GetDomainModelByIdAsync(id);
            DepartmentDto departmentDto = _mapper.Map<DepartmentModel, DepartmentDto>(departmentModel);
            return departmentDto;
        }

        public async Task<DepartmentDto> PostUpdateDepartmentDto(DepartmentDto departmentDto)
        {
            DepartmentModel departmentModel = new DepartmentModel(departmentDto.DepartmentId, departmentDto.DepartmentName);
            if (departmentDto.DepartmentId > 0) //Update Branch
            {
                bool exists = await _unitofwork.DepartmentRepository.CheckEntityExists(departmentDto.DepartmentId);
                if (exists)
                {
                    await _unitofwork.DepartmentRepository.UpdateDomainAsync(departmentModel);
                }
                else
                {
                    throw new ExceptionEntityNotExists(departmentDto.DepartmentId.ToString());
                }

            }
            else
            {//Post Branch
                departmentModel = await _unitofwork.DepartmentRepository.AddDomainAsync(departmentModel);
            }

            DepartmentDto result = _mapper.Map<DepartmentModel, DepartmentDto>(departmentModel);
            return result;
        }

        public async Task<ResultStatus> DeleteDepartmentById(int id)
        {
            //implement check to see if anyone still belongs to this department
            
            DepartmentModel departmentModel = await _unitofwork.DepartmentRepository.GetDomainModelByIdAsync(id);
            if(departmentModel is not null)
            {
                bool entityExists = departmentModel.HasDependencies();
                
                if (entityExists)
                {
                    return ResultStatus.NotAllowed;
                }
                else
                {
                    await _unitofwork.DepartmentRepository.DeleteEntityAsyncById(id);
                    return ResultStatus.Success;
                }
            }
            else
            {
                return ResultStatus.NotFound;
            }
            
        }
    }
}
