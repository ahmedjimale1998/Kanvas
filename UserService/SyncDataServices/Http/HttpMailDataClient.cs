using System.Text;
using System.Text.Json;
using UserService.DTOs;

namespace UserService.SyncDataServices.Http
{
    public class HttpMailDataClient : IMailDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpMailDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task SendUserToMail(UserReadDto user)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(user),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync($"{_configuration["MailService"]}", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to MailService was OK!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to MailService was NOT OK!");
            }
        }
    }
}
