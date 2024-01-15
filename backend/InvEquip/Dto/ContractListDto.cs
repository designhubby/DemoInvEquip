using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record ContractListDto
    {
        public int ContractId { get; set; }
        public string ContractName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string VendorName { get; set; }
    }
}
