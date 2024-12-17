using EventManagerClient.AppLayer.DTOs;
using EventManagerClient.Domain.Entities;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

namespace EventManagerClient.Infastructure.API
{


    public class UserApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
                    Email = dto.UserEmail,
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
    }


}
