namespace RuKiSo.Utils.MVVM
{
    public interface IGenericService<TResponse, TRequest>
    {
        Task<IEnumerable<TResponse>?> GetAllAsync();
        Task<TResponse?> GetByIdAsync(int id);
        Task<TResponse?> CreateAsync(TRequest request);
        Task<TResponse?> UpdateAsync(int id, TRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
