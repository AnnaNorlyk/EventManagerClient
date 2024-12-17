using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerClient.Domain.Entities
{
    public class User
    {
        public string UserId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public string? Role { get; set; }

        public int EventCount { get; set; }
    }


}
