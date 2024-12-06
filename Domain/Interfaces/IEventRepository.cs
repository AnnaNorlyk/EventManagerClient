using EventManagerClient.Domain.Entities;

public interface IEventRepository
{
    Task<List<Event>> GetAllAsync();
    Task<Event> GetByIdAsync(int id);
}
