using InvEquip.ApiErrorResponse;
using InvEquip.Dto;
using InvEquip.ExceptionHandling.Exceptions;
using InvEquip.Logic.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class DepartmentController : ControllerBase
    {
        IApplicationDepartmentService _applicationDepartmentService;
        public DepartmentController(IApplicationDepartmentService applicationService)
        {
            _applicationDepartmentService = applicationService;

        }

        [HttpGet("GetAllDepartmentDtos", Name = "GetAllDepartmentDtos")]
        public async Task<ActionResult<IList<DepartmentDto>>> Get()
        {
            IList<DepartmentDto> departmentDto = await _applicationDepartmentService.ShowListOfAllDepartmentsAsync();
            return Ok(departmentDto);

        }

        [HttpGet("GetDepartmentDtoById/{id}", Name = "GetDepartmentDtoById")]
        public async Task<ActionResult<DepartmentDto>> GetDepartmentDtoById(int id)
        {
            try
            {
                DepartmentDto result = await _applicationDepartmentService.GetDepartmentDtoById(id);
                if(result is not null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpGet("GetDeptRoles/{id}", Name ="GetDeptRoles")]
        public async Task<ActionResult<IList<RoleDto>>> GetDepartmentsAssociatedRolesByDepartmentId(int id)
        {
            IList<RoleDto> roleDto = await _applicationDepartmentService.ShowListOfAllRolesByDepartmentId(id);
            return Ok(roleDto);
        }
        [HttpPost ("PostDepartmentDto", Name = "PostDepartmentDto")]
        public async Task<ActionResult> PostDepartmentDto(DepartmentDto departmentDto)
        {
            try
            {
                DepartmentDto result = await _applicationDepartmentService.PostUpdateDepartmentDto(departmentDto);
                string actionName = nameof(GetDepartmentDtoById);
                if (result is not null)
                {
                    var routeValue = new
                    {
                        id = result.DepartmentId,
                    };

                    return CreatedAtAction(actionName, routeValue, result);

                }
                else
                {
                    throw new BadHttpRequestException("Invalid Request");
                }
            }
            catch(ExceptionEntityNotExists ex)
            {
                return BadRequest();
            }

        }
        [HttpPut("PutDepartmentDto", Name = "PutDepartmentDto")]
        public async Task<ActionResult> PutDepartmentDto(DepartmentDto departmentDto)
        {
            if(departmentDto.DepartmentId < 1)
            {
                return ApiResponse.BadRequest("Malformed Dto, missing ID");
            }
            try
            {
                DepartmentDto result = await _applicationDepartmentService.PostUpdateDepartmentDto(departmentDto);
                string actionName = nameof(GetDepartmentDtoById);
                if (result is not null)
                {
                    var routeValue = new
                    {
                        id = result.DepartmentId,
                    };

                    return CreatedAtAction(actionName, routeValue, result);

                }
                else
                {
                    throw new BadHttpRequestException("Invalid Request");
                }
            }
            catch (ExceptionEntityNotExists ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult> DeleteDepartmentById(int id)
        {

            ResultStatus success = await _applicationDepartmentService.DeleteDepartmentById(id);
            if (success.Equals(ResultStatus.Success))
            {
                return Ok();
            }
            else if(success.Equals(ResultStatus.NotAllowed))
            {
                var result = ApiResponse.StatusCode424;
                var resultc = new StatusCodeResult(424);
                return result;
            }
            else
            {
                return ApiResponse.StatusCode404();
            }

        }
        [HttpDelete("ErrorTest/{id}", Name ="ErrorTest")]
        public async Task<ActionResult> ErrorTest(int id)
        {
            return id switch
            {
                0 => Ok(),
                1 => StatusCode(405),//not standardized Problem Detail scheme
                2 => StatusCode(404, "not Found"), //gives string only, not standardized Problem Detail scheme
                3 => StatusCode(403, _applicationDepartmentService.TestError(id)),
                4=>BadRequest(),
                5=> Problem(
                                "Product is already Available For Sale.",
                                $"/sales/products/{id}/availableForSale",
                                400,
                                "Cannot set product as available.",
                                "http://example.com/problems/already-available"
                           ),
                6=>StatusCode(424, new ProblemDetails
                           {
                               Status = StatusCodes.Status403Forbidden,
                               Type = "https://example.com/probs/out-of-credit",
                               Title = "Division by zero...",
                               Detail = "Details",
                               Instance = HttpContext.Request.Path
                           }),
                7=> StatusCode(427, _applicationDepartmentService.TestError(id)),
                _ => StatusCode(500),
            };

        }


    }
}
