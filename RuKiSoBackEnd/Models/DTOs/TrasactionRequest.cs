namespace RuKiSoBackEnd.Models.DTOs
{
    public class TransactionRequest
    {
        public string Name { get; set; }
        public bool TranType { get; set; }
        public DateTime TranDate { get; set; }
        public double Value { get; set; }
        public int Quantity { get; set; }
    }
}
