using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvEquip.Configuration
{
    public class ConnectionStrings
    {
        public const string ConnectionStringName = "ConnectionStrings";
        public string AzureSQL { get; set; }
        public string AzureSQLDev { get; set; }
        public string DataAPI { get; set; }

    }
}
