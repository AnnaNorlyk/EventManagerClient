using EventManagerClient.Domain.Entities;
using EventManagerClient.Domain.Interfaces;
using System.Threading.Tasks;

namespace EventManagerClient.AppLayer.UseCases.Users
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await _userRepository.CreateUserAsync(user);
        }
    }
}