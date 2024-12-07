﻿using Newtonsoft.Json;

namespace EventManagerClient.Domain.Entities
{
    public class Event
    {
        [JsonProperty("eventId")]
        public int EventId { get; set; }

        [JsonProperty("location")]
        public string? Location { get; set; }

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

        [JsonProperty("eventCreationTimestamp")]
        public DateTime EventCreationTimestamp { get; set; }
    }
}
