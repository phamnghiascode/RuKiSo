using RuKiSo.Features.Models;

namespace RuKiSo.Features.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductRespone>?> GetAll();
        Task<ProductRespone?> GetById(int id);
        Task<ProductRespone?> Create(ProductRequest productRequest);
        Task<ProductRespone?> Update(int id, ProductRequest productRequest);
        Task<bool> Delete(int id);
    }
}
