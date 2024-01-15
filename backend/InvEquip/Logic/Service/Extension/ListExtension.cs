using InvEquip.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvEquip.Data.Entity;

namespace InvEquip.Logic.Service.Extension
{
    public static class ListExtension
    {
        public static IList<PersonDto> ToPersonDto<PersonModel>(this IList<PersonModel> people, IMapper mapper)
        {
            var personDtoList = new List<PersonDto>();
            foreach(PersonModel person in people)
            {
                var personDto = mapper.Map<PersonModel, PersonDto>(person);
                personDtoList.Add(personDto);
            }
            return personDtoList;
        }
    }
}
