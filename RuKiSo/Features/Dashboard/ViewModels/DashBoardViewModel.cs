using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using RuKiSoBackEnd.Models.Domains;
using System.Collections.ObjectModel;

namespace RuKiSo.ViewModels
{
    public class DashBoardViewModel : BaseViewModel
    {
        public ObservableCollection<WeeklyHistoryDTO> WeeklyHistories { get; set; }
        public ObservableCollection<TopSellerDTO> TopSellers { get; set; }
        public ObservableCollection<MostUsedIngredient> MostUsedIngredients { get; set; }
        public ObservableCollection<ProfitDTO> MonthlyProfit { get; set; }
        private readonly IGenericService<ProductRespone, ProductRequest> productService;
        private readonly IGenericService<IngredientRespone, IngredientRequest> ingredientService;
        private readonly IGenericService<TransactionResponse, TransactionRequest> transactionService;
        public DashBoardViewModel(
            IGenericService<ProductRespone, ProductRequest> productService,
            IGenericService<IngredientRespone, IngredientRequest> ingredientService,
            IGenericService<TransactionResponse, TransactionRequest> transactionService)
        {
            this.productService = productService;
            this.ingredientService = ingredientService;
            this.transactionService = transactionService;
            InitChartData();
        }
        private async void InitChartData()
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
            //TopSellers = new ObservableCollection<TopSellerDTO>()
            //{
            //    new() { Name = "Thượng hạng", Quantity = 400 },
            //    new() { Name = "Đòng đòng", Quantity = 210 },
            //    new() { Name = "Thường", Quantity = 200 },
            //    new() { Name = "Khác", Quantity = 100 }
            //};
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
            await GetTopSellerAsync();
        }
        private async Task GetTopSellerAsync()
        {
            try
            {
                // Lấy tất cả giao dịch
                var transactions = await transactionService.GetAllAsync();
                if (transactions == null || !transactions.Any())
                {
                    Console.WriteLine("No transactions found.");
                    TopSellers = new ObservableCollection<TopSellerDTO>();
                    return;
                }

                // Nhóm và tính tổng Quantity theo ProductId
                var topTransactions = transactions
                    .Where(t => t.ProductId.HasValue) // Chỉ lấy các giao dịch có ProductId
                    .GroupBy(t => t.ProductId)
                    .Select(group => new
                    {
                        ProductId = group.Key,
                        TotalQuantity = group.Sum(t => t.Quantity)
                    })
                    .OrderByDescending(t => t.TotalQuantity) // Sắp xếp giảm dần
                    .Take(4) // Lấy 4 sản phẩm có số lượng cao nhất
                    .ToList();

                // Lấy thông tin chi tiết sản phẩm từ ProductService
                var products = await productService.GetAllAsync();
                if (products == null)
                {
                    Console.WriteLine("No products found.");
                    TopSellers = new ObservableCollection<TopSellerDTO>();
                    return;
                }

                // Kết hợp dữ liệu sản phẩm với topTransactions
                var topSellers = topTransactions
                    .Join(products,
                        t => t.ProductId,
                        p => p.Id,
                        (t, p) => new TopSellerDTO
                        {
                            Name = p.Name,
                            Quantity = t.TotalQuantity
                        })
                    .ToList();

                // Cập nhật danh sách TopSellers
                TopSellers = new ObservableCollection<TopSellerDTO>(topSellers);
            }
            catch (Exception ex)
            {
                HandleException("Error retrieving top sellers", ex);
                TopSellers = new ObservableCollection<TopSellerDTO>();
            }
        }
        private async Task<List<TopSellerDTO>> GetTopSeller()
        {
            try
            {
                // Lấy tất cả giao dịch
                var transactions = await transactionService.GetAllAsync();
                if (transactions == null || !transactions.Any())
                {
                    Console.WriteLine("No transactions found.");
                    return new List<TopSellerDTO>();
                }

                // Nhóm và tính tổng Quantity theo ProductId
                var topTransactions = transactions
                    .Where(t => t.ProductId.HasValue) // Chỉ lấy các giao dịch có ProductId
                    .GroupBy(t => t.ProductId)
                    .Select(group => new
                    {
                        ProductId = group.Key,
                        TotalQuantity = group.Sum(t => t.Quantity)
                    })
                    .OrderByDescending(t => t.TotalQuantity) // Sắp xếp giảm dần
                    .Take(4) // Lấy 4 sản phẩm có số lượng cao nhất
                    .ToList();

                // Lấy thông tin chi tiết sản phẩm từ ProductService
                var products = await productService.GetAllAsync();
                if (products == null)
                {
                    Console.WriteLine("No products found.");
                    return new List<TopSellerDTO>();
                }

                // Kết hợp dữ liệu sản phẩm với topTransactions
                var topSellers = topTransactions
                    .Join(products,
                        t => t.ProductId,
                        p => p.Id,
                        (t, p) => new TopSellerDTO
                        {
                            Name = p.Name,
                            Quantity = t.TotalQuantity
                        })
                    .ToList();

                return topSellers;
            }
            catch (Exception ex)
            {
                HandleException("Error retrieving top sellers", ex);
                return new List<TopSellerDTO>();
            }
        }

    }
}
