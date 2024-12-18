using EventManagerClient.Domain.Entities;
using EventManagerClient.Presentation.ViewModels;
using EventManagerClient.AppLayer.UseCases.Events;

using System.Windows;

namespace EventManagerClient.Presentation.View
{

    public partial class EventEditWindow : Window
    {
        private readonly EditEventUseCase _editEventUseCase;
        private readonly EventsViewModel _parentViewModel;
        private Event _event;

        public EventEditWindow(Event selectedEvent, EditEventUseCase editEventUseCase, EventsViewModel parentViewModel)
        {
            InitializeComponent();
            _event = selectedEvent;
            _editEventUseCase = editEventUseCase;
            _parentViewModel = parentViewModel;

            DataContext = _event;
        }


    }
}
