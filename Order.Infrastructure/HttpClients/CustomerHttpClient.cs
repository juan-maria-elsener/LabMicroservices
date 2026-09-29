using System.Net.Http;
using System.Text.Json;
using Order.Application.Integrations;

namespace Order.Infrastructure.HttpClients
{
    public class CustomerHttpClient : ICustomerIntegration
    {
        private readonly HttpClient _httpClient;

        public CustomerHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomerInfoDto?> GetCustomerByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/customer/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CustomerInfoDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}