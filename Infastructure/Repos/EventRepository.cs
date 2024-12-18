using EventManagerClient.Domain.Entities;
using EventManagerClient.Domain.Interfaces;
using EventManagerClient.Infastructure.API;
using EventManagerClient.Infastructure.Repos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagerClient.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventApiService _eventApiService;

        public EventRepository(EventApiService eventApiService)
        {
            _eventApiService = eventApiService;
        }


        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _eventApiService.GetAllEventsAsync();
        }

        public async Task<Event> GetByIdAsync(string id)
        {
            var events = await _eventApiService.GetAllEventsAsync();
            return events.FirstOrDefault(e => e.EventId == id);
        }

        public async Task UpdateEventAsync(Event updatedEvent)
        {
            await _eventApiService.UpdateEventAsync(updatedEvent);
        }

        public async Task DeleteEventAsync(string eventId)
        {
            await _eventApiService.DeleteEventAsync(eventId);
        }

    }
}