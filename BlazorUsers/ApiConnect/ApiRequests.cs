using BlazorUsers.ApiConnect.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BlazorUsers.ApiConnect
{
    public class ApiRequests
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiRequests> _logger;

        public ApiRequests(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserDataShort>> GetAllUsersAsync()
        {
            var url = "api/UsersLogins/GetUsers";

            try
            {
                var response = await _httpClient.GetAsync(url).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (string.IsNullOrEmpty(responseContent))
                {
                    _logger.LogWarning("Ответ от сервера пуст.");
                    return [];
                }

                var usersData = JsonSerializer.Deserialize<List<UserDataShort>>(responseContent, new JsonSerializerOptions //
                {
                    PropertyNameCaseInsensitive = true
                });

                return usersData ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при запросе");
                return [];
            }
        }

        public async Task<UserAddData> AddNewUser(ReqDataUser user)
        {
            var url = "api/UsersLogins/createNewUserAndLogin";

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, user);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                var userAddData = JsonSerializer.Deserialize<UserAddData>(responseContent);

                return userAddData ?? new UserAddData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запросе: {ex.Message}");
                return new UserAddData();
            }
        }

        public async Task<List<AuthorizationUser>> Authorization(string email, string pass)
        {
            var url = "api/UsersLogins/Authorization";

            var loginData = new { email, pass };

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, loginData);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();

                var authorizationResult = JsonSerializer.Deserialize<AuthorizationUser>(responseContent);
                return [authorizationResult];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запросе: {ex.Message}");
                return [];
            }
        }
    }
}
