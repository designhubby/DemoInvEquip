using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Dto;
using InvEquip.Logic.Service;
using InvEquip.ExceptionHandling.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace InvEquip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HwModelController : ControllerBase
    {
        readonly IApplicationHwModelService _ApplicationHwModelService;
        public HwModelController(IApplicationHwModelService applicationHwModelService)
        {
            _ApplicationHwModelService = applicationHwModelService;
        }

        [HttpGet("GetAllHwModels", Name = "GetAllHwModels")]
        public async Task<ActionResult> GetAllHwModels()
        {
            var allHwModels = await _ApplicationHwModelService.GetAllHwModelsDataDtoAsync();
            return Ok(allHwModels);
        }
        [HttpGet]
        public async Task<ActionResult> GetAllHwModelsList()
        {
            IEnumerable<HwModelListDto> allHwModels = await _ApplicationHwModelService.GetAllHwModelsListDtoAsync();
            return Ok(allHwModels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetHwModelById(int id)
        {
            HwModelListDto hwModelListDto = await _ApplicationHwModelService.GetHwModelListDtoByIdAsync(id);
            return Ok(hwModelListDto);

        }
        [HttpGet("GetHwModelDataById/{id}", Name = "GetHwModelDataById")]
        public async Task<ActionResult> GetHwModelDataById(int id)
        {
            HwModelDataDto hwModelDataDto = await _ApplicationHwModelService.GetHwModelDataDtoByIdAsync(id);
            return Ok(hwModelDataDto);

        }
        [HttpPost("PostHWModelByDto", Name = "PostHWModelByDto")]
        public async Task<ActionResult> PostHWModelByDto(HwModelDataDto hwModelDataDto)
        {
            
            string actionName = nameof(GetHwModelById);

            HwModelDataDto hwModelDataDtoResult = await _ApplicationHwModelService.PostHwModel(hwModelDataDto);
            if(hwModelDataDtoResult is not null)
            {
                
                var routeValue = new { id = hwModelDataDtoResult.HwModelId };
                return CreatedAtAction(actionName, routeValue, hwModelDataDtoResult);

            }
            else
            {
                return StatusCode(500, "Internal Error");
            }

        }
        [HttpPut]
        public async Task<ActionResult> UpdateHwModelByDto(HwModelDataDto hwModelDataDto)
        {
            if(hwModelDataDto.HwModelId != 0)
            {
                //if Id exists
                ResultStatus result = await _ApplicationHwModelService.UpdateHwModelByDto(hwModelDataDto);
                return result switch
                {
                    ResultStatus.Success => Ok(),
                    ResultStatus.NotFound => StatusCode(404, "Not Found"),
                    _ => StatusCode(500),
                };
                
            }
            else
            {
                return StatusCode(400, "Request Missing Id"); //malformed request
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteHwModelByIdAsync(int id)
        {
            ResultStatus result = await _ApplicationHwModelService.DeleteHwModelById(id);
            return result switch
            {
                ResultStatus.Success => Ok(),
                ResultStatus.NotAllowed => StatusCode(405, "Has Dependencies"),
                ResultStatus.NotFound => StatusCode(404, "Not Found"),
                _ => StatusCode(404, "Not Found"),
            };
        }
    }
}
