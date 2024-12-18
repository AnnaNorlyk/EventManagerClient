using EventManagerClient.Domain.Entities;
using EventManagerClient.Domain.Interfaces;
using EventManagerClient.Presentation.ViewModels;
using System.Windows;

namespace EventManagerClient.Presentation.View
{
    public partial class EditUserWindow : Window
    {
        private readonly IUserRepository _userRepository;
        private readonly UsersViewModel _parentViewModel;
        private User _user;

        public EditUserWindow(User user, IUserRepository userRepository, UsersViewModel parentViewModel)
        {
            InitializeComponent();
            _user = user;
            _userRepository = userRepository;
            _parentViewModel = parentViewModel;

            DataContext = _user;
           
        }


        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _userRepository.UpdateUserAsync(_user);
                MessageBox.Show("User updated successfully!", "Success");

                // Refresh the users in the parent ViewModel
                if (_parentViewModel.LoadUsersCommand.CanExecute(null))
                {
                    _parentViewModel.LoadUsersCommand.Execute(null);
                }

                Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}", "Error");
            }
        }
    }
}
