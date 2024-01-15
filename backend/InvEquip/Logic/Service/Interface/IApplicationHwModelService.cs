using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Dto;

namespace InvEquip.Logic.Service
{
    public interface IApplicationHwModelService
    {
        Task<IEnumerable<HwModelDataDto>> GetAllHwModelsDataDtoAsync();
        Task<IEnumerable<HwModelListDto>> GetAllHwModelsListDtoAsync();
        Task<HwModelListDto> GetHwModelListDtoByIdAsync(int id);
        Task<HwModelDataDto> GetHwModelDataDtoByIdAsync(int id);
        Task<ResultStatus> DeleteHwModelById(int id);
        Task<HwModelDataDto> PostHwModel(HwModelDataDto hwModelDataDto);
        Task<ResultStatus> UpdateHwModelByDto(HwModelDataDto hwModelDataDto);
    }
}
