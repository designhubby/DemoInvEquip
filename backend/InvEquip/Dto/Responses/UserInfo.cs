using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InvEquip.Dto.Responses
{
    public class UserInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; internal set; }
        public string Email { get; internal set; }
        [JsonPropertyName("claims")]
        public Dictionary<string, string> Claims { get; set; }
    }
}
