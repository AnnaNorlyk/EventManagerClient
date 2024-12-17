using EventManagerClient.AppLayer.UseCases.Users;
using EventManagerClient.Domain.Entities;
using EventManagerClient.Presentation.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace EventManagerClient.Presentation.View
{
    public partial class NewUserWindow : Window
    {
        private readonly CreateUserUseCase _createUserUseCase;
        private readonly UsersViewModel _parentViewModel;
        private User _newUser;

        public NewUserWindow(CreateUserUseCase createUserUseCase, UsersViewModel parentViewModel)
        {
            InitializeComponent();
            _createUserUseCase = createUserUseCase;
            _parentViewModel = parentViewModel;

            _newUser = new User();
            DataContext = _newUser;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _newUser.UserPassword = ((PasswordBox)sender).Password;
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _createUserUseCase.Execute(_newUser);
                MessageBox.Show("User created successfully!", "Success");

                // Refresh parent ViewModel
                if (_parentViewModel.LoadUsersCommand.CanExecute(null))
                    _parentViewModel.LoadUsersCommand.Execute(null);

                Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error creating user: {ex.Message}", "Error");
            }
        }
    }
}
