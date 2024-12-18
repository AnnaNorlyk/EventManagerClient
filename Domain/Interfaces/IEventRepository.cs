using EventManagerClient.Domain.Entities;

public interface IEventRepository
{
    Task<List<Event>> GetAllEventsAsync();
    Task<Event> GetByIdAsync(string id);
    Task UpdateEventAsync(Event updatedEvent);

    Task DeleteEventAsync(string eventId);


}
