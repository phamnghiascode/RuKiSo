namespace RuKiSoBackEnd.Models.DTOs
{
    public class BatchRes
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimateEndDate { get; set; }
        public int Yield { get; set; }
        public int? ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public List<BatchIngredientDTO> BatchIngredients { get; set; } = new();
    }
}
