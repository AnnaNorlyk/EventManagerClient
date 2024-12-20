using EventManagerClient.AppLayer.DTOs;
using EventManagerClient.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

public class EventApiService
{
    private readonly HttpClient _httpClient; // HTTP client for API communication

    public EventApiService(HttpClient httpClient)
    {
        _httpClient = httpClient; // Injecting HttpClient dependency
    }

    // Fetch all events from the API
    public async Task<List<Event>> GetAllEventsAsync()
    {
        // Send GET request to "events" endpoint
        var response = await _httpClient.GetAsync("events");
        response.EnsureSuccessStatusCode(); // Ensure the response is successful

        // Deserialize the response content into a list of EventDto objects
        var content = await response.Content.ReadAsStringAsync();
        var eventDtos = JsonConvert.DeserializeObject<List<EventDto>>(content);

        // Map EventDto objects to Event entities and return them
        return eventDtos?.Select(dto => new Event
        {
            EventId = dto.EventId,
            EventName = dto.EventName,
            EventDescription = dto.EventDescription,
            EventCategory = dto.EventCategory,
            EventStart = dto.EventStart,
            EventEnd = dto.EventEnd,
            EventStatus = dto.EventStatus,
            EventLocation = dto.EventLocation,
            UserId = dto.UserId,
        }).ToList() ?? new List<Event>();
    }

    // Update an existing event
    public async Task UpdateEventAsync(Event updatedEvent)
    {
        // Map Event entity to EventDto object for serialization
        var dto = new EventDto
        {
            EventId = updatedEvent.EventId,
            EventName = updatedEvent.EventName,
            EventDescription = updatedEvent.EventDescription,
            EventCategory = updatedEvent.EventCategory,
            EventStart = updatedEvent.EventStart,
            EventEnd = updatedEvent.EventEnd,
            EventStatus = updatedEvent.EventStatus,
            EventLocation = updatedEvent.EventLocation,
            UserId = updatedEvent.UserId
        };

        // Serialize the EventDto into JSON format
        var jsonContent = new StringContent(
            JsonConvert.SerializeObject(dto),
            Encoding.UTF8,
            "application/json"
        );

        // Send PUT request to update the event
        var response = await _httpClient.PutAsync($"events/{updatedEvent.EventId}", jsonContent);
        response.EnsureSuccessStatusCode(); // Ensure the response is successful
    }

    // Delete an event by its ID
    public async Task DeleteEventAsync(string eventId)
    {
        // Send DELETE request to "events/{eventId}" endpoint
        var response = await _httpClient.DeleteAsync($"events/{eventId}");
        response.EnsureSuccessStatusCode(); // Ensure the response is successful
    }

    // Fetch a specific event by its ID
    public async Task<Event> GetEventByIdAsync(int id)
    {
        // Send GET request to "events/{id}" endpoint
        var response = await _httpClient.GetAsync($"events/{id}");
        response.EnsureSuccessStatusCode(); // Ensure the response is successful

        // Deserialize the response content into an EventDto object
        var content = await response.Content.ReadAsStringAsync();
        var dto = JsonConvert.DeserializeObject<EventDto>(content);

        // Map EventDto to Event entity and return it
        return new Event
        {
            EventId = dto.EventId,
            EventName = dto.EventName,
            EventDescription = dto.EventDescription,
            EventCategory = dto.EventCategory,
            EventStart = dto.EventStart,
            EventEnd = dto.EventEnd,
            EventStatus = dto.EventStatus,
            EventLocation = dto.EventLocation,
            UserId = dto.UserId,
        };
    }
}
