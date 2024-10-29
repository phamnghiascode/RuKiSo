using System.ComponentModel.DataAnnotations;

namespace RuKiSo.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
