using System.ComponentModel.DataAnnotations;

namespace RuKiSo.Entities
{
    public class Ingredients : BaseEntity
    {
        [Required]
        public  string Name { get; set; }
        public string Image { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public int BatchId { get; set; }
        public Batches Batch { get; set; }
    }
}
