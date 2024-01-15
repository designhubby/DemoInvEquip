using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InvEquip.Data.Entity;
using InvEquip.Data.Repository;
using InvEquip.Dto;
using InvEquip.Logic.DomainModel;
using InvEquip.Logic.Service.Helper;

namespace InvEquip.Logic.Service
{
    public class AuthService : ServiceBase
    {
        public IJwtService JwtService { get; set; }

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            
        }
        
        public async Task<WebuserDto> GetUserByEmail(string email)
        {
            IEnumerable<Webuser> webusers = await _unitofwork.WebUserRepository.GetAllEntitiesWhereAsync(wu => wu.Email == email);
            Webuser webuser = webusers.FirstOrDefault();
            return webuser is not null ? _mapper.Map<Webuser, WebuserDto>(webuser) : null;
        }
        public async Task<ResultStatus> CheckUserAuth(WebuserLoginDto webuserLoginDto)
        {
            WebuserDto webuserDto = await GetUserByEmail(webuserLoginDto.email);
            if (webuserDto is not null)
            {
                return BCrypt.Net.BCrypt.Verify(webuserLoginDto.password, webuserDto.Password) ? ResultStatus.Success : ResultStatus.NotAllowed;
            }
            else
            {
                return ResultStatus.NotFound;
            }
        }

        internal string GenerateJwt(int id, IJwtService jwtService)
        {
            JwtService = jwtService;
            return JwtService.Generate(id);
        }

        public async Task<WebuserDto> CreateUserByDto(WebuserDto webuserDto)
        {

            Webuser webuser = await _unitofwork.WebUserRepository.AddEntityAsync(new Webuser()
            {
                Email = webuserDto.Email,
                Fname = webuserDto.Fname,
                Lname = webuserDto.Lname,
                Password = webuserDto.Password,
            }
            );
            WebuserDto result = new WebuserDto()
            {
                Email = webuser.Email,
                Fname = webuser.Fname,
                Lname = webuser.Lname,

            };
            return result;
        }
        public WebuserDto GetUserByJwt(string jwtSecurityToken, IJwtService jwtService)
        {
            JwtService = jwtService;
            JwtSecurityToken token = JwtService.Verify(jwtSecurityToken);
            int userId = int.Parse(token.Issuer);
            Webuser webuser = _unitofwork.WebUserRepository.GetEntityById(userId);
            WebuserDto webuserDto = _mapper.Map<Webuser, WebuserDto>(webuser);
            return webuserDto;
        }

    }
}
