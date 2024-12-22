using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Models
{
    public class BatchResponse : BaseModel
    {
        public int Id { get; set; }
        public ProductRespone Product { get; set; }
        public List<BatchIngredientDTO> Ingredients { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimateEndDate { get; set; }
        /// <summary>
        /// San luong san pham
        /// </summary>
        public int Yield { get; set; }
        public double Value => Ingredients?.Sum(i => i.Price) ?? 0;
    }
}
