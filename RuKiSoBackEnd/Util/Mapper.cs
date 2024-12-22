using RuKiSoBackEnd.Models.Domains;
using RuKiSoBackEnd.Models.DTOs;

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
                Name = transaction.Name,
                TranType = transaction.TranType,
                Quantity = transaction.Quantity,
                Value = transaction.Value,
                TranDate = transaction.TranDate,
                ProductId = transaction.ProductId,
                IngredientId = transaction.IngredientId,
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

        #region Batch
        public static Batches ToDomain(this BatchRequest request)
        {
            return new Batches
            {
                StartDate = request.StartDate,
                EstimateEndDate = request.EstimateEndDate,
                Yield = request.Yield,
                ProductId = request.ProductId,
                BatchIngredients = request.BatchIngredients.Select(bi => new BatchIngredient
                {
                    IngredientId = bi.IngredientId,
                    Quantity = bi.Quantity
                }).ToList()
            };
        }

        public static BatchResponse ToDTO(this Batches batch)
        {
            return new BatchResponse
            {
                Id = batch.Id,
                StartDate = batch.StartDate,
                EstimateEndDate = batch.EstimateEndDate,
                Yield = batch.Yield,
                ProductId = batch.ProductId,
                Product = batch.Product != null ? new ProductDTO
                {
                    Id = batch.Product.Id,
                    Name = batch.Product.Name,
                    Quantity = batch.Product.Quantity
                } : null,
                BatchIngredients = batch.BatchIngredients.Select(bi => new BatchIngredientDTO
                {
                    BatchId = bi.BatchId,
                    IngredientId = bi.IngredientId,
                    Quantity = bi.Quantity,
                    Ingredient = bi.Ingredient != null ? new IngredientDTO
                    {
                        Id = bi.Ingredient.Id,
                        Name = bi.Ingredient.Name,
                        Unit = bi.Ingredient.Unit,
                        Quantity = bi.Ingredient.Quantity
                    } : null
                }).ToList()
            };
        }

        #endregion
    }
}
