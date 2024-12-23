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
        public Task<BatchResponse?> CreateAsync(BatchRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BatchResponse>?> GetAllAsync()
        {
            var batches = await httpClient.GetFromJsonAsync<IEnumerable<BatchRes>>("/api/Batch");
            return batches?.Select(b => b.ToViewModel());
        }

        public Task<BatchResponse?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BatchResponse?> UpdateAsync(int id, BatchRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
