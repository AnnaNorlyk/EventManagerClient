using EventManagerClient.Domain.Entities;
using EventManagerClient.Domain.Interfaces;
using System.Threading.Tasks;

namespace EventManagerClient.AppLayer.UseCases.Users
{
    public class EditUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public EditUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // Update the user via repository
            await _userRepository.UpdateUserAsync(user);
        }
    }
}
