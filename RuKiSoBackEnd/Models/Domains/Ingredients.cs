using System.ComponentModel.DataAnnotations;

namespace RuKiSoBackEnd.Models.Domains
{
    public class Ingredients
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public double PurchasePrice { get; set; }
        public ICollection<BatchIngredient> BatchIngredients { get; set; } = new List<BatchIngredient>();
        public ICollection<Transactions> Transactions { get; set; } = new List<Transactions>();
    }
}
