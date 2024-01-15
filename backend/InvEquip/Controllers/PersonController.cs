using InvEquip.Dto;
using InvEquip.Logic.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvEquip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        IApplicationPersonService _ApplicationPerson;
        public PersonController(IApplicationPersonService applicationPersonService)
        {
            _ApplicationPerson = applicationPersonService;
        }
        // GET: api/<PersonController>
        [HttpGet]
        public async Task<ActionResult<IList<PersonDto>>> Get()
        {
            var listOfPeople = await _ApplicationPerson.GetAllPersonsDto();
            return  Ok(listOfPeople.ToList());
        }
        [HttpGet("PersonDetailsDto", Name = "PersonDetailsDto")]
        public async Task<ActionResult<IEnumerable<PersonDetailsDto>>> PersonDetailsDto()
        {
            IEnumerable<PersonDetailsDto> personDetailsDtos = await _ApplicationPerson.GetAllPersonsDetailsDtosAsync();
            return Ok(personDetailsDtos);
        }

        // GET api/<PersonController>/5
        [HttpGet("GetById/{id}", Name = "GetPerson")]
        public async Task<ActionResult<PersonDto>> GetById(int id)
        {
            if(id < 1)
            {
                return BadRequest("id must be greater than 1");
            }
            PersonDto personDto = await _ApplicationPerson.GetPersonByIdAsync(id);
            return Ok(personDto);
        }
        [HttpGet("GetByName")]
        // GET api/<PersonController>/GetByName/?(query string)
        public async Task<ActionResult>Get(string firstname, string lastname)
        {
            if(firstname == null || lastname == null)
            {
                return BadRequest("Miss request first, last name parameters");
            }
            var personDto = await _ApplicationPerson.GetPersonDtoByFirstLastNameAsync(firstname, lastname);
            if (personDto == null)
            {
                return NotFound();
            }
            return Ok(personDto);
        }
        [HttpGet("GetDetailsById/{id}", Name ="GetDetailsById")]
        public async Task<ActionResult> Get(int id)
        {
            PersonDetailsDto personDetailsDto = await _ApplicationPerson.GetPersonDetailsDtoByIdAsync(id);
            return Ok(personDetailsDto);
        }

        // POST api/<PersonController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PersonDto value)
        {
            var actionName = nameof(GetById);
            var personDto = await _ApplicationPerson.CreateNewPersonAsync(value);
            if(personDto == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(actionName, new { id = personDto.PersonId }, personDto);


        }

        // PUT api/<PersonController>/5
        [HttpPut]
        public async Task Put([FromBody] PersonDto value)
        {
            
           await _ApplicationPerson.UpdatePersonByDto(value);
            

        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            ResultStatus result = await _ApplicationPerson.DeletePersonAsyncById(id);
            return result switch
            {
                ResultStatus.Success => Ok(),
                ResultStatus.NotAllowed => ApiErrorResponse.ApiResponse.StatusCode424,
                _ => ApiErrorResponse.ApiResponse.BadRequest(),
            };
        }
    }
}
