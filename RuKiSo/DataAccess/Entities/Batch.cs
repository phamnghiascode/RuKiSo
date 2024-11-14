using RuKiSo.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RuKiSo.Entities
{
    public class Batch : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimateEndDate { get; set; }
        public BatchStatus Status { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
