using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Models
{
    public class BatchIngredientDTO : BaseModel
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public double StoredQuantity { get; set; }
        public double UsedQuantity { get; set; }
        public double PricePerUnit { get; set; }
        public double Price => UsedQuantity * PricePerUnit;
        public bool IsSelected { get; set; }
    }
}
