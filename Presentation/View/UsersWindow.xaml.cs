using System.Collections.ObjectModel;
using System.Windows;
using EventManagerClient.Domain.Entities;
using EventManagerClient.Presentation.ViewModels;

namespace EventManagerClient.Presentation.View
{
    public partial class UsersWindow : Window
    {
        public UsersWindow(UsersViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
