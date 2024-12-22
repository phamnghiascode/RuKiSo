using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
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
            return await httpClient.GetFromJsonAsync<IEnumerable<BatchResponse>>("/api/Batch");
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
