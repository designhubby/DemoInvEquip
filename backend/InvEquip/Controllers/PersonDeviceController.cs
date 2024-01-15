using InvEquip.Dto;
using InvEquip.Logic.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InvEquip.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize]
    public class PersonDeviceController : Controller
    {
        IApplicationPersonDeviceService _appPersonDeviceService;
        public PersonDeviceController(IApplicationPersonDeviceService appPersonDeviceService)
        {
            _appPersonDeviceService = appPersonDeviceService;
        }
        [HttpGet("GetPersonAssociatedDevices/{personId}", Name ="GetAssociatedDevices")]
        public async Task<ActionResult> GetPersonAssignedDevices(int personId)
        {
            var listOf_Persons_Assigned_DevicesWithDate = await _appPersonDeviceService.ShowPersonsAssignDeviceWithDate(personId);
            return  Ok(listOf_Persons_Assigned_DevicesWithDate);
        }
        [HttpGet("GetDevicesAssociatedPersons/{deviceId}", Name = "GetDevicesAssociatedPersons")]
        public async Task<ActionResult<IEnumerable<PersonDateDto>>> GetDevicesAssociatedPersons(int deviceId)
        {
            IEnumerable<PersonDateDto> listOf_Devices_Assigned_PersonsWithDate = await _appPersonDeviceService.ShowDevicesAssignPersonWithDate(deviceId);
            return Ok(listOf_Devices_Assigned_PersonsWithDate);
        }

        [HttpGet("ShowCurrentlyAssignedPCDevices/{personId}", Name = "ShowCurrentlyAssignedPCDevice")]
        public async Task<ActionResult> GetCurrentlyAssignedPCDevices (int personId)
        {
            var PersonsPcDevices = await _appPersonDeviceService.ShowCurrentAssignedPCDevice(personId);
            if(PersonsPcDevices == null)
            {
                return NotFound();
            }
            return Ok(PersonsPcDevices);
        }

        [HttpGet("ShowUnassignedDevicesByType/{deviceTypeId}", Name = "ShowUnassignedDevicesByType")]
        public async Task<ActionResult> GetUnassignedDevicesByType(int deviceTypeId)
        {
            var listUnassignedDevices = await _appPersonDeviceService.ShowUnassignedDevicesByType(deviceTypeId);
            return Ok(listUnassignedDevices);
        }
        [HttpGet("ShowActivelyAssociatedDevices/{personId}", Name ="ShowActivelyAssociatedDevices")]
        public async Task<ActionResult> GetActivelyAssociatedDevices(int personId)
        {
            var listActivelyAssociatedDevices = await _appPersonDeviceService.Show_Person_Actively_Associated_Devices(personId);
            return Ok(listActivelyAssociatedDevices);
        }
        [HttpPatch("UnassociatePersonFromDeviceNow/{personDeviceId}")]
        public async Task<ActionResult> UnassociatePersonFromDevice(int personDeviceId)
        {

            bool success = await _appPersonDeviceService.UnAssociateDevice_Now_By_PersonDeviceId(personDeviceId);
            if(success)
            return Ok();

            return BadRequest();

            
        }
        [HttpPatch("UnassociatePersonFromDeviceByList", Name = "UnassociatePersonFromDeviceByList")]
        public async Task<ActionResult> UnassociatePersonFromDeviceByList(int[] personDeviceId)
        {

            bool success = await _appPersonDeviceService.UnAssociateDevice_Now_By_PersonDeviceIdList(personDeviceId);
            if (success)
                return Ok();

            return BadRequest();


        }
        [HttpGet("GetDeviceByFilteredQuery", Name = "GetDeviceByFilteredQuery")]
        public async Task<ActionResult<IEnumerable<DeviceDto>>> GetDeviceByFilteredQuery(int? deviceTypeId, bool retired, bool activeRental, int? contractId, int? hwModelId)
        {
            IEnumerable<DeviceDto> deviceDtos = await _appPersonDeviceService.ShowDevicesByQueryObject(deviceTypeId, retired, activeRental, contractId, hwModelId);
            return Ok(deviceDtos);
        }

        [HttpPost("AssociatePersonDeviceByPdDto", Name = "AssociatePersonDeviceByPdDto")]
        public async Task<ActionResult> AssociatePersonDeviceByPdDto(IEnumerable<PersonDeviceListDto> personDeviceListDtos)
        {
            bool result = await  _appPersonDeviceService.AttemptAssignPersonToDevice(personDeviceListDtos);
            if (result)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
            
        }
    }
}
