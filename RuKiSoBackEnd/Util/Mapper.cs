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

        public static ProductResponse ToDTO(this Products products)
        {
            return new ProductResponse()
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

        public static IngredientResponse ToDTO(this Ingredients ingredients)
        {
            return new IngredientResponse()
            {
                Id = ingredients.Id,
                Name = ingredients.Name,
                Unit = ingredients.Unit,
                Quantity = ingredients.Quantity,
                PurchasePrice = ingredients.PurchasePrice
            };
        }

        #endregion

        #region Transaction
        public static TransactionRes ToDTO(this Transactions transaction)
        {
            return new TransactionRes
            {
                Id = transaction.Id,
                Name = transaction.Name, // Đã được set tên trong controller
                TranType = transaction.TranType,
                Quantity = transaction.Quantity,
                Value = transaction.Value,
                TranDate = transaction.TranDate,
            };
        }

        public static Transactions ToDomain(this TransactionReq request)
        {
            return new Transactions
            {
                TranType = request.TranType,
                Quantity = request.Quantity,
                Value = request.Value,
                TranDate = request.TranDate ?? DateTime.Now,
                IngredientId = !request.TranType ? request.IngredientId : null,
                ProductId = request.TranType ? request.ProductId : null
            };
        }
        #endregion
    }
}
