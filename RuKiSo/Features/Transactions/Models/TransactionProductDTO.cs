using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Models
{
    public class TransactionProductDTO : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int UsedQuantity { get; set; }
    }
}
