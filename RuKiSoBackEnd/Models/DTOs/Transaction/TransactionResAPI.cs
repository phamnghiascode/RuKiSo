namespace RuKiSoBackEnd.Models.DTOs
{
    public class TransactionResAPI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool TranType { get; set; }
        public int Quantity { get; set; }
        public double Value { get; set; }
        public DateTime TranDate { get; set; }
        public int? IngredientId { get; set; }
        public int? ProductId { get; set; }
    }
}
