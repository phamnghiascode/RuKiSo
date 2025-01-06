using RuKiSo.Features.Models;
using RuKiSo.Features.Services;
using RuKiSo.Resources.Text;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;

namespace RuKiSo.ViewModels
{
    public partial class DashBoardViewModel : BaseViewModel
    {
          
        #region Fields

        private readonly IGenericService<ProductRespone, ProductRequest> _productService;
        private readonly IGenericService<IngredientRespone, IngredientRequest> _ingredientService;
        private readonly IGenericService<TransactionResponse, TransactionRequest> _transactionService;
        private readonly IGenericService<BatchResponse, BatchRequest> _batchService;

        private ObservableCollection<WeeklyHistoryDTO> weeklyHistories;
        private ObservableCollection<TopSellerDTO> topSellers;
        private ObservableCollection<MostUsedIngredient> mostUsedIngredients;
        private ObservableCollection<ProfitDTO> monthlyProfit;

        #endregion

        public DashBoardViewModel(
            IGenericService<ProductRespone, ProductRequest> productService,
            IGenericService<IngredientRespone, IngredientRequest> ingredientService,
            IGenericService<TransactionResponse, TransactionRequest> transactionService,
            IGenericService<BatchResponse, BatchRequest> batchService,
            BatchReminderViewModel reminderViewModel,
            IErrorHandlingService errorHandlingService) : base(errorHandlingService)
        {
            _productService = productService;
            _ingredientService = ingredientService;
            _transactionService = transactionService;
            _batchService = batchService;
            ReminderViewModel = reminderViewModel;

            InitializeCollections();
        }

        #region Properties

        private void InitializeCollections()
        {
            WeeklyHistories = new();
            TopSellers = new();
            MostUsedIngredients = new();
            MonthlyProfit = new();
        }

        public ObservableCollection<WeeklyHistoryDTO> WeeklyHistories
        {
            get => weeklyHistories;
            set
            {
                weeklyHistories = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TopSellerDTO> TopSellers
        {
            get => topSellers;
            set
            {
                topSellers = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MostUsedIngredient> MostUsedIngredients
        {
            get => mostUsedIngredients;
            set
            {
                mostUsedIngredients = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProfitDTO> MonthlyProfit
        {
            get => monthlyProfit;
            set
            {
                monthlyProfit = value;
                OnPropertyChanged();
            }
        }

        public BatchReminderViewModel ReminderViewModel { get; }

        #endregion

        protected override async Task LoadDataAsync()
        {
            try
            {
                await Task.WhenAll(
                    LoadWeeklyHistoryAsync(),
                    LoadTopSellersAsync(),
                    LoadMostUsedIngredientsAsync(),
                    LoadMonthlyProfitAsync()
                );
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.LOADING_DASHBOARD, ex);
            }
        }

        private async Task LoadWeeklyHistoryAsync()
        {
            try
            {
                var transactions = await _transactionService.GetAllAsync();
                if (transactions == null) return;

                var endDate = DateTime.Today;
                var startDate = endDate.AddDays(-6);

                var weeklyData = transactions
                    .Where(t => t.TranDate >= startDate && t.TranDate <= endDate)
                    .GroupBy(t => new {
                        Date = t.TranDate.ToString("ddd"),
                        t.TranType
                    })
                    .Select(g => new WeeklyTransactionData
                    {
                        Date = g.Key.Date,
                        TranType = g.Key.TranType,
                        Total = g.Sum(t => t.Quantity)
                    })
                    .ToList();

                var histories = GenerateWeeklyHistories(weeklyData);
                WeeklyHistories = new(histories);
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.WEEKLY_HISTORY, ex);
            }
        }

        private static List<WeeklyHistoryDTO> GenerateWeeklyHistories(List<WeeklyTransactionData> weeklyData)
        {
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

            return histories;
        }

        private async Task LoadTopSellersAsync()
        {
            try
            {
                var transactions = await _transactionService.GetAllAsync();
                var products = await _productService.GetAllAsync();

                if (transactions == null || products == null) return;

                var topProducts = GetTopSellingProducts(transactions, products);
                TopSellers = new(topProducts);
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.TOP_SELLERS, ex);
            }
        }

        private async Task LoadMostUsedIngredientsAsync()
        {
            try
            {
                var batches = await _batchService.GetAllAsync();
                var ingredients = await _ingredientService.GetAllAsync();

                if (batches == null || ingredients == null) return;

                var topIngredients = GetMostUsedIngredients(batches);
                MostUsedIngredients = new(topIngredients);
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.MOST_USED, ex);
            }
        }

        private async Task LoadMonthlyProfitAsync()
        {
            try
            {
                var transactions = await _transactionService.GetAllAsync();
                if (transactions == null) return;

                var monthlyData = CalculateMonthlyProfit(transactions);
                MonthlyProfit = new(monthlyData);
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.MONTHLY_PROFIT, ex);
            }
        }

        private static List<TopSellerDTO> GetTopSellingProducts(
            IEnumerable<TransactionResponse> transactions,
            IEnumerable<ProductRespone> products)
        {
            return transactions
                .Where(t => t.TranType && t.ProductId.HasValue)
                .GroupBy(t => t.ProductId)
                .Select(g => new { ProductId = g.Key, TotalQuantity = g.Sum(t => t.Quantity) })
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
        }

        private static List<MostUsedIngredient> GetMostUsedIngredients(IEnumerable<BatchResponse> batches)
        {
            return batches
                .SelectMany(b => b.Ingredients)
                .GroupBy(bi => bi.IngredientName)
                .Select(g => new
                {
                    IngredientName = g.Key,
                    TotalQuantity = g.Sum(bi => bi.UsedQuantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(5)
                .Select(x => new MostUsedIngredient
                {
                    Name = x.IngredientName,
                    Quantity = (int)x.TotalQuantity
                })
                .ToList();
        }

        private static List<ProfitDTO> CalculateMonthlyProfit(IEnumerable<TransactionResponse> transactions)
        {
            return transactions
                .GroupBy(t => new DateTime(t.TranDate.Year, t.TranDate.Month, 1))
                .Select(g => new ProfitDTO
                {
                    Date = g.Key,
                    Profit = g.Where(t => t.TranType).Sum(t => t.Value * 0.20)
                })
                .OrderBy(x => x.Date)
                .Take(10)
                .ToList();
        }
    }
}