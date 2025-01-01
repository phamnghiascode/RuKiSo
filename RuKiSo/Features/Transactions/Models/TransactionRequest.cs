using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Models
{
    public class TransactionRequest : BaseModel
    {
        public bool TranType {  get; set; }
        public DateTime TranDate { get; set; }
        public double Value { get; set; }
        public int Quantity { get; set; }
        public int? IngredientId { get; set; }
        public int? ProductId { get; set; }
    }
}
