using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerClient.AppLayer.DTOs
{
    public class EventDto
    {
        [JsonProperty("eventId")]
        public string? EventId { get; set; }



        [JsonProperty("eventName")]
        public string? EventName { get; set; }

        [JsonProperty("eventDescription")]
        public string? EventDescription { get; set; }

        [JsonProperty("eventCategory")]
        public string? EventCategory { get; set; }

        [JsonProperty("eventStart")]
        public string? EventStart { get; set; }

        [JsonProperty("eventEnd")]
        public string? EventEnd { get; set; }

        [JsonProperty("eventStatus")]
        public string? EventStatus { get; set; }

        [JsonProperty("eventLocation")]
        public string? EventLocation { get; set; }

        [JsonProperty("userId")]
        public string? UserId { get; set; } // Link to the User (fk)

        [JsonProperty("eventCreationTimestamp")]
        public DateTime EventCreationTimestamp { get; set; } = DateTime.Now;
    }

}
