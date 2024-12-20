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
        private readonly IUserRepository _userRepository; // Interface for user data operations
        private readonly CreateUserUseCase _createUserUseCase; // Use case for creating a new user

        public ObservableCollection<User> Users { get; private set; } // Collection of all users
        public ObservableCollection<User> PendingUsers { get; private set; } // Collection of users pending approval

        // Commands for various user actions
        public ICommand LoadUsersCommand { get; }
        public ICommand ApproveUserCommand { get; }
        public ICommand EditUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand CreateUserCommand { get; }
        public ICommand BackCommand { get; }

        private User _selectedPendingUser; // Selected user for approval
        public User SelectedPendingUser
        {
            get => _selectedPendingUser;
            set
            {
                _selectedPendingUser = value;
                OnPropertyChanged(nameof(SelectedPendingUser));
            }
        }

        private User _selectedUser; // Selected user for editing or deletion
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        // Constructor to initialize dependencies and commands
        public UsersViewModel(IUserRepository userRepository, CreateUserUseCase createUserUseCase)
        {
            _userRepository = userRepository;
            _createUserUseCase = createUserUseCase;

            Users = new ObservableCollection<User>();
            PendingUsers = new ObservableCollection<User>();

            // Initialize commands with their respective actions
            LoadUsersCommand = new RelayCommand(async (param) => await LoadUsersAsync());
            ApproveUserCommand = new RelayCommand(async (param) => await ApproveUserAsync(), (param) => SelectedPendingUser != null);
            EditUserCommand = new RelayCommand(OpenEditUserWindow, CanEditUser);
            DeleteUserCommand = new RelayCommand(async (param) => await DeleteUserAsync());
            CreateUserCommand = new RelayCommand(_ => OpenCreateUserWindow());
            BackCommand = new RelayCommand(_ => NavigateBack());
        }

        // Check if editing is allowed (user selected)
        private bool CanEditUser(object param) => SelectedUser != null;

        // Open a window for editing the selected user
        private void OpenEditUserWindow(object param)
        {
            var editWindow = new EditUserWindow(SelectedUser, _userRepository, this);
            editWindow.ShowDialog();
        }

        // Open a window for creating a new user
        private void OpenCreateUserWindow()
        {
            var createWindow = new NewUserWindow(_createUserUseCase, this);
            createWindow.ShowDialog();
        }

        // Navigate back by closing the current window
        private void NavigateBack()
        {
            Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive)?.Close();
        }

        // Load all users and segregate pending users
        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllUserAsync();
                Users.Clear();
                PendingUsers.Clear();

                // Add users to collections based on their roles
                foreach (var user in users)
                {
                    Users.Add(user);

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

        // Approve the selected pending user
        private async Task ApproveUserAsync()
        {
            if (SelectedPendingUser == null)
            {
                MessageBox.Show("Please select a user to approve.", "No User Selected");
                return;
            }

            try
            {
                // Change user role to approved and update in repository
                SelectedPendingUser.Role = "EventHolder";
                await _userRepository.UpdateUserAsync(SelectedPendingUser);

                // Remove user from pending collection
                PendingUsers.Remove(SelectedPendingUser);

                MessageBox.Show("User approved successfully!", "Success");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Error approving user: {ex.Message}", "Error");
            }
        }

        // Delete the selected user
        private async Task DeleteUserAsync()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Please select a user to delete.", "No User Selected");
                return;
            }

            try
            {
                // Delete user from repository and collection
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
