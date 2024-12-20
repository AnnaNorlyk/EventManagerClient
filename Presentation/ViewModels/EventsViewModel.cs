using EventManagerClient.AppLayer.UseCases.Events;
using EventManagerClient.Domain.Entities;
using EventManagerClient.Infrastructure.Repositories;
using EventManagerClient.Presentation.Commands;
using EventManagerClient.Presentation.View;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EventManagerClient.Presentation.ViewModels
{
    public class EventsViewModel : BaseViewModel
    {
        private readonly GetEventsUseCase _getEventsUseCase;
        private readonly EditEventUseCase _editEventUseCase;
        private readonly EventRepository _eventRepository;

        public ObservableCollection<Event> Events { get; private set; } // All Events
        public ObservableCollection<Event> PendingEvents { get; private set; } // Events with "Pending" status

        public ICommand LoadEventsCommand { get; }
        public ICommand ApproveCommand { get; }
        public ICommand RejectCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        private Event _selectedEvent;
        public Event SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                _selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));
            }
        }

        private Event _selectedPendingEvent;
        public Event SelectedPendingEvent
        {
            get => _selectedPendingEvent;
            set
            {
                _selectedPendingEvent = value;
                OnPropertyChanged(nameof(SelectedPendingEvent));
            }
        }

        public EventsViewModel(GetEventsUseCase getEventsUseCase, EditEventUseCase editEventUseCase, IEventRepository eventRepository)
        {
            _getEventsUseCase = getEventsUseCase;
            _editEventUseCase = editEventUseCase;
            _eventRepository = (EventRepository?)eventRepository;

            Events = new ObservableCollection<Event>();
            PendingEvents = new ObservableCollection<Event>();

            LoadEventsCommand = new RelayCommand(async (param) => await LoadEventsAsync());
            ApproveCommand = new RelayCommand(async (param) => await ApprovePendingEventAsync(), (param) => SelectedPendingEvent != null);
            RejectCommand = new RelayCommand(async (param) => await RejectPendingEventAsync(), (param) => SelectedPendingEvent != null);
            BackCommand = new RelayCommand(_ => Back());
            EditCommand = new RelayCommand(EditEvent, CanEditEvent);
            DeleteCommand = new RelayCommand(DeleteEvent);
        }


        private async Task LoadEventsAsync()
        {
            try
            {
                var events = await _getEventsUseCase.Execute();

                Events.Clear();
                PendingEvents.Clear();

                foreach (var evt in events)
                {
                    Events.Add(evt);

                    if (evt.EventStatus == "Pending")
                    {
                        PendingEvents.Add(evt);
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Failed to load events. Details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

     
        private async Task ApprovePendingEventAsync()
        {
            if (SelectedPendingEvent == null)
            {
                MessageBox.Show("Please select a pending event to approve.", "No Event Selected");
                return;
            }

            try
            {
                
                SelectedPendingEvent.EventStatus = "Approved";
                await _editEventUseCase.Execute(SelectedPendingEvent);
                PendingEvents.Remove(SelectedPendingEvent);

                MessageBox.Show("Event approved successfully!", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error approving event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    
        private async Task RejectPendingEventAsync()
        {
            if (SelectedPendingEvent == null)
            {
                MessageBox.Show("Please select a pending event to reject.", "No Event Selected");
                return;
            }

            try
            {
                
                await _eventRepository.DeleteEventAsync(SelectedPendingEvent.EventId);

                
                PendingEvents.Remove(SelectedPendingEvent);

                MessageBox.Show("Event rejected and deleted successfully!", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error rejecting event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back()
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)?.Close();
        }

        private bool CanEditEvent(object param) => SelectedEvent != null;

        private void EditEvent(object param)
        {
            if (SelectedEvent == null) return;

            var editWindow = new EventEditWindow(SelectedEvent, _editEventUseCase, this);
            editWindow.ShowDialog();
        }

        private async void DeleteEvent(object parameter)
        {
            if (SelectedEvent == null)
            {
                MessageBox.Show("Please select an event to delete.", "No Event Selected");
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete the event '{SelectedEvent.EventName}'?",
                                          "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    
                    await _eventRepository.DeleteEventAsync(SelectedEvent.EventId);

                   
                    Events.Remove(SelectedEvent);

                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
