using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Net.Http.Json;

namespace RuKiSo.Features.Services
{
    public class IngredientService : IGenericService<IngredientRespone, IngredientRequest>
    {
        private readonly HttpClient httpClient;

        public IngredientService(ApiClientOptions apiClientOptions)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiClientOptions.ApiBaseAddress)
            };
        }

        public async Task<IEnumerable<IngredientRespone>?> GetAllAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<IngredientRespone>>("/api/Ingredient");
        }

        public async Task<IngredientRespone?> GetByIdAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<IngredientRespone>($"/api/Ingredient/{id}");
        }

        public async Task<IngredientRespone?> CreateAsync(IngredientRequest ingredientRequest)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Ingredient", ingredientRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IngredientRespone>();
            }
            return null;
        }

        public async Task<IngredientRespone?> UpdateAsync(int id, IngredientRequest ingredientRequest)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/Ingredient/{id}", ingredientRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IngredientRespone>();
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"/api/Ingredient/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
