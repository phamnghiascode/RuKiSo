using RuKiSo.Utils.MVVM;
using RuKiSo.Features.Models;
using System.Net.Http.Json;

namespace RuKiSo.Features.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(ApiClientOptions apiClientOptions)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiClientOptions.ApiBaseAddress)
            };
        }

        public async Task<IEnumerable<ProductRespone>?> GetAll()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<ProductRespone>>("/api/Product");
        }

        public async Task<ProductRespone?> GetById(int id)
        {
            return await httpClient.GetFromJsonAsync<ProductRespone>($"/api/Product/{id}");
        }

        public async Task<ProductRespone?> Create(ProductRequest productRequest)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Product", productRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProductRespone>();
            }
            return null;
        }

        public async Task<ProductRespone?> Update(int id, ProductRequest productRequest)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/Product/{id}", productRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProductRespone>();
            }

            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var response = await httpClient.DeleteAsync($"/api/Product/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
