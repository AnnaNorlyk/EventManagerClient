using EventManagerClient.AppLayer.UseCases.Events;
using EventManagerClient.AppLayer.UseCases.Users;
using EventManagerClient.Domain.Entities;
using EventManagerClient.Presentation.Commands;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EventManagerClient.Presentation.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private readonly GetUsersUseCase _getUsersUseCase;

        public ObservableCollection<User> Users { get; private set; }
        public ICommand LoadUsersCommand { get; }

        public UsersViewModel(GetUsersUseCase getUsersUseCase)
        {
            _getUsersUseCase = getUsersUseCase;
            Users = new ObservableCollection<User>();
            LoadUsersCommand = new RelayCommand(async (param) => await LoadUsersAsync());
        }

        private async Task LoadUsersAsync()
        {
           var users = await _getUsersUseCase.Execute();
            Users.Clear();
            foreach (var user in users)
            {
               Users.Add(user);
            }
        }

    }
}
