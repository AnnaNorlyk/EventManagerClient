using EventManagerClient.AppLayer.DTOs;
using EventManagerClient.Domain.Entities;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace EventManagerClient.Infastructure.API
{


    public class UserApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> DeleteUserAsync(int userId)
        {
            return await _httpClient.DeleteAsync($"Users/{userId}");
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {

                var response = await _httpClient.GetAsync("Users");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var userDtos = JsonConvert.DeserializeObject<List<UserDto>>(content);

                if (userDtos == null || !userDtos.Any())
                {
                    return new List<User>();
                }


                var users = userDtos.Select(dto => new User
                {
                    UserId = dto.UserId,
                    UserName = dto.UserName,
                    Firstname = dto.Firstname,
                    Lastname = dto.Lastname,
                    UserEmail = dto.UserEmail,
                    UserPassword = dto.UserPassword,
                    EventCount = dto.EventCount
                }).ToList();


                return users;
            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"HTTP request error: {httpEx.Message}");
                throw;
            }
            catch (JsonException jsonEx)
            {
                Debug.WriteLine($"JSON deserialization error: {jsonEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }


        public async Task UpdateUserAsync(User user)
        {
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PutAsync($"Users/{user.UserId}", jsonContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task CreateUserAsync(User user)
        {
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("Users", jsonContent);
            response.EnsureSuccessStatusCode();
        }


    }
}
