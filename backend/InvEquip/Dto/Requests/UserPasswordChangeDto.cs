using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto.Requests
{
    public record UserPasswordChangeDto
    {
        [Required]
        public string CurrentPwd { get; set; }
        [Required]
        public string NewPwd { get; set; }
    }
}
