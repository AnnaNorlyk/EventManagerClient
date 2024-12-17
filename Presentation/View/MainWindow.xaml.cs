using EventManagerClient.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EventManagerClient.Presentation.View
{
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

        }

        private void EventsButton_Click(object sender, RoutedEventArgs e)
        {
            var eventsWindow = _serviceProvider.GetRequiredService<EventsWindow>();
            this.Hide();
            eventsWindow.ShowDialog();
            this.Show();
        }
        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            var usersWindow = _serviceProvider.GetRequiredService<UsersWindow>();
            this.Hide();
            usersWindow.ShowDialog();
            this.Show();
        }


        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Navigating to Reports.");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
