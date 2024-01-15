using InvEquip.Data.Repository;
using InvEquip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvEquip.Logic.DomainModel;
using InvEquip.Data.Entity;

namespace InvEquip.Logic.Service
{
    public class PersonService : ServiceBase
    {
        public PersonService(IUnitOfWork unitOfWork, IMapper mapper) : base (unitOfWork, mapper)
        {
        }
        public async Task<IList<PersonModel>> GetAllPersonModels()
        {
            var PersonModels = await _unitofwork.PersonRepository.GetAllPersonModels();
            return PersonModels;

        }
        public async Task<IList<DeviceDto>> GetPersonsAssociatedDevicesById(int personId)
        {
            //For given person, Shows all associated unique devices
            var listOfDevicesDto = new List<DeviceDto>();
            var PersonModel = await _unitofwork.PersonRepository.GetModelByIdAsync(personId);
            //Get list of Devices associated with personid
            var devicesOfPerson = PersonModel.Devices;
            foreach(Device device in devicesOfPerson)
            {
                var newPersonDto = _mapper.Map<Device, DeviceDto>(device);
                listOfDevicesDto.Add(newPersonDto);
            }
            
            return listOfDevicesDto;
        }

        public async Task<IEnumerable<PersonDetailsDto>> GetAllPersonDetailsAsync()
        {
            IEnumerable<PersonModel> personModels = await _unitofwork.PersonRepository.GetAllPersonModels();
            IEnumerable<PersonDetailsDto> result = await Task.Run(()=>personModels.Select(indiv => _mapper.Map<PersonModel, PersonDetailsDto>(indiv)));
            return result;
        }

        internal async Task<PersonModel>  CreateNewPersonAsync(PersonDto personDto)
        {
            PersonModel newPersonModel = new PersonModel(null, personDto.Fname, personDto.Lname, personDto.RoleId);
            newPersonModel = await _unitofwork.PersonRepository.AddPersonAsync(newPersonModel);
            return newPersonModel;
            
        }

        internal async Task<PersonModel> GetPersonModelByFirstLastNameAsync(string firstname, string lastname)
        {
            var retrievedPersonModel = await _unitofwork.PersonRepository.GetPersonModelByFirstLastNameAsync(firstname, lastname);
            return retrievedPersonModel;
        }

        internal async Task<PersonModel> GetPersonByIdAsync(int id)
        {
            PersonModel personModel = await _unitofwork.PersonRepository.GetModelByIdAsync(id);
            return personModel;
        }
        internal async Task UpdatePersonModelByDto(PersonDto personDto)
        {
            PersonModel personModel = new PersonModel(personDto.PersonId, personDto.Fname, personDto.Lname, personDto.RoleId);
            await _unitofwork.PersonRepository.UpdatePersonAsync(personModel);
        }
        public async Task<ResultStatus> DeletePersonAsyncById(int id)
        {
            return await _unitofwork.PersonRepository.DeletePersonAsync(id);
        }
    }
}
