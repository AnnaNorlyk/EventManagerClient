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
            Requests = new ObservableCollection<RequestItem>
        {
            new RequestItem { RequesterName = "John Doe", RequesterID = 12345, RequesterEventCount = 5, RequesterDesc = "Lorem ipsum dolor sit amet." },
            new RequestItem { RequesterName = "Jane Doe", RequesterID = 67890, RequesterEventCount = 3, RequesterDesc = "Lorem ipsum dolor sit amet." }
        };
            DataContext = this;
        }
    

    private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); 
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Godkendt");
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Afvist");
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
