using RuKiSo.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;

namespace RuKiSo.ViewModels
{
    public class DashBoardViewModel : BaseViewModel
    {
        public ObservableCollection<WeeklyHistoryDTO> WeeklyHistories { get; set; } = new();
        public DashBoardViewModel()
        {
            InitChartData();
        }
        private void InitChartData()
        {
            WeeklyHistories = new ObservableCollection<WeeklyHistoryDTO>()
            {
                new WeeklyHistoryDTO { Purchase = 290, Sell = 300, Date = "Hai" },
                new WeeklyHistoryDTO { Purchase = 380, Sell = 500, Date = "Ba" },
                new WeeklyHistoryDTO { Purchase = 250, Sell = 400, Date = "Tư" },
                new WeeklyHistoryDTO { Purchase = 230, Sell = 400, Date = "Năm" },
                new WeeklyHistoryDTO { Purchase = 350, Sell = 350, Date = "Sáu" },
                new WeeklyHistoryDTO { Purchase = 250, Sell = 480, Date = "Bảy" },
                new WeeklyHistoryDTO { Purchase = 110, Sell = 350, Date = "CN" },
            };
        }
    }
}
