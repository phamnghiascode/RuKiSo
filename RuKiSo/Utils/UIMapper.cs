using RuKiSo.Features.Models;
using RuKiSoBackEnd.Models.DTOs;

namespace RuKiSo.Utils
{
    public static class UIMapper
    {
        public static TransactionProductDTO ToTransactionProductDTO(this ProductRespone product)
        {
            return new TransactionProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
                Price = product.Price,
                UsedQuantity = 0
            };
        }
        public static TransactionIngredientDTO ToTransactionIngredientDTO(this IngredientRespone ingredient)
        {
            return new TransactionIngredientDTO
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Quantity = ingredient.Quantity,
                PurchasePrice = ingredient.PurchasePrice,
                UsedQuantity = 0
            };
        }
        public static BatchIngredientDTO ToBatchIngredientDTO(this IngredientRespone ingredient)
        {
            return new BatchIngredientDTO
            {
                Id = ingredient.Id,
                IngredientName = ingredient.Name,
                StoredQuantity = ingredient.Quantity,
                UsedQuantity = 0,
                PricePerUnit = ingredient.PurchasePrice,
                IsSelected = false,
            };
        }
        public static BatchResponse ToViewModel(this BatchRes res)
        {
            if (res == null) return null;
            return new BatchResponse
            {
                Id = res.Id,
                Product = res.Product?.ToViewModel(),
                Ingredients = res.BatchIngredients?.Select(x => x.ToViewModel()).ToList() ?? new(),
                StartDate = res.StartDate,
                EstimateEndDate = res.EstimateEndDate,
                Yield = res.Yield
            };
        }

        public static ProductRespone ToViewModel(this ProductAPIDTO dto)
        {
            if (dto == null) return null;
            return new ProductRespone
            {
                Id = dto.Id,
                Name = dto.Name,
                Quantity = dto.Quantity
            };
        }

        public static BatchIngredientDTO ToViewModel(this BatchIngredientAPIDTO dto)
        {
            if (dto == null) return null;
            return new BatchIngredientDTO
            {
                Id = dto.IngredientId,
                IngredientName = dto.Ingredient?.Name,
                StoredQuantity = dto.Ingredient?.Quantity ?? 0,
                UsedQuantity = dto.Quantity,
                PricePerUnit = dto.Ingredient?.Price ?? 0,
                IsSelected = false
            };
        }
    }
}
