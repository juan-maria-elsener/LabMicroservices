using System.Net.Http.Json;
using Frontend.Blazor.Models;

namespace Frontend.Blazor.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductApi");
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/product") ?? new List<ProductDto>();
        }

        public async Task CreateProductAsync(ProductCreateDto product)
        {
            await _httpClient.PostAsJsonAsync("api/product", product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/product/{id}");
        }
    }
}