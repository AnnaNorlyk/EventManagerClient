using System.Collections.ObjectModel;
using System.Windows;
using EventManagerClient.Domain.Entities; 

namespace EventManagerClient.Presentation.View
{
    public partial class EventsWindow : Window
    {
        public ObservableCollection<EventRequest> EventRequests { get; set; }
        public ObservableCollection<Event> Events { get; set; }

        public EventsWindow()
        {
            InitializeComponent();

            // Sample data for EventRequests
            EventRequests = new ObservableCollection<EventRequest>
            {
                new EventRequest
                {
                    RequesterName = "John Doe",
                    RequesterID = 12345,
                    EventName = "Annual Gala",
                    EventDescription = "A grand annual gala event."
                },
                new EventRequest
                {
                    RequesterName = "Jane Doe",
                    RequesterID = 67890,
                    EventName = "Tech Meetup",
                    EventDescription = "An event for tech enthusiasts."
                }
            };

            // Sample data for Events
            Events = new ObservableCollection<Event>
            {
                new Event { Name = "Annual Gala", Date = "2023-12-10", Location = "Conference Center of the new world order" },
                new Event { Name = "Tech Meetup", Date = "2023-11-15", Location = "Tech Hub" }
            };


            DataContext = this;
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Godkend");

        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Afvis");
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close(); 
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ret");

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Slet");

        }
    }
}
