namespace RuKiSo.Entities
{
    public class Transaction : BaseEntity
    {
        public string Name { get; set; }
        public bool Type { get; set; }
        public double Value { get; set; }
    }
}
