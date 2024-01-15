using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record WebuserLoginDto
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
