using EventManagerClient.Domain.Interfaces;
using System.Threading.Tasks;

namespace EventManagerClient.AppLayer.UseCases.Users
{
    public class DeleteUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid User ID", nameof(userId));

            // Delete the user via repository
            await _userRepository.DeleteUserAsync(userId);
        }
    }
}
