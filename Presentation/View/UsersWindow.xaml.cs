using System.Collections.ObjectModel;
using System.Windows;
using EventManagerClient.Domain.Entities;
using EventManagerClient.Presentation.ViewModels;

namespace EventManagerClient.Presentation.View
{
    public partial class UsersWindow : Window
    {
        private readonly UsersViewModel _viewModel;
        public UsersWindow(UsersViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel.LoadUsersCommand.CanExecute(null))
            {
                _viewModel.LoadUsersCommand.Execute(null);
            }
        }
    }
}
