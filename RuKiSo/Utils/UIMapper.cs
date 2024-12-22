using RuKiSo.Features.Models;

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
    }
}
