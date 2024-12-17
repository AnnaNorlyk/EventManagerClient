using EventManagerClient.Domain.Entities;
using EventManagerClient.Domain.Interfaces;
using EventManagerClient.Infastructure.API;

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

        public async Task DeleteUserAsync(int userId)
        {
            var response = await _userApiService.DeleteUserAsync(userId);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userApiService.UpdateUserAsync(user); 
        }
        public async Task CreateUserAsync(User user)
        {
            await _userApiService.CreateUserAsync(user);
        }

    }
}
