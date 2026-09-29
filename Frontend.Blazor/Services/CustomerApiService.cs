using System.Net.Http.Json;
using Frontend.Blazor.Models;

namespace Frontend.Blazor.Services
{
    public class CustomerApiService
    {
        private readonly HttpClient _httpClient;

        public CustomerApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CustomerApi");
        }

        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CustomerDto>>("api/customer") ?? new List<CustomerDto>();
        }

        public async Task CreateCustomerAsync(CustomerCreateDto customer)
        {
            await _httpClient.PostAsJsonAsync("api/customer", customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/customer/{id}");
        }
    }
}