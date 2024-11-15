using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Models
{
    public class TransactionDTO : BaseModel
    {
        public string Name { get; set; }
        public bool TranType {  get; set; }
        public DateTime TranDate { get; set; }
        public double Value { get; set; }
        public int Quantity { get; set; }
    }
}
