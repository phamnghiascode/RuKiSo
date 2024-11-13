using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Batches.Models
{
    public class BatchDTO : BaseModel
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public List<BatchIngredientDTO> Ingredients { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimateEndDate { get; set; }
        public int Yield { get; set; }
        public double Value => Ingredients?.Sum(i => i.Price) ?? 0;
    }
}
