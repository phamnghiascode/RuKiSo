using RuKiSo.Utils.MVVM;

namespace RuKiSo.Models
{
    public class WeeklyHistoryDTO : BaseModel
    {
        public double Purchase { get; set; } = 0;
        public double Sell { get; set; } = 0;
        public string? Date { get; set; }
    }
}
