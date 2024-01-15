using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.EntityFrameworkCore;
using InvEquip.Data;
using InvEquip.Data.Entity;
using InvEquip.Data.Repository;
using InvEquip.Logic.DomainModel;
using InvEquip.Dto;
using InvEquip.Logic.Service.Extension;

namespace InvEquip.Logic.Service
{
    public partial class ApplicationService
    {
        public async Task<IList<PersonDto>> GetAllPersonsDto()
        {
            IList<PersonDto> personDtoList = new List<PersonDto>();
            IList<PersonModel> personModels = await _personService.GetAllPersonModels();
            personDtoList = personModels.ToPersonDto(_mapper);

            return personDtoList;
        }

        public async Task<IEnumerable<PersonDetailsDto>> GetAllPersonsDetailsDtosAsync()
        {
            IEnumerable<PersonDetailsDto> result = await _personService.GetAllPersonDetailsAsync();
            return result;
        }
        public async Task<PersonDto> GetPersonByIdAsync(int id)
        {
            PersonModel personModel = await _personService.GetPersonByIdAsync(id);
            PersonDto personDto = _mapper.Map<PersonModel, PersonDto>(personModel);
            return personDto;
        }

        public async Task<IList<DeviceDto>> ShowPersonsAssignDevices(int personId)
        {
            return await _personService.GetPersonsAssociatedDevicesById(personId);

        }


        public async Task<PersonDto> CreateNewPersonAsync(PersonDto personDto)
        {
            PersonModel newPersonModel = await _personService.CreateNewPersonAsync(personDto);
            PersonDto newPersonDto = _mapper.Map<PersonModel, PersonDto>(newPersonModel);
            return newPersonDto;
        }

        public async Task<PersonDto> GetPersonDtoByFirstLastNameAsync(string firstname, string lastname)
        {
            var retrievedPersonModel = await _personService.GetPersonModelByFirstLastNameAsync(firstname, lastname);
            var personDto = retrievedPersonModel != null ? _mapper.Map<PersonModel, PersonDto>(retrievedPersonModel): null;
            return personDto;
        }


        public async Task<PersonDetailsDto> GetPersonDetailsDtoByIdAsync(int id)
        {
            PersonDetailsDto personDetailsDto = _mapper.Map<PersonModel, PersonDetailsDto>(await _personService.GetPersonByIdAsync(id));
            return personDetailsDto;
        }
        public async Task UpdatePersonByDto(PersonDto value)
        {
            await _personService.UpdatePersonModelByDto(value);
        }
        public async Task<ResultStatus> DeletePersonAsyncById(int id)
        {
            return await _personService.DeletePersonAsyncById(id);
        }
    }
}
