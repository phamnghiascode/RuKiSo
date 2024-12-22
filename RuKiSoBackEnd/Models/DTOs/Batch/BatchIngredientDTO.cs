namespace RuKiSoBackEnd.Models.DTOs
{
    public class BatchIngredientDTO
    {
        public int BatchId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
        public IngredientDTO Ingredient { get; set; }
    }
}
