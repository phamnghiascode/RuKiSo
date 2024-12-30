using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;

namespace RuKiSo.ViewModels
{
    public class DashBoardViewModel : BaseViewModel
    {
        private ObservableCollection<WeeklyHistoryDTO> _weeklyHistories;
        public ObservableCollection<WeeklyHistoryDTO> WeeklyHistories
        {
            get => _weeklyHistories;
            set
            {
                _weeklyHistories = value;
                OnPropertyChanged(nameof(WeeklyHistories));
            }
        }

        private ObservableCollection<TopSellerDTO> _topSellers;
        public ObservableCollection<TopSellerDTO> TopSellers
        {
            get => _topSellers;
            set
            {
                _topSellers = value;
                OnPropertyChanged(nameof(TopSellers));
            }
        }

        private ObservableCollection<MostUsedIngredient> _mostUsedIngredients;
        public ObservableCollection<MostUsedIngredient> MostUsedIngredients
        {
            get => _mostUsedIngredients;
            set
            {
                _mostUsedIngredients = value;
                OnPropertyChanged(nameof(MostUsedIngredients));
            }
        }

        private ObservableCollection<ProfitDTO> _monthlyProfit;
        public ObservableCollection<ProfitDTO> MonthlyProfit
        {
            get => _monthlyProfit;
            set
            {
                _monthlyProfit = value;
                OnPropertyChanged(nameof(MonthlyProfit));
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private readonly IGenericService<ProductRespone, ProductRequest> productService;
        private readonly IGenericService<IngredientRespone, IngredientRequest> ingredientService;
        private readonly IGenericService<TransactionResponse, TransactionRequest> transactionService;
        private readonly IGenericService<BatchResponse, BatchRequest> batchService;

        public DashBoardViewModel(
            IGenericService<ProductRespone, ProductRequest> productService,
            IGenericService<IngredientRespone, IngredientRequest> ingredientService,
            IGenericService<TransactionResponse, TransactionRequest> transactionService,
            IGenericService<BatchResponse, BatchRequest> batchService)
        {
            this.productService = productService;
            this.ingredientService = ingredientService;
            this.transactionService = transactionService;
            this.batchService = batchService;

            // Initialize collections
            WeeklyHistories = new ObservableCollection<WeeklyHistoryDTO>();
            TopSellers = new ObservableCollection<TopSellerDTO>();
            MostUsedIngredients = new ObservableCollection<MostUsedIngredient>();
            MonthlyProfit = new ObservableCollection<ProfitDTO>();

            // Load data
            LoadDataAsync();
        }
        private async Task InitChartDataAsync()
        {
            try
            {
                await Task.WhenAll(
                    GetWeeklyHistoryAsync(),
                    GetTopSellersAsync(),
                    GetMostUsedIngredientsAsync(),
                    GetMonthlyProfitAsync()
                );
            }
            catch (Exception ex)
            {
                // Handle error appropriately
                Console.WriteLine($"Error initializing dashboard data: {ex.Message}");
            }
        }

        private async void LoadDataAsync()
        {
            try
            {
                IsLoading = true;
                await InitChartDataAsync();

                // Add debug logs
                Console.WriteLine($"WeeklyHistories count: {WeeklyHistories?.Count}");
                Console.WriteLine($"TopSellers count: {TopSellers?.Count}");
                Console.WriteLine($"MostUsedIngredients count: {MostUsedIngredients?.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task GetWeeklyHistoryAsync()
        {
            try
            {
                var transactions = await transactionService.GetAllAsync();
                if (transactions == null) return;

                var endDate = DateTime.Today;
                var startDate = endDate.AddDays(-6);

                var weeklyData = transactions
                    .Where(t => t.TranDate >= startDate && t.TranDate <= endDate)
                    .GroupBy(t => new {
                        Date = t.TranDate.ToString("ddd"),
                        t.TranType
                    })
                    .Select(g => new {
                        g.Key.Date,
                        g.Key.TranType,
                        Total = g.Sum(t => t.Quantity)
                    })
                    .ToList();

                var histories = new List<WeeklyHistoryDTO>();
                var daysOfWeek = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

                foreach (var day in daysOfWeek)
                {
                    var sells = weeklyData.FirstOrDefault(x => x.Date == day && x.TranType)?.Total ?? 0;
                    var purchases = weeklyData.FirstOrDefault(x => x.Date == day && !x.TranType)?.Total ?? 0;

                    histories.Add(new WeeklyHistoryDTO
                    {
                        Date = day,
                        Sell = sells,
                        Purchase = purchases
                    });
                }

                WeeklyHistories = new ObservableCollection<WeeklyHistoryDTO>(histories);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting weekly history: {ex.Message}");
            }
        }

        private async Task GetTopSellersAsync()
        {
            try
            {
                var transactions = await transactionService.GetAllAsync();
                var products = await productService.GetAllAsync();

                if (transactions == null || products == null) return;

                var topProducts = transactions
                    .Where(t => t.TranType && t.ProductId.HasValue)
                    .GroupBy(t => t.ProductId)
                    .Select(g => new {
                        ProductId = g.Key,
                        TotalQuantity = g.Sum(t => t.Quantity)
                    })
                    .OrderByDescending(x => x.TotalQuantity)
                    .Take(3)
                    .Join(
                        products,
                        t => t.ProductId,
                        p => p.Id,
                        (t, p) => new TopSellerDTO
                        {
                            Name = p.Name,
                            Quantity = t.TotalQuantity
                        }
                    )
                    .ToList();

                TopSellers = new ObservableCollection<TopSellerDTO>(topProducts);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting top sellers: {ex.Message}");
            }
        }

        private async Task GetMostUsedIngredientsAsync()
        {
            try
            {
                var batches = await batchService.GetAllAsync();
                var ingredients = await ingredientService.GetAllAsync();

                if (batches == null || ingredients == null) return;

                var topIngredients = batches
                    .SelectMany(b => b.Ingredients)
                    .GroupBy(bi => bi.IngredientName)  // Group by name since we don't have IngredientId
                    .Select(g => new {
                        IngredientName = g.Key,
                        TotalQuantity = g.Sum(bi => bi.UsedQuantity)  // Use UsedQuantity instead of Quantity
                    })
                    .OrderByDescending(x => x.TotalQuantity)
                    .Take(5)
                    .Select(x => new MostUsedIngredient
                    {
                        Name = x.IngredientName,
                        Quantity = (int)x.TotalQuantity
                    })
                    .ToList();

                MostUsedIngredients = new ObservableCollection<MostUsedIngredient>(topIngredients);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting most used ingredients: {ex.Message}");
            }
        }

        private async Task GetMonthlyProfitAsync()
        {
            try
            {
                var transactions = await transactionService.GetAllAsync();
                if (transactions == null) return;

                var monthlyData = transactions
                    .GroupBy(t => new DateTime(t.TranDate.Year, t.TranDate.Month, 1))
                    .Select(g => new ProfitDTO
                    {
                        Date = g.Key,
                        Profit = g.Sum(t => t.TranType
                            ? t.Value * 0.20  // 20% profit from sales
                            : -t.Value        // Subtract purchase costs
                        )
                    })
                    .OrderBy(x => x.Date)
                    .Take(10)  // Get last 10 months
                    .ToList();

                MonthlyProfit = new ObservableCollection<ProfitDTO>(monthlyData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating monthly profit: {ex.Message}");
            }
        }
    }
}