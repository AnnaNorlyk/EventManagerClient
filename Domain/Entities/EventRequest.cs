using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerClient.Domain.Entities
{
    public class EventRequest
    {
        public string? RequesterName { get; set; }
        public int RequesterID { get; set; }
        public string? EventName { get; set; }
        public string? EventDescription { get; set; }
    }
}
