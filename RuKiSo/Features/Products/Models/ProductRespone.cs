using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Models
{
    public class ProductRespone: BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double TotalValue => Price * Quantity;
    }
}
