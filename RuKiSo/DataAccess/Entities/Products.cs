using System.ComponentModel.DataAnnotations;

namespace RuKiSo.Entities
{
    public class Products : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public int AlcoholContent { get; set; }
        public decimal Price { get; set; }
        public int BatchId { get; set; }
        public Batches Batch { get; set; }
    }
}
