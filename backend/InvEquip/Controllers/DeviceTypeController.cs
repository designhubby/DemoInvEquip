using InvEquip.Logic.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Dto;
using Microsoft.AspNetCore.Authorization;

namespace InvEquip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeviceTypeController : ControllerBase
    {
        IApplicationDeviceTypeService _applicationDeviceTypeService;
        public DeviceTypeController(IApplicationDeviceTypeService applicationDeviceTypeService)
        {
            _applicationDeviceTypeService = applicationDeviceTypeService;
        }
        [HttpGet("GetAllDeviceTypeDtos", Name ="GetAllDeviceTypeDto")]
        public async Task<ActionResult> GetDeviceTypeDataDto()
        {
            var allDeviceTypeDataDto = await _applicationDeviceTypeService.GetDeviceTypeDataDtos();
            return Ok(allDeviceTypeDataDto);
        }
        [HttpGet("GetDeviceTypeDtoByDeviceTypeId/{id}", Name = "GetDeviceTypeDtoByDeviceTypeId")]
        public async Task<ActionResult<DeviceTypeDataDto>> GetDeviceTypeDtoByDeviceTypeId(int id)
        {
            DeviceTypeDataDto result = await _applicationDeviceTypeService.GetDeviceTypeDataDtoById(id);
            if(result is not null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
            
        }
        [HttpGet("GetHwModelDataDtosByDeviceTypeId/{deviceTypeId}", Name = "GetHwModelDataDtosByDeviceTypeId")]
        public async Task<ActionResult<IEnumerable<HwModelDataDto>>> GetHwModelDataDtosByDeviceTypeId(int deviceTypeId)
        {
            IEnumerable<HwModelDataDto> hwModelDataDtos = await _applicationDeviceTypeService.GetHwModelsDataDtoByDeviceTypeIdAsync(deviceTypeId);
            return Ok(hwModelDataDtos);
        }
        [HttpPost("PostDeviceTypeDto", Name = "PostDeviceTypeDto")]
        public async Task<ActionResult> PostDeviceTypeDto(DeviceTypeDataDto deviceTypeDataDto)
        {
            int deviceTypeId;
            string actionName = nameof(GetDeviceTypeDtoByDeviceTypeId);

            DeviceTypeDataDto _resultDto = await _applicationDeviceTypeService.CreateDeviceTypeFromDeviceTypeDto(deviceTypeDataDto);
            if(_resultDto is not null)
            {
                deviceTypeId = _resultDto.DeviceTypeId;
                var routeValues = new { id = _resultDto.DeviceTypeId };
                return CreatedAtAction(actionName, routeValues, _resultDto);
            }
            else
            {
                return StatusCode(500);
            }

        }
        [HttpPut("UpdateDeviceTypeDto", Name = "UpdateDeviceTypeDto")]
        public async Task<ActionResult> UpdateDeviceTypeDto(DeviceTypeDataDto deviceTypeDataDto)
        {
            if (await _applicationDeviceTypeService.CheckEntityExists(deviceTypeDataDto.DeviceTypeId))
            {
                bool result = await _applicationDeviceTypeService.UpdateDeviceTypeByDto(deviceTypeDataDto);
                if (result)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDeviceType(int id)
        {
            ResultStatus result = await _applicationDeviceTypeService.DeleteById(id);
            if(result == ResultStatus.NotAllowed)
            {
                return new ObjectResult("Dependencies Exist") { StatusCode = 500 };
            }else if(result == ResultStatus.NotFound)
            {
                return new ObjectResult("Entity Not Found") { StatusCode = 404 };
            }
            else
            {
                return Ok();
            }

        }
    }
}
