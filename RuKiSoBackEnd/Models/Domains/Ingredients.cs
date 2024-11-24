namespace RuKiSoBackEnd.Models.Domains
{
    public class Ingredients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public double PurchasePrice { get; set; }
        public Batches Batch { get; set; }
        public int? BatchId { get; set; }
        public ICollection<Transactions> Transactions { get; set; }
    }
}
