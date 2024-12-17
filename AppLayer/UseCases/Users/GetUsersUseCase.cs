using EventManagerClient.Domain.Entities;
using EventManagerClient.Domain.Interfaces;

namespace EventManagerClient.AppLayer.UseCases.Users
{
    public class GetUsersUseCase
    {
        private readonly IUserRepository _userRepository;

        public GetUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> Execute()
        {
            return await _userRepository.GetAllUserAsync();
        }
    }
}
