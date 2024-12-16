namespace RuKiSoBackEnd.Models.DTOs
{
    public class BatchRespone
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimateEndDate { get; set; }
        public int Yield { get; set; }
        public ProductResponse Product { get; set; }
        public int? ProductId { get; set; }
        public ICollection<IngredientResponse> Ingredients { get; set; }
    }
}
