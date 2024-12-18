using EventManagerClient.Domain.Entities;

public class EditEventUseCase
{
    private readonly IEventRepository _eventRepository;

    public EditEventUseCase(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task Execute(Event updatedEvent)
    {
        if (updatedEvent == null)
            throw new ArgumentNullException(nameof(updatedEvent));

        await _eventRepository.UpdateEventAsync(updatedEvent);
    }
}
