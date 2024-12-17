using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerClient.AppLayer.DTOs
{
    public class UserDto
    {

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("userName")]
        public string? UserName { get; set; }
        [JsonProperty("firstName")]
        public string? Firstname { get; set; }
        [JsonProperty("lastName")]
        public string? Lastname { get; set; }

        [JsonProperty("userEmail")]
        public string? UserEmail { get; set; }

        [JsonProperty("userPassword")]
        public string? UserPassword { get; set; }

        [JsonProperty("eventCount")]
        public int EventCount { get; set; }
    }

}

