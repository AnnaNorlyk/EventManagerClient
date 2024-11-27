using System.Collections.ObjectModel;
using System.Windows;
using EventManagerClient.Domain.Entities;

namespace EventManagerClient.Presentation.View
{
    public partial class UsersWindow : Window
    {
        public ObservableCollection<RequestItem> Requests { get; set; }

        public UsersWindow()
        {
            InitializeComponent();

            // Example data for Requests
            Requests = new ObservableCollection<RequestItem>
            {
                new RequestItem
                {
                    RequesterName = "John Doe",
                    RequesterID = 12345,
                    RequesterEventCount = 5,
                    RequesterDesc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
                },
                new RequestItem
                {
                    RequesterName = "Jane Doe",
                    RequesterID = 67890,
                    RequesterEventCount = 3,
                    RequesterDesc = "Lorem ipsum dolor sit amet, consectetur adipiscing elit."
                }
            };

            DataContext = this;
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Godkendt");
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Afvist");
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
