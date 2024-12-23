namespace RuKiSoBackEnd.Models.DTOs
{
    public class BatchIngredientAPIDTO
    {
        public int BatchId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
        public IngredientAPIDTO Ingredient { get; set; }
    }
}
