using System.IdentityModel.Tokens.Jwt;

namespace InvEquip.Logic.Service.Helper
{
    public interface IJwtService
    {
        string Generate(int id);
        JwtSecurityToken Verify(string jwt);
    }
}