using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvEquip.Dto;
using InvEquip.Logic.Service.Helper;

namespace InvEquip.Logic.Service
{
    public interface IApplicationAuthService
    {

        Task<WebuserDto> GetUserByEmail(string email);
        Task<ResultStatus> CheckUserAuth(WebuserLoginDto webuserLoginDto);

        string GenerateJwt(int id, IJwtService jwtService);
        Task<WebuserDto> CreateUser(WebuserDto webuserDto);
        WebuserDto GetUserByJwtCookie(string jwt, IJwtService jwtService);
    }
}
