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

        public static Batches ToDomain(this BatchReq request)
        {
            // Validation
            if (request.StartDate >= request.EstimateEndDate)
                throw new ArgumentException("Start date must be before estimate end date");

            if (request.BatchIngredients == null || !request.BatchIngredients.Any())
                throw new ArgumentException("Batch must have at least one ingredient");

            if (request.Yield < 0)
                throw new ArgumentException("Yield cannot be negative");

            if (request.ProductId <= 0)
                throw new ArgumentException("Invalid ProductId");

            return new Batches
            {
                StartDate = request.StartDate,
                EstimateEndDate = request.EstimateEndDate,
                Yield = request.Yield,
                ProductId = request.ProductId,
                BatchIngredients = request.BatchIngredients.Select(bi => new BatchIngredient
                {
                    IngredientId = bi.IngredientId > 0 ? bi.IngredientId :
                        throw new ArgumentException($"Invalid IngredientId: {bi.IngredientId}"),
                    Quantity = bi.Quantity > 0 ? bi.Quantity :
                        throw new ArgumentException($"Quantity must be positive for ingredient {bi.IngredientId}")
                }).ToList()
            };
        }

        public static BatchRes ToDTO(this Batches batch)
        {
            return new BatchRes
            {
                Id = batch.Id,
                StartDate = batch.StartDate,
                EstimateEndDate = batch.EstimateEndDate,
                Yield = batch.Yield,
                ProductId = batch.ProductId,
                Product = batch.Product?.ToProductDTO(),
                BatchIngredients = batch.BatchIngredients.Select(bi => bi.ToBatchIngredientDTO()).ToList()
            };
        }

        private static ProductDTO ToProductDTO(this Products product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity
            };
        }

        private static BatchIngredientDTO ToBatchIngredientDTO(this BatchIngredient bi)
        {
            return new BatchIngredientDTO
            {
                BatchId = bi.BatchId,
                IngredientId = bi.IngredientId,
                Quantity = bi.Quantity,
                Ingredient = bi.Ingredient?.ToIngredientDTO()
            };
        }

        private static IngredientDTO ToIngredientDTO(this Ingredients ingredient)
        {
            return new IngredientDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Unit = ingredient.Unit,
                Quantity = ingredient.Quantity
            };
        }
        #endregion
    }
}
