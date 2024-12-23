using RuKiSo.Features.Models;
using RuKiSo.Utils;
using RuKiSo.Utils.MVVM;
using RuKiSoBackEnd.Models.DTOs;
using System.Net.Http.Json;

namespace RuKiSo.Features.Services
{
    public class BatchService : IGenericService<BatchResponse, BatchRequest>
    {
        private readonly HttpClient httpClient;

        public BatchService(ApiClientOptions apiClientOptions)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiClientOptions.ApiBaseAddress)
            };
        }
        public async Task<BatchResponse?> CreateAsync(BatchRequest request)
        {
            var response = await httpClient.PostAsJsonAsync("/api/Batch", request);
            if (response.IsSuccessStatusCode)
            {
                var batchRes = await response.Content.ReadFromJsonAsync<BatchRes>();
                return batchRes?.ToViewModel();
            }
            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"/api/Batch/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<BatchResponse>?> GetAllAsync()
        {
            var batches = await httpClient.GetFromJsonAsync<IEnumerable<BatchRes>>("/api/Batch");
            return batches?.Select(b => b.ToViewModel());
        }

        public async Task<BatchResponse?> GetByIdAsync(int id)
        {
            var batch = await httpClient.GetFromJsonAsync<BatchRes>($"/api/Batch/{id}");
            return batch?.ToViewModel();
        }

        public async Task<BatchResponse?> UpdateAsync(int id, BatchRequest request)
        {
            var response = await httpClient.PutAsJsonAsync($"/api/Batch/{id}", request);
            if (response.IsSuccessStatusCode)
            {
                var batchRes = await response.Content.ReadFromJsonAsync<BatchRes>();
                return batchRes?.ToViewModel();
            }
            return null;
        }
    }
}
