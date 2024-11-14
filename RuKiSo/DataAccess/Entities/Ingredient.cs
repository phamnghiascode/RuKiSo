using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuKiSo.Entities
{
    public class Ingredient : BaseEntity
    {
        [Required]
        public  string Name { get; set; }
        public string Image { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public Batch Batch { get; set; }

        [ForeignKey(nameof(Batch))]
        public int BatchId { get; set; }
    }
}
