using EventManagerClient.Presentation.ViewModels;
using System.Windows;

namespace EventManagerClient.Presentation.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            var usersWindow = new UsersWindow();
            this.Hide();
            usersWindow.ShowDialog();
            this.Show();
        }

        private void EventsButton_Click(object sender, RoutedEventArgs e)
        {
            var eventsViewModel = (EventsViewModel)DataContext; // Get ViewModel
            var eventsWindow = new EventsWindow(eventsViewModel);
            this.Hide();
            eventsWindow.ShowDialog();
            this.Show();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navigating to Reports.");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Properly close the application
        }
    }

}
