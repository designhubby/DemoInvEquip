using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Data.Entity;
using InvEquip.Dto;
using InvEquip.Logic.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace InvEquip.Data.Repository
{
    public class PersonRepository : BaseRepository<Person>
    {
        public PersonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<PersonModel> GetModelByIdAsync(int id)
        {
            var entity = await GetEntityByIdAsync(id);
            return (PersonModel)Activator.CreateInstance(typeof(PersonModel), entity);
        }

        public async Task<PersonModel> AddPersonAsync(PersonModel personModel)
        {
            personModel._entity = await AddEntityAsync(personModel._entity);
            return personModel;

        }

        public async Task UpdatePersonAsync(PersonModel personModel)
        {
             await UpdateEntityAsync(personModel._entity);
        }

        internal async Task<PersonModel> GetPersonModelByFirstLastNameAsync(string firstname, string lastname)
        {
            Person retrievedPersonEntity = await _dbSet.FirstOrDefaultAsync(person => person.Fname == firstname && person.Lname == lastname);
            PersonModel returnedPersonModel = retrievedPersonEntity != null ? (PersonModel)Activator.CreateInstance(typeof(PersonModel), retrievedPersonEntity) : null;
            return returnedPersonModel;
        }
        internal async Task<IList<PersonModel>> GetAllPersonModels()
        {
            var entities = await GetAllEntitiesAsync();

            var ListOfPersonModels = new List<PersonModel>();
            foreach(Person person in entities)
            {
                var PersonModel = (PersonModel)Activator.CreateInstance(typeof(PersonModel), person);
                ListOfPersonModels.Add(PersonModel);
            }
            return ListOfPersonModels;
        }
        public  async Task<ResultStatus> DeletePersonAsync(int id)
        {
            PersonModel personModel = await GetModelByIdAsync(id);
            //add code for person entity doesn't exist
            bool hasAssociations = personModel._entity.PersonDevice.Any(indiv => indiv.EndDate is null || indiv.EndDate > DateTime.Now);
            if (hasAssociations)
            {
                return ResultStatus.NotAllowed;
            }
            else
            {
                await DeleteEntityAsyncById(id);
                return ResultStatus.Success;
            }

        }
    }
}
