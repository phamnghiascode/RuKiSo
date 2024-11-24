using RuKiSoBackEnd.Models.Domains;
using RuKiSoBackEnd.Models.DTOs;
using System.Runtime.CompilerServices;

namespace RuKiSoBackEnd.Util
{
    public static class Mapper
    {
        #region Product
        public static Products ToDomain(this ProductRequest productRequest)
        {
            return new Products()
            {
                Name = productRequest.Name,
                Description = productRequest.Description,
                Quantity = productRequest.Quantity,
                Price = productRequest.Price,
            };
        }

        public static ProductRespone ToDTO(this Products products)
        {
            return new ProductRespone()
            {
                Id = products.Id,
                Name = products.Name,
                Description = products.Description,
                Price = products.Price,
            };
        }
        #endregion
    }
}
