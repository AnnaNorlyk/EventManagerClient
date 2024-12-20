using EventManagerClient.AppLayer.DTOs;
using EventManagerClient.Domain.Entities;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace EventManagerClient.Infastructure.API
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient; // HTTP client to communicate with the API

        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient; // Dependency injection of HttpClient
        }

        // Delete a user by their ID
        public async Task<HttpResponseMessage> DeleteUserAsync(string userId)
        {
            return await _httpClient.DeleteAsync($"Users/{userId}");
        }

        // Fetch all users from the API
        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                // Send GET request to the "Users" endpoint
                var response = await _httpClient.GetAsync("Users");
                response.EnsureSuccessStatusCode(); // Ensure the response is successful

                // Deserialize the response content into a list of UserDto objects
                var content = await response.Content.ReadAsStringAsync();
                var userDtos = JsonConvert.DeserializeObject<List<UserDto>>(content);

                if (userDtos == null || !userDtos.Any())
                {
                    // Return an empty list if no users are found
                    return new List<User>();
                }

                // Map UserDto objects to User entities
                var users = userDtos.Select(dto => new User
                {
                    UserId = dto.UserId,
                    Firstname = dto.Firstname,
                    Lastname = dto.Lastname,
                    UserEmail = dto.UserEmail,
                    UserPassword = dto.UserPassword,
                    Role = dto.Role,
                    EventCount = dto.EventCount
                }).ToList();

                return users; // Return the list of User entities
            }
            catch (HttpRequestException httpEx)
            {
                // Log HTTP request exceptions
                Debug.WriteLine($"HTTP request error: {httpEx.Message}");
                throw;
            }
            catch (JsonException jsonEx)
            {
                // Log JSON deserialization exceptions
                Debug.WriteLine($"JSON deserialization error: {jsonEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log any unexpected exceptions
                Debug.WriteLine($"Unexpected error: {ex.Message}");
                throw;
            }
        }

        // Update an existing user
        public async Task UpdateUserAsync(User user)
        {
            // Serialize the user object into JSON format
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                "application/json"
            );

            // Send PUT request to update the user
            var response = await _httpClient.PutAsync($"Users/{user.UserId}", jsonContent);
            response.EnsureSuccessStatusCode(); // Ensure the response is successful
        }

        // Create a new user
        public async Task CreateUserAsync(User user)
        {
            // Serialize the user object into JSON format
            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                "application/json"
            );

            // Send POST request to create the user
            var response = await _httpClient.PostAsync("Users", jsonContent);
            response.EnsureSuccessStatusCode(); // Ensure the response is successful
        }
    }
}
