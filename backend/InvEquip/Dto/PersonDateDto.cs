using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record PersonDateDto
    {
        public int PersonDeviceId { get; set; }
        public int PersonId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
