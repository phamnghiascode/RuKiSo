namespace RuKiSoBackEnd.Models.DTOs
{
    public class ProductRequestAPI
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
