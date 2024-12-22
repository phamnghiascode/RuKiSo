namespace RuKiSoBackEnd.Models.Domains
{
    public class BatchIngredient
    {
        public int BatchId { get; set; }
        public int IngredientId { get; set; }
        public int Quantity { get; set; }
        public Batches Batch { get; set; }
        public Ingredients Ingredient { get; set; }
    }
}
