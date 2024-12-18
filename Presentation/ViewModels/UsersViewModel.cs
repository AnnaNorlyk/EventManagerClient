using EventManagerClient.AppLayer.UseCases.Users;
using EventManagerClient.Domain.Entities;
using EventManagerClient.Domain.Interfaces;
using EventManagerClient.Presentation.Commands;
using EventManagerClient.Presentation.View;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EventManagerClient.Presentation.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly CreateUserUseCase _createUserUseCase;

        public ObservableCollection<User> Users { get; private set; } // All users
        public ObservableCollection<User> PendingUsers { get; private set; } // Users needing approval

        public ICommand LoadUsersCommand { get; }
        public ICommand ApproveUserCommand { get; }
        public ICommand EditUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand CreateUserCommand { get; }

        private User _selectedPendingUser;
        public User SelectedPendingUser
        {
            get => _selectedPendingUser;
            set
            {
                _selectedPendingUser = value;
                OnPropertyChanged(nameof(SelectedPendingUser));
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public UsersViewModel(IUserRepository userRepository, CreateUserUseCase createUserUseCase)
        {
            _userRepository = userRepository;
            _createUserUseCase = createUserUseCase;

            Users = new ObservableCollection<User>();
            PendingUsers = new ObservableCollection<User>();

            LoadUsersCommand = new RelayCommand(async (param) => await LoadUsersAsync());
            ApproveUserCommand = new RelayCommand(async (param) => await ApproveUserAsync(), (param) => SelectedPendingUser != null);
            EditUserCommand = new RelayCommand(OpenEditUserWindow, CanEditUser);
            DeleteUserCommand = new RelayCommand(async (param) => await DeleteUserAsync());
            CreateUserCommand = new RelayCommand(_ => OpenCreateUserWindow());
        }

        private bool CanEditUser(object param) => SelectedUser != null;

        private void OpenEditUserWindow(object param)
        {
            var editWindow = new EditUserWindow(SelectedUser, _userRepository, this);
            editWindow.ShowDialog();
        }

        private void OpenCreateUserWindow()
        {
            var createWindow = new NewUserWindow(_createUserUseCase, this);
            createWindow.ShowDialog();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUserAsync();
                Users.Clear();
                PendingUsers.Clear();

                foreach (var user in users)
                {
                    Users.Add(user);

                    // Add users who are not "EventHolder" to PendingUsers
                    if (user.Role != "EventHolder")
                    {
                        PendingUsers.Add(user);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error");
            }
        }

        private async Task ApproveUserAsync()
        {
            if (SelectedPendingUser == null)
            {
                MessageBox.Show("Please select a user to approve.", "No User Selected");
                return;
            }

            try
            {
                // Update the role to "EventHolder"
                SelectedPendingUser.Role = "EventHolder";
                await _userRepository.UpdateUserAsync(SelectedPendingUser);

                // Remove the approved user from PendingUsers
                PendingUsers.Remove(SelectedPendingUser);

                MessageBox.Show("User approved successfully!", "Success");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error approving user: {ex.Message}", "Error");
            }
        }

        private async Task DeleteUserAsync()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a user to delete.", "No User Selected");
                return;
            }

            try
            {
                await _userRepository.DeleteUserAsync(SelectedUser.UserId);

                Users.Remove(SelectedUser);

                MessageBox.Show("User deleted successfully.", "Success");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}", "Error");
            }
        }
    }
}
