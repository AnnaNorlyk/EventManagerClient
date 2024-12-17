using EventManagerClient.Domain.Entities;
using EventManagerClient.Domain.Interfaces;
using EventManagerClient.Infastructure.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace EventManagerClient.Infastructure.Repos
{
    public class UserRepository : IUserRepository
    {

        private readonly UserApiService _userApiService;
        public UserRepository(UserApiService userApiService) 
        { 
            _userApiService = userApiService;
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _userApiService.GetAllUsersAsync();
        }

    }
}
