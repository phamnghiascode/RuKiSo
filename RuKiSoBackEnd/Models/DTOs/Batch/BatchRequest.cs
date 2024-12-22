using RuKiSoBackEnd.Models.Domains;

namespace RuKiSoBackEnd.Models.DTOs
{
    public class BatchRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EstimateEndDate { get; set; }
        public int Yield { get; set; }
        public int ProductId { get; set; }
        public List<BatchIngredientRequest> BatchIngredients { get; set; } = new();
    }
}
