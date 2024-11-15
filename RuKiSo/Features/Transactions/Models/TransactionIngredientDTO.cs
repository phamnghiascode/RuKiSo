using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Models
{
    public class TransactionIngredientDTO : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public double PurchasePrice { get; set; }
        public int UsedQuantity { get; set; }
    }
}
