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
        private readonly GetEventsUseCase _getEventsUseCase; // Use case to fetch events
        private readonly EditEventUseCase _editEventUseCase; // Use case to edit events
        private readonly EventRepository _eventRepository; // Repository for event data operations

        public ObservableCollection<Event> Events { get; private set; } // All events
        public ObservableCollection<Event> PendingEvents { get; private set; } // Events with "Pending" status

        // Commands for various event actions
        public ICommand LoadEventsCommand { get; }
        public ICommand ApproveCommand { get; }
        public ICommand RejectCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        private Event _selectedEvent; // Selected event for actions like edit or delete
        public Event SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                _selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));
            }
        }

        private Event _selectedPendingEvent; // Selected pending event for approval or rejection
        public Event SelectedPendingEvent
        {
            get => _selectedPendingEvent;
            set
            {
                _selectedPendingEvent = value;
                OnPropertyChanged(nameof(SelectedPendingEvent));
            }
        }

        // Constructor to initialize dependencies and commands
        public EventsViewModel(GetEventsUseCase getEventsUseCase, EditEventUseCase editEventUseCase, IEventRepository eventRepository)
        {
            _getEventsUseCase = getEventsUseCase;
            _editEventUseCase = editEventUseCase;
            _eventRepository = (EventRepository?)eventRepository;

            Events = new ObservableCollection<Event>();
            PendingEvents = new ObservableCollection<Event>();

            // Initialize commands
            LoadEventsCommand = new RelayCommand(async (param) => await LoadEventsAsync());
            ApproveCommand = new RelayCommand(async (param) => await ApprovePendingEventAsync(), (param) => SelectedPendingEvent != null);
            RejectCommand = new RelayCommand(async (param) => await RejectPendingEventAsync(), (param) => SelectedPendingEvent != null);
            BackCommand = new RelayCommand(_ => Back());
            EditCommand = new RelayCommand(EditEvent, CanEditEvent);
            DeleteCommand = new RelayCommand(DeleteEvent);
        }

        // Load all events and segregate pending events
        private async Task LoadEventsAsync()
        {
            try
            {
                var events = await _getEventsUseCase.Execute();

                Events.Clear();
                PendingEvents.Clear();

                // Add events to respective collections
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
                // Show error message on failure
                MessageBox.Show($"Failed to load events. Details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Approve the selected pending event
        private async Task ApprovePendingEventAsync()
        {
            if (SelectedPendingEvent == null)
            {
                MessageBox.Show("Please select a pending event to approve.", "No Event Selected");
                return;
            }

            try
            {
                // Update event status to "Approved"
                SelectedPendingEvent.EventStatus = "Approved";
                await _editEventUseCase.Execute(SelectedPendingEvent);

                // Remove event from pending collection
                PendingEvents.Remove(SelectedPendingEvent);

                MessageBox.Show("Event approved successfully!", "Success");
            }
            catch (Exception ex)
            {
                // Show error message on failure
                MessageBox.Show($"Error approving event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Reject the selected pending event
        private async Task RejectPendingEventAsync()
        {
            if (SelectedPendingEvent == null)
            {
                MessageBox.Show("Please select a pending event to reject.", "No Event Selected");
                return;
            }

            try
            {
                // Delete the event from repository
                await _eventRepository.DeleteEventAsync(SelectedPendingEvent.EventId);

                // Remove event from pending collection
                PendingEvents.Remove(SelectedPendingEvent);

                MessageBox.Show("Event rejected and deleted successfully!", "Success");
            }
            catch (Exception ex)
            {
                // Show error message on failure
                MessageBox.Show($"Error rejecting event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Navigate back by closing the current window
        private void Back()
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)?.Close();
        }

        // Check if editing is allowed (event selected)
        private bool CanEditEvent(object param) => SelectedEvent != null;

        // Open a window to edit the selected event
        private void EditEvent(object param)
        {
            if (SelectedEvent == null) return;

            var editWindow = new EventEditWindow(SelectedEvent, _editEventUseCase, this);
            editWindow.ShowDialog();
        }

        // Delete the selected event
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
                    // Delete event from repository
                    await _eventRepository.DeleteEventAsync(SelectedEvent.EventId);

                    // Remove event from collection
                    Events.Remove(SelectedEvent);
                }
                catch (Exception ex)
                {
                    // Show error message on failure
                    MessageBox.Show($"Error deleting event: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
