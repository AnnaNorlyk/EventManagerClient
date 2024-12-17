using EventManagerClient.AppLayer.UseCases.Events;
using EventManagerClient.Domain.Entities;
using EventManagerClient.Presentation.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace EventManagerClient.Presentation.ViewModels
{
    public class EventsViewModel : INotifyPropertyChanged
    {
        private readonly GetEventsUseCase _getEventsUseCase;

        public ObservableCollection<Event> Events { get; private set; }
        public ObservableCollection<EventRequest> EventRequests { get; private set; }

        public ICommand LoadEventsCommand { get; }
        public ICommand ApproveCommand { get; }
        public ICommand RejectCommand { get; }
        public ICommand BackCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public EventsViewModel(GetEventsUseCase getEventsUseCase)
        {
            _getEventsUseCase = getEventsUseCase;
            Events = new ObservableCollection<Event>();
            EventRequests = new ObservableCollection<EventRequest>();

            LoadEventsCommand = new RelayCommand(async (param) => await LoadEventsAsync());
            ApproveCommand = new RelayCommand(ApproveRequest);
            RejectCommand = new RelayCommand(RejectRequest);
            BackCommand = new RelayCommand(Back);
            EditCommand = new RelayCommand(EditEvent);
            DeleteCommand = new RelayCommand(DeleteEvent);
        }

        private async Task LoadEventsAsync()
        {
            try
            {
                Console.WriteLine("LoadEventsAsync triggered...");
                var events = await _getEventsUseCase.Execute();

                if (events.Count == 0)
                {
                    Console.WriteLine("No events were loaded.");
                }

                Events.Clear();
                foreach (var evt in events)
                {
                    Console.WriteLine($"Adding event: {evt.EventName}");
                    Events.Add(evt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading events: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                System.Windows.MessageBox.Show($"Failed to load events. Details: {ex.Message}", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }


        private void ApproveRequest(object parameter)
        {
            if (parameter is EventRequest request)
            {
                Console.WriteLine($"Approved request for event: {request.EventName}");
                EventRequests.Remove(request);
            }
        }

        private void RejectRequest(object parameter)
        {
            if (parameter is EventRequest request)
            {
                Console.WriteLine($"Rejected request for event: {request.EventName}");
                EventRequests.Remove(request);
            }
        }

        private void Back(object parameter) => Console.WriteLine("Navigating back...");
        private void EditEvent(object parameter) => Console.WriteLine("Editing event...");
        private void DeleteEvent(object parameter) => Console.WriteLine("Deleting event...");


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
