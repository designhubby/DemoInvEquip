using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InvEquip.Logic.Service.Helper
{
    public static class UserHelpers
    {
        public static string GetId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier) ?? principal.FindFirst(c => c.Type == "sub");
            if(userIdClaim !=null && !string.IsNullOrEmpty(userIdClaim.Value))
            {
                return userIdClaim.Value;
            }
            return null;
        }
        public static string GetIdentityId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(c => c.Type == "id");
            if (userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value))
            {
                return userIdClaim.Value;
            }
            return null;
        }
    }
}
