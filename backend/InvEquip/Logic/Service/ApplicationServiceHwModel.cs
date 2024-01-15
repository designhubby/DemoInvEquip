using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Dto;

namespace InvEquip.Logic.Service
{
    public partial class ApplicationService
    {
        public async Task<IEnumerable<HwModelDataDto>> GetAllHwModelsDataDtoAsync()
        {
            var allHwModelDtos = await _hwModelService.GetAllHwModelDataDtosAsync();
            return allHwModelDtos;
        }
        public async Task<IEnumerable<HwModelListDto>> GetAllHwModelsListDtoAsync()
        {
            IEnumerable<HwModelListDto> allHwModelDtos = await _hwModelService.GetAllHwModelListDtosAsync();
            return allHwModelDtos;
        }
        public async Task<HwModelListDto> GetHwModelListDtoByIdAsync(int id)
        {
            HwModelListDto hwModelListDto = await _hwModelService.GetHwModelDtoByIdAsync(id);
            return hwModelListDto;
        }
        public async Task<HwModelDataDto> GetHwModelDataDtoByIdAsync(int id)
        {
            HwModelDataDto hwModelDataDto = await _hwModelService.GetHwModelDataDtoByIdAsync(id);
            return hwModelDataDto;
        }
        public async Task<HwModelDataDto> PostHwModel(HwModelDataDto hwModelDataDto)
        {
            HwModelDataDto hwModelDataDtoResult = await _hwModelService.CreateHwModelFromDto(hwModelDataDto);
            return hwModelDataDtoResult;
        }
        public async Task<ResultStatus> UpdateHwModelByDto(HwModelDataDto hwModelDataDto)
        {
            //Conditions: 1-> Update Success 2->Id Not Exist 3-> Internal Error
            ResultStatus result = await _hwModelService.UpdateHwModelByDto(hwModelDataDto);
            return result;
        }
        public async Task<ResultStatus> DeleteHwModelById(int id)
        {
            ResultStatus result = await _hwModelService.DeleteById(id);
            return result;

        }



    }
}
