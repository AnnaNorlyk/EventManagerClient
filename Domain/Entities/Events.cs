using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerClient.Domain.Entities
{
    public class Event
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string EventId { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string Time { get; set; }
    }
}
