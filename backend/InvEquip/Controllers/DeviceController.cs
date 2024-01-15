using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Dto;
using InvEquip.Logic.Service;
using Microsoft.AspNetCore.Authorization;

namespace InvEquip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DeviceController : ControllerBase
    {
        IApplicationDeviceService _ApplicationDevice;

        public DeviceController(IApplicationDeviceService applicationDeviceService)
        {
            _ApplicationDevice = applicationDeviceService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetAllDevices()
        {
            var deviceDtos = await _ApplicationDevice.GetAllDeviceDtosAsync();
            return Ok(deviceDtos);
        }

        [HttpGet("GetAllDeviceData", Name ="GetDevice")]
        public async Task<ActionResult<IEnumerable<DeviceDataDto>>> GetAllDevicesData()
        {
            var deviceDataDtos = await _ApplicationDevice.GetAllDeviceDataDtosAsync();
            return Ok(deviceDataDtos);
        }

        [HttpGet("GetDeviceDataById/{id}", Name ="GetById")]
        public async Task<ActionResult> GetDataByDeviceId(int id)
        {
            var deviceData = await _ApplicationDevice.GetDeviceDataDtoByIdAsync(id);
            return Ok(deviceData);
        }
        [HttpPut("PutDeviceData", Name = "PutDeviceData")]
        public async Task PutDeviceData(DeviceDataPostDto deviceDataPostDto)
        {
            await _ApplicationDevice.PutDeviceDataDto(deviceDataPostDto);
        }
        [HttpPost("PostDeviceData", Name ="PostDeviceData")]
        public async Task<ActionResult> PostDeviceData(DeviceDataPostDto deviceDataPostDto)
        {
            DeviceDataPostDto result = await _ApplicationDevice.PostDeviceDataDto(deviceDataPostDto);
            
            if (result is not null)
            {
                int resultId = result.DeviceId;
                var actionName = nameof(GetDataByDeviceId);
                var routeValues = new { id = resultId };
                return CreatedAtAction(actionName, routeValues, result);
            }
            else
            {
                return Problem();
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDeviceDataById(int id)
        {
            ResultStatus result = await _ApplicationDevice.DeleteDeviceDataByIdAsync(id);
            if (result == ResultStatus.NotAllowed)
            {
                return ApiErrorResponse.ApiResponse.StatusCode500("Dependencies Exist");
            }
            else if (result == ResultStatus.NotFound)
            {
                return ApiErrorResponse.ApiResponse.StatusCode404("Device Not Found");
            }
            else
            {
                return Ok();
            }
        }
    }
}
