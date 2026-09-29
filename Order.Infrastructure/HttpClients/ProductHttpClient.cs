using System.Net.Http;
using System.Text;
using System.Text.Json;
using Order.Application.Integrations;

namespace Order.Infrastructure.HttpClients
{
    public class ProductHttpClient : IProductIntegration
    {
        private readonly HttpClient _httpClient;

        public ProductHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductInfoDto?> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/product/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductInfoDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateStockAsync(int productId, int newStock)
        {
            var payload = new StringContent(newStock.ToString(), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"/api/product/{productId}/stock", payload);
        }
    }
}
