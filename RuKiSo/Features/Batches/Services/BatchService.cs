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
        private readonly TimeSpan timeout = TimeSpan.FromSeconds(30);

        public BatchService(ApiClientOptions apiClientOptions)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiClientOptions.ApiBaseAddress),
                Timeout = timeout
            };
        }

        public async Task<BatchResponse?> CreateAsync(BatchRequest request)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync("/api/Batch", request);
                if (response.IsSuccessStatusCode)
                {
                    var batchRes = await response.Content.ReadFromJsonAsync<BatchRes>();
                    return batchRes?.ToViewModel();
                }

                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to create batch: {error}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var response = await httpClient.DeleteAsync($"/api/Batch/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to delete batch: {error}");
                }
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<BatchResponse>?> GetAllAsync()
        {
            try
            {
                var response = await httpClient.GetAsync("/api/Batch");
                if (response.IsSuccessStatusCode)
                {
                    var batches = await response.Content.ReadFromJsonAsync<IEnumerable<BatchRes>>();
                    return batches?.Select(b => b.ToViewModel());
                }

                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to get batches: {error}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BatchResponse?> GetByIdAsync(int id)
        {
            try
            {
                var response = await httpClient.GetAsync($"/api/Batch/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var batch = await response.Content.ReadFromJsonAsync<BatchRes>();
                    return batch?.ToViewModel();
                }

                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to get batch: {error}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BatchResponse?> UpdateAsync(int id, BatchRequest request)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync($"/api/Batch/{id}", request);
                if (response.IsSuccessStatusCode)
                {
                    var batchRes = await response.Content.ReadFromJsonAsync<BatchRes>();
                    return batchRes?.ToViewModel();
                }

                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to update batch: {error}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
