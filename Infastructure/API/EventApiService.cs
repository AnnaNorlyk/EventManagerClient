using EventManagerClient.Domain.Entities;
using Newtonsoft.Json;
using System.Net.Http;

public class EventApiService : IEventRepository
{
    private readonly HttpClient _httpClient;

    public EventApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Event>> GetAllAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("http://localhost:8080/api/events");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: API returned status code {response.StatusCode}");
                return new List<Event>();
            }

            var content = await response.Content.ReadAsStringAsync();
            var events = JsonConvert.DeserializeObject<List<Event>>(content);

            if (events == null)
            {
                Console.WriteLine("Error: Unable to deserialize the response content.");
                return new List<Event>();
            }

            return events;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred: {ex.Message}");
            throw; 
        }
    }

    public Task<Event> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
