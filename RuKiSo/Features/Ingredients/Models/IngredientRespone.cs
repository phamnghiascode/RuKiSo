using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Models
{
    public class IngredientRespone : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public double PurchasePrice { get; set; }
        public double TotalValue => PurchasePrice * Quantity;
    }
}
