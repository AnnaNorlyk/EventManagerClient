using Newtonsoft.Json;

namespace EventManagerClient.Domain.Entities
{
    public class Event
    {


        public int EventId { get; set; }
        public string? Location { get; set; }
        public string? EventName { get; set; }
        public string? EventDescription { get; set; }
        public string? EventCreationTimestamp { get; set; }


    }
}
