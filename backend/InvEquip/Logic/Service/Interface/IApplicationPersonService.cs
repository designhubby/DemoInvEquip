using InvEquip.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service
{
    public interface IApplicationPersonService
    {
        Task<PersonDto> CreateNewPersonAsync(PersonDto personDto);
        Task<PersonDto> GetPersonDtoByFirstLastNameAsync(string firstname, string lastname);
        Task<IList<PersonDto>> GetAllPersonsDto();
        Task<PersonDto> GetPersonByIdAsync(int id);
        Task<IList<DeviceDto>> ShowPersonsAssignDevices(int personId);
        Task UpdatePersonByDto(PersonDto value);
        Task<PersonDetailsDto> GetPersonDetailsDtoByIdAsync(int id);
        Task<ResultStatus> DeletePersonAsyncById(int id);
        Task<IEnumerable<PersonDetailsDto>> GetAllPersonsDetailsDtosAsync();
    }
}