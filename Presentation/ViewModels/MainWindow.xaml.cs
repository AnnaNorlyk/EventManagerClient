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
            usersWindow.Show();
            Close();
        }


        private void EventsButton_Click(object sender, RoutedEventArgs e)
        {
            var eventsWindow = new EventsWindow();
            eventsWindow.Show();
            Close();

            void ReportsButton_Click(object sender, RoutedEventArgs e)
            {
                MessageBox.Show("Navigating to Reports.");

            }

            void CloseButton_Click(object sender, RoutedEventArgs e)
            {
                
            }
        }
    }
}