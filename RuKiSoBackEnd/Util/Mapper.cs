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
                Quantity = products.Quantity,
                Price = products.Price,
            };
        }
        #endregion

        #region Ingredient
        public static Ingredients ToDomain(this IngredientRequest ingredientRequest)
        {
            return new Ingredients()
            {
                Name = ingredientRequest.Name,
                Unit = ingredientRequest.Unit,
                Quantity = ingredientRequest.Quantity,
                PurchasePrice = ingredientRequest.PurchasePrice
            };
        }

        public static IngredientRespone ToDTO(this Ingredients ingredients)
        {
            return new IngredientRespone()
            {
                Id = ingredients.Id,
                Name = ingredients.Name,
                Unit = ingredients.Unit,
                Quantity = ingredients.Quantity
            };
        }

        #endregion
    }
}
