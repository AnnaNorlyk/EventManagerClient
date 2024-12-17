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

        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public int EventCount { get; set; }


    }

}
