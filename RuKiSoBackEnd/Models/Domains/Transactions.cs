namespace RuKiSoBackEnd.Models.Domains
{
    public class Transactions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool TranType { get; set; }
        public int Quantity { get; set; }
        public double Value { get; set; }
        public DateTime TranDate { get; set; }

        public int? IngredientId { get; set; }
        public Ingredients Ingredient { get; set; }

        public int? ProductId { get; set; }
        public Products Product { get; set; }
    }
}
