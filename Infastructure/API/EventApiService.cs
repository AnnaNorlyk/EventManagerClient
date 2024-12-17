using EventManagerClient.AppLayer.DTOs;
using EventManagerClient.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http;

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
            Location = dto.Location,
            EventName = dto.EventName,
            EventDescription = dto.EventDescription,
        }).ToList() ?? new List<Event>();
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
            Location = dto.Location,
            EventName = dto.EventName,
            EventDescription = dto.EventDescription
        };
    }
}
