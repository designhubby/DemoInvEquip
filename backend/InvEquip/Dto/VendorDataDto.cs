﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Dto
{
    public record VendorDataDto
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; }
    }
}
