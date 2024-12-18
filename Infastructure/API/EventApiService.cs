using EventManagerClient.AppLayer.DTOs;
using EventManagerClient.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

public class EventApiService
{
    private readonly HttpClient _httpClient;

    public EventApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Event>> GetAllEventsAsync()
    {
        var response = await _httpClient.GetAsync("events");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var eventDtos = JsonConvert.DeserializeObject<List<EventDto>>(content);

  
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


    public async Task UpdateEventAsync(Event updatedEvent)
    {
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

        var jsonContent = new StringContent(
            JsonConvert.SerializeObject(dto),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PutAsync($"events/{updatedEvent.EventId}", jsonContent);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteEventAsync(string eventId)
    {
        var response = await _httpClient.DeleteAsync($"events/{eventId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<Event> GetEventByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"events/{id}");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var dto = JsonConvert.DeserializeObject<EventDto>(content);

  
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
