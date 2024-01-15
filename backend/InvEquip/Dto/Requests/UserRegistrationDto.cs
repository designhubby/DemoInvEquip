using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto.Requests
{
    public record UserRegistrationDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string InviteCode { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
