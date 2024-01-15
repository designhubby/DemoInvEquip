using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Dto;
using InvEquip.Logic.Service;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Authorization;

namespace InvEquip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VendorController : ControllerBase
    {
        IApplicationVendorService _applicationVendorService;
        public VendorController(IApplicationVendorService applicationVendorService)
        {
            _applicationVendorService = applicationVendorService;
        }
        [HttpGet ("GetVendorDataDtoByVendorId/{id}", Name = "GetVendorDataDtoByVendorId")]
        public async Task<ActionResult> GetVendorDataDtoByVendorId(int id)
        {
            VendorDataDto vendorDataDto = await _applicationVendorService.GetVendorDataDtoByVendorId(id);
            if(vendorDataDto is not null)
            {
                return Ok(vendorDataDto);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAllVendorData", Name = "GetAllVendorData")]
        public async Task<ActionResult> GetAllVendors()
        {
            var allVendors = await _applicationVendorService.GetAllVendorDataDtos();
            return Ok(allVendors);
        }
        [HttpGet("GetContractDataDtosOwnByVendorByVendorId/{vendorId}", Name = "GetContractDataDtosOwnByVendorByVendorId")]
        public async Task<ActionResult> GetContractDataDtosOwnByVendorByVendorId(int vendorId)
        {
            var contractDataDtos = await _applicationVendorService.GetContractDataDtosOwnByVendorByVendorId(vendorId);
            return Ok(contractDataDtos);
        }

        [HttpGet("GetContractDataDtosSibblingOwnByVendorByContractId/{contractId}", Name = "GetContractDataDtosSibblingOwnByVendorByContractId")]
        public async Task<ActionResult<IEnumerable<ContractDataDto>>> GetContractDataDtosSibblingOwnByVendorByContractId(int contractId)
        {
            var contractDataDtos = await _applicationVendorService.GetContractDataDtosOwnByVendorByContractId(contractId);
            return Ok(contractDataDtos);
        }
        [HttpPost("PostVendorDataByDto", Name = "PostVendorDataByDto")]
        public async Task<ActionResult> PostVendorDataByDto(VendorDataDto vendorDataDto)
        {
            int vendorId;
            string actionName = nameof(GetVendorDataDtoByVendorId);
            
            VendorDataDto _resultVendorDataDto = await _applicationVendorService.CreateVendorDataByDtoAsync(vendorDataDto);
            if (_resultVendorDataDto is not null) {
                vendorId = _resultVendorDataDto.VendorId;
                var routeValues = new { id = vendorId };
                return CreatedAtAction(actionName, routeValues, _resultVendorDataDto);
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("PatchVendorEntity/{id:int}", Name = "PatchVendorEntity")]
        public async Task<ActionResult> PatchVendorEntity (int id, [FromBody] VendorDataDto vendorDataDto)
        {
            VendorDataDto _vendorDataDto = await _applicationVendorService.GetVendorDataDtoByVendorId(id);
            if(_vendorDataDto is null)
            {
                return NotFound();
            }
            bool result = await _applicationVendorService.UpdateVendorDataByDtoAsync(vendorDataDto);
            if (result)
            {
                return Ok();
            }
            else { 
                return StatusCode(500);
            }


        }

    }
}
