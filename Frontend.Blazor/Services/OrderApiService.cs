using System.Net.Http.Json;
using Frontend.Blazor.Models;

namespace Frontend.Blazor.Services
{
    public class OrderApiService
    {
        private readonly HttpClient _httpClient;

        public OrderApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("OrderApi");
        }

        public async Task<List<OrderDto>> GetOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<OrderDto>>("api/order") ?? new List<OrderDto>();
        }

        public async Task CreateOrderAsync(OrderCreateDto order)
        {
            var response = await _httpClient.PostAsJsonAsync("api/order", order);

            if (!response.IsSuccessStatusCode)
            {
                var errorMsg = await response.Content.ReadAsStringAsync();
                throw new Exception($"No se pudo crear la orden. {errorMsg}");
            }
        }
    }
}