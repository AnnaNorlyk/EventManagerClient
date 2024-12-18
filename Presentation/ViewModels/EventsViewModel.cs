using EventManagerClient.AppLayer.UseCases.Events;
using EventManagerClient.Domain.Entities;
using EventManagerClient.Domain.Interfaces;
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
        private readonly IEventRepository _eventRepository;

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

        public EventsViewModel(
            GetEventsUseCase getEventsUseCase,
            EditEventUseCase editEventUseCase,
            IEventRepository eventRepository)
        {
            _getEventsUseCase = getEventsUseCase;
            _editEventUseCase = editEventUseCase;
            _eventRepository = eventRepository;

            Events = new ObservableCollection<Event>();
            PendingEvents = new ObservableCollection<Event>();

            LoadEventsCommand = new RelayCommand(async (param) => await LoadEventsAsync());
            ApproveCommand = new RelayCommand(async (param) => await ApprovePendingEventAsync(), (param) => SelectedPendingEvent != null);
            RejectCommand = new RelayCommand(async (param) => await RejectPendingEventAsync(), (param) => SelectedPendingEvent != null);
            BackCommand = new RelayCommand(Back);
            EditCommand = new RelayCommand(EditEvent, CanEditEvent);
            DeleteCommand = new RelayCommand(DeleteEvent);
        }

        /// <summary>
        /// Loads all events and filters pending events for the PendingEvents list.
        /// </summary>
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

                    // Filter events with "Pending" status
                    if (evt.EventStatus == "Pending")
                    {
                        PendingEvents.Add(evt);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading events: {ex.Message}");
                MessageBox.Show($"Failed to load events. Details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Approves the selected pending event by updating its status to "Approved".
        /// </summary>
        private async Task ApprovePendingEventAsync()
        {
            if (SelectedPendingEvent == null)
            {
                MessageBox.Show("Please select a pending event to approve.", "No Event Selected");
                return;
            }

            try
            {
                // Update the status of the selected event
                SelectedPendingEvent.EventStatus = "Approved";
                await _editEventUseCase.Execute(SelectedPendingEvent);

                // Remove the event from the PendingEvents list
                PendingEvents.Remove(SelectedPendingEvent);

                MessageBox.Show("Event approved successfully!", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error approving event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Rejects the selected pending event by deleting it.
        /// </summary>
        private async Task RejectPendingEventAsync()
        {
            if (SelectedPendingEvent == null)
            {
                MessageBox.Show("Please select a pending event to reject.", "No Event Selected");
                return;
            }

            try
            {
                // Delete the event from the backend
                await _eventRepository.DeleteEventAsync(SelectedPendingEvent.EventId);

                // Remove the event from the PendingEvents list
                PendingEvents.Remove(SelectedPendingEvent);

                MessageBox.Show("Event rejected and deleted successfully!", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error rejecting event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back(object parameter)
        {
            MessageBox.Show("Going back.");
        }

        private bool CanEditEvent(object param) => SelectedEvent != null;

        private void EditEvent(object param)
        {
            if (SelectedEvent == null) return;

            var editWindow = new EventEditWindow(SelectedEvent, _editEventUseCase, this);
            editWindow.ShowDialog();
        }

        private void DeleteEvent(object parameter)
        {
            if (SelectedEvent == null)
            {
                MessageBox.Show("Please select an event to delete.", "No Event Selected");
                return;
            }

            // Simulate deletion (implement backend deletion if needed)
            MessageBox.Show($"Deleted event: {SelectedEvent.EventName}", "Deleted");
        }
    }
}
