using InvEquip.Data.Entity;
using InvEquip.Dto;
using InvEquip.Logic.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service
{
    public partial class ApplicationService
    {
        public async Task<WebuserDto> GetUserByEmail(string email)
        {
            return await _authService.GetUserByEmail(email);
        }
        public async Task<WebuserDto> CreateUser(WebuserDto webuserDto)
        {
            WebuserDto result = await _authService.CreateUserByDto(webuserDto);
            return result;
        }
        public async Task<ResultStatus> CheckUserAuth(WebuserLoginDto webuserLoginDto)
        {
            return await _authService.CheckUserAuth(webuserLoginDto);
        }
        public string GenerateJwt(int id, IJwtService jwtService)
        {
            return _authService.GenerateJwt(id, jwtService);
        }
        public WebuserDto GetUserByJwtCookie(string jwt, IJwtService jwtService)
        {
            return _authService.GetUserByJwt(jwt, jwtService);
        }
    }
}
