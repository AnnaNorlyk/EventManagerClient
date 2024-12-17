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
        public string UserId { get; set; }

        [JsonProperty("firstName")]
        public string? Firstname { get; set; }
        [JsonProperty("lastName")]
        public string? Lastname { get; set; }

        [JsonProperty("userEmail")]
        public string? UserEmail { get; set; }

        [JsonProperty("userPassword")]
        public string? UserPassword { get; set; }

        [JsonProperty("userRole")]
        public string? Role { get; set; }

        [JsonProperty("eventCount")]
        public int EventCount { get; set; }
    }

}

