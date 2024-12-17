using Newtonsoft.Json;

namespace EventManagerClient.Domain.Entities
{
    public class Event
    {

        public string? EventId { get; set; }

        public string? EventName { get; set; }
        public string? EventDescription { get; set; }
        public string? EventCategory { get; set; }
        public string? EventStart { get; set; }
        public string? EventEnd { get; set; }
        public string? EventStatus { get; set; }
        public string? EventLocation { get; set; }
        public string? UserId { get; set; } // Link to the User (fk)
        public DateTime EventCreationTimestamp { get; set; } = DateTime.Now;


    }
}
