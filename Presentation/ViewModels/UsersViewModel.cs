using EventManagerClient.AppLayer.UseCases.Users;
using EventManagerClient.Domain.Entities;
using EventManagerClient.Domain.Interfaces;
using EventManagerClient.Presentation.Commands;
using EventManagerClient.Presentation.View;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EventManagerClient.Presentation.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly CreateUserUseCase _createUserUseCase;

        public ObservableCollection<User> Users { get; private set; }
        public ICommand LoadUsersCommand { get; }
        public ICommand EditUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand CreateUserCommand { get; }

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
            LoadUsersCommand = new RelayCommand(async (param) => await LoadUsersAsync());
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
            var users = await _userRepository.GetAllUserAsync();
            Users.Clear();
            foreach (var user in users)
            {
                Users.Add(user);
            }
        }

        private async Task DeleteUserAsync()
        {
            if (SelectedUser == null)
            {
                System.Windows.MessageBox.Show("Please select a user to delete.", "No User Selected");
                return;
            }

            try
            {
                await _userRepository.DeleteUserAsync(SelectedUser.UserId);

                Users.Remove(SelectedUser);

                System.Windows.MessageBox.Show("User deleted successfully.", "Success");
            }
            catch (System.Exception ex)
            {
                System.Windows.MessageBox.Show($"Error deleting user: {ex.Message}", "Error");
            }
        }
    }
}
