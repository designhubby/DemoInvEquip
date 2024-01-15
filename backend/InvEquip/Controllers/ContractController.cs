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
    public class ContractController : ControllerBase
    {
        IApplicationContractService _applicationContractService;

        public ContractController(IApplicationContractService applicationContractService)
        {
            _applicationContractService = applicationContractService;
        }

        [HttpGet("GetAllContracts", Name = "GetAllContracts")]
        public async Task<ActionResult> GetAllContracts()
        {
            var allContracts = await _applicationContractService.GetAllContractDataDtos();
            return Ok(allContracts);
        }
        [HttpGet("GetAllContractsListDto", Name = "GetAllContractsListDto")]
        public async Task<ActionResult> GetAllContractsListDto()
        {
            IEnumerable<ContractListDto> contractListDtos = await _applicationContractService.GetAllContractListDtos();
            return Ok(contractListDtos);
        }
        [HttpGet("GetAllContractDevicesByContractId/{contractId}", Name = "GetAllContractDevicesByContractId")]
        public async Task<ActionResult> GetAllContractDevicesByContractId(int contractId)
        {
            IEnumerable<DeviceDto> devicesDto = await _applicationContractService.GetAllContractDevicesByContractId(contractId);
            return Ok(devicesDto);
        }
        [HttpGet("GetContractsOwnedByVendorByVendorId", Name = "GetContractsOwnedByVendorByVendorId")]
        public async Task<ActionResult> GetContractsOwnedByVendorByVendorId(int VendorId)
        {
            IEnumerable<ContractDataDto> contractdatadtos = await _applicationContractService.GetContractsOwnedByVendorByVendorIdAsync(VendorId);
            return Ok(contractdatadtos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetContractDataDtoById(int id)
        {
            ContractDataDto contractDataDto = await _applicationContractService.GetContractDataDtoById(id);
            return Ok(contractDataDto);
        }
        [HttpPost]
        public async Task<ActionResult> CreateContractDataByDto(ContractDataDto contractDataDto)
        {
            string actionName = nameof(GetContractDataDtoById);
            ContractDataDtoNew result = await _applicationContractService.CreateContractDataByDto(contractDataDto);
            var routeValues = new { id = result.ContractId };
            return CreatedAtAction(actionName, routeValues, result);

        }
        [HttpPut]
        public async Task<ActionResult> UpdateContractDataByDto(ContractDataDto contractDataDto)
        {
            if(contractDataDto.ContractId != 0)
            {
                ResultStatus result = await _applicationContractService.UpdateContractDataByDto(contractDataDto);
                return result switch
                {
                    ResultStatus.NotFound => ApiErrorResponse.ApiResponse.BadRequest("Entity Id Not Exist"),
                    ResultStatus.Success => Ok(),
                    _ => ApiErrorResponse.ApiResponse.BadRequest(),
                };
            }
            else
            {
                return ApiErrorResponse.ApiResponse.BadRequest("MalFormed Request: Missing Id");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContractById(int id)
        {
            ResultStatus result = await _applicationContractService.DeleteContractByIdAsync(id);
            return result switch
            {
                ResultStatus.Success => Ok(),
                ResultStatus.NotFound => ApiErrorResponse.ApiResponse.StatusCode404(),
                ResultStatus.NotAllowed => ApiErrorResponse.ApiResponse.StatusCode424,
                _ => ApiErrorResponse.ApiResponse.Response(500),
            };
        }




    }
}
