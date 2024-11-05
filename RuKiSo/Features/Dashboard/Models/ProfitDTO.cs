using RuKiSo.Utils.MVVM;

namespace RuKiSo.Features.Models
{
    public class ProfitDTO : BaseModel
    {
        public double Profit { get; set; }
        public DateTime Date { get; set; }
    }
}
