using System.ComponentModel.DataAnnotations;

namespace RuKiSoBackEnd.Models.Domains
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public ICollection<Batches>? Batches { get; set; }
        public ICollection<Transactions>? Transactions { get; set; }
    }
}
