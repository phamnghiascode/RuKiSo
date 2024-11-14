using System.ComponentModel.DataAnnotations;

namespace RuKiSo.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public ICollection<Batch> Batches { get; set; }
    }
}
