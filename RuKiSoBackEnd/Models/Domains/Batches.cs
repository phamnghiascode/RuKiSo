namespace RuKiSoBackEnd.Models.Domains
{
    public class Batches
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EstimateEndDate { get; set; }
        public int Yield { get; set; }
        public ICollection<Ingredients> Ingredients { get; set; }
        public Products Product { get; set; }
        public int? ProductId { get; set; }
    }
}
