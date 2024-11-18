﻿using CommunityToolkit.Maui.Core;
using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;

namespace RuKiSo.ViewModels
{
    public class DashBoardViewModel : BaseViewModel
    {
        public ObservableCollection<WeeklyHistoryDTO> WeeklyHistories { get; set; }
        public ObservableCollection<TopSellerDTO> TopSellers { get; set; }
        public ObservableCollection<MostUsedIngredient> MostUsedIngredients { get; set; }
        public ObservableCollection<ProfitDTO> MonthlyProfit { get; set; }
        public DashBoardViewModel(IPopupService popupService) : base(popupService)
        {
            InitChartData();
        }
        private void InitChartData()
        {
            WeeklyHistories = new ObservableCollection<WeeklyHistoryDTO>()
            {
                new() { Purchase = 290, Sell = 300, Date = "Hai" },
                new() { Purchase = 380, Sell = 500, Date = "Ba" },
                new() { Purchase = 250, Sell = 400, Date = "Tư" },
                new() { Purchase = 230, Sell = 400, Date = "Năm" },
                new() { Purchase = 350, Sell = 350, Date = "Sáu" },
                new() { Purchase = 250, Sell = 480, Date = "Bảy" },
                new() { Purchase = 110, Sell = 350, Date = "CN" },
            };
            TopSellers = new ObservableCollection<TopSellerDTO>()
            {
                new() { Name = "Thượng hạng", Quantity = 400 },
                new() { Name = "Đòng đòng", Quantity = 210 },
                new() { Name = "Thường", Quantity = 200 },
                new() { Name = "Khác", Quantity = 100 }
            };
            MostUsedIngredients = new ObservableCollection<MostUsedIngredient>
            {
                new(){ Name = "Nếp đen", Quantity = 100},
                new(){ Name = "Nếp cái hoa vàng", Quantity = 80},
                new(){ Name = "Nếp thường", Quantity = 80},
                new(){ Name = "Đòng đòng", Quantity = 80},
                new(){ Name = "Men thuốc bắc", Quantity = 100},
                new(){ Name = "Men lá", Quantity = 80},
            };
            MonthlyProfit = new ObservableCollection<ProfitDTO>
                {
                    new ProfitDTO{Date = new DateTime(2024,3,31), Profit = 18.3},
                    new ProfitDTO{Date = new DateTime(2024,4,30), Profit = 14.2},
                    new ProfitDTO{Date = new DateTime(2024,5,31), Profit = 16.7},
                    new ProfitDTO{Date = new DateTime(2024,6,30), Profit = 10.9},
                    new ProfitDTO{Date = new DateTime(2024,7,31), Profit = 13.4},
                    new ProfitDTO{Date = new DateTime(2024,8,31), Profit = 17.6},
                    new ProfitDTO{Date = new DateTime(2024,9,30), Profit = 19.2},
                    new ProfitDTO{Date = new DateTime(2024,10,31), Profit = 15.5},
                    new ProfitDTO{Date = new DateTime(2024,11,30), Profit = 12.3},
                    new ProfitDTO{Date = new DateTime(2024,12,31), Profit = 18.7},
                };
        }
    }
}
