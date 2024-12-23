using RuKiSoBackEnd.Models.DTOs;

namespace RuKiSo.Features.Models
{
    public class BatchRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EstimateEndDate { get; set; }
        public int Yield { get; set; }
        public int ProductId { get; set; }
        public List<BatchIngredientAPIRequest> BatchIngredients { get; set; } = new();
    }
}
