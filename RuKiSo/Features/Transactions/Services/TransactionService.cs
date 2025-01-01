using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Net.Http.Json;

namespace RuKiSo.Features.Services
{
    public class TransactionService : IGenericService<TransactionResponse, TransactionRequest>
    {
        private readonly HttpClient httpClient;

        public TransactionService(ApiClientOptions apiClientOptions)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiClientOptions.ApiBaseAddress)
            };
        }

        public async Task<IEnumerable<TransactionResponse>?> GetAllAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<TransactionResponse>>("/api/Transaction");
        }

        public async Task<TransactionResponse?> GetByIdAsync(int id)
        {
            return await httpClient.GetFromJsonAsync<TransactionResponse>($"/api/Transaction/{id}");
        }

        public async Task<TransactionResponse?> CreateAsync(TransactionRequest transactionRequest)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Transaction", transactionRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TransactionResponse>();
            }
            return null;
        }

        public async Task<TransactionResponse?> UpdateAsync(int id, TransactionRequest transactionRequest)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/Transaction/{id}", transactionRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TransactionResponse>();
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"/api/Transaction/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
