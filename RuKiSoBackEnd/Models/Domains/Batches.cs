using System.ComponentModel.DataAnnotations;

namespace RuKiSoBackEnd.Models.Domains
{
    public class Batches
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimateEndDate { get; set; }
        public int Yield { get; set; }
        public Products Product { get; set; }
        public int? ProductId { get; set; }
        public ICollection<BatchIngredient> BatchIngredients { get; set; } = new List<BatchIngredient>();
    }
}
