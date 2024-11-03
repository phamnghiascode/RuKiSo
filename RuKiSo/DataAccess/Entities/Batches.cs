using RuKiSo.Resources;
using System.ComponentModel.DataAnnotations;

namespace RuKiSo.Entities
{
    public class Batches : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BatchStatus Status { get; set; }
        public ICollection<Ingredients> Ingredients { get; set; }
        public Products Product { get; set; }
    }
}
