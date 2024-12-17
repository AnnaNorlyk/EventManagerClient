using EventManagerClient.Domain.Entities;


namespace EventManagerClient.AppLayer.UseCases.Events
{
    public class GetEventsUseCase
    {
        private readonly IEventRepository _eventRepository;

        public GetEventsUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<Event>> Execute()
        {
            return await _eventRepository.GetAllEventsAsync();
        }
    }
}
