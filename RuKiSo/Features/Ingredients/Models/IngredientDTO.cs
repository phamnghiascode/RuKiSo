using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Models
{
    public class IngredientDTO : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalValue => Price * Quantity;
    }
}
