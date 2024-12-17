using EventManagerClient.Domain.Entities;
using EventManagerClient.Presentation.ViewModels;
using System.Windows;

namespace EventManagerClient.Presentation.View
{
    /// <summary>
    /// Interaction logic for EventEditWindow.xaml
    /// </summary>
    public partial class EventEditWindow : Window
    {
        private readonly IEventRepository _eventRepository;
        private readonly EventsViewModel _parentViewModel;
        private Event _events; 
        public EventEditWindow(Event events, IEventRepository eventRepository, EventsViewModel parentViewModel)
        {
            InitializeComponent();
            _events = events; 
            _parentViewModel = parentViewModel;
            _eventRepository = eventRepository;

            DataContext = _events; 

        }


    }
}
