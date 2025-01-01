namespace RuKiSo.Features.Transactions.Models
{
    public class CreateTransactionDTO
    {
        public bool TranType { get; set; }
        public int Quantity { get; set; }
        public double Value { get; set; }
        public DateTime? TranDate { get; set; }
        public int? IngredientId { get; set; }
        public int? ProductId { get; set; }
    }
}
