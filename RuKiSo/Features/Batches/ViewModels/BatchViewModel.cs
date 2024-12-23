using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Models;
using RuKiSo.Utils;
using RuKiSo.Utils.MVVM;
using RuKiSoBackEnd.Models.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public partial class BatchViewModel : BaseViewModel
    {
        private BatchResponse selectedBatch;
        private ProductRespone? selectedProduct;
        private bool isEditCookPopupOpen;
        private int totalBatch;
        private double totalValue;
        private double projectedYield;
        private DateTime startDate = DateTime.Now;
        private DateTime estimateEndDate = DateTime.Now;

        private readonly IGenericService<ProductRespone, Features.Models.ProductRequest> productService;
        private readonly IGenericService<IngredientRespone, Features.Models.IngredientRequest> ingredientService;
        private readonly IGenericService<BatchResponse, BatchRequest> batchService;

        public BatchViewModel(
            IGenericService<ProductRespone, Features.Models.ProductRequest> productService,
            IGenericService<IngredientRespone, Features.Models.IngredientRequest> ingredientService,
            IGenericService<BatchResponse, BatchRequest> batchService)
        {
            this.productService = productService;
            this.ingredientService = ingredientService;
            this.batchService = batchService;

            Batches = new();
            Ingredients = new();
            AllBatches = new();
            Products = new();

            ResetCommand = new RelayCommand(Reset);
            EditCookBatchCommand = new RelayCommand<BatchResponse>(EditCookBatch);
            SaveBatchCommand = new RelayCommand(SaveBatch);
            DeleteBatchCommand = new RelayCommand<BatchResponse>(DeleteBatch);
            AddBatchCommand = new RelayCommand(AddBatch);

            InitializeData();
        }

        public BatchResponse SelectedBatch
        {
            get => selectedBatch;
            set
            {
                selectedBatch = value;
                OnPropertyChanged(nameof(SelectedBatch));
            }
        }

        public ProductRespone? SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EstimateEndDate
        {
            get => estimateEndDate;
            set
            {
                estimateEndDate = value;
                OnPropertyChanged(nameof(EstimateEndDate));
            }
        }

        public bool IsEditCookPopupOpen
        {
            get => isEditCookPopupOpen;
            set
            {
                isEditCookPopupOpen = value;
                OnPropertyChanged(nameof(IsEditCookPopupOpen));
            }
        }

        public int TotalBatch
        {
            get => totalBatch;
            set
            {
                totalBatch = value;
                OnPropertyChanged(nameof(TotalBatch));
            }
        }

        public double TotalValue
        {
            get => totalValue;
            set
            {
                totalValue = value;
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public double ProjectedYield
        {
            get => projectedYield;
            set
            {
                projectedYield = value;
                OnPropertyChanged(nameof(ProjectedYield));
            }
        }

        public ICommand AddBatchCommand { get; }
        public ICommand EditCookBatchCommand { get; }
        public ICommand DeleteBatchCommand { get; }
        public ICommand SaveBatchCommand { get; }
        public ICommand ResetCommand { get; }

        public ObservableCollection<BatchResponse> Batches { get; }
        public ObservableCollection<BatchIngredientDTO> Ingredients { get; }
        public ObservableCollection<BatchResponse> AllBatches { get; }
        public ObservableCollection<ProductRespone> Products { get; }

        private async void InitializeData()
        {
            await LoadProduct();
            await LoadIngredient();
            await LoadAllBaches();
        }

        private async Task LoadProduct()
        {
            try
            {
                var response = await productService.GetAllAsync();
                if (response != null)
                {
                    Products.Clear();
                    foreach (var item in response)
                    {
                        Products.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException("Error loading products", ex);
            }
        }

        private async Task LoadIngredient()
        {
            try
            {
                var response = await ingredientService.GetAllAsync();
                if (response != null)
                {
                    Ingredients.Clear();
                    foreach (var item in response)
                    {
                        Ingredients.Add(item.ToBatchIngredientDTO());
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException("Error loading ingredients", ex);
            }
        }

        private async Task LoadAllBaches()
        {
            try
            {
                var response = await batchService.GetAllAsync();
                if (response != null)
                {
                    AllBatches.Clear();
                    foreach (var item in response)
                    {
                        AllBatches.Add(item);
                    }
                }
                UpdateBatches();
            }
            catch (Exception ex)
            {
                HandleException("Error loading batches", ex);
            }
        }

        private void Reset()
        {
            SelectedProduct = null;
            StartDate = DateTime.Now;
            EstimateEndDate = DateTime.Now;
            foreach (var ingredient in Ingredients)
            {
                ingredient.IsSelected = false;
                ingredient.UsedQuantity = 0;
            }
            OnPropertyChanged(nameof(Ingredients));
        }

        private async void SaveBatch()
        {
            if (SelectedBatch == null) return;

            var request = new BatchRequest
            {
                ProductId = SelectedBatch.Product?.Id ?? 0,
                StartDate = SelectedBatch.StartDate,
                EstimateEndDate = SelectedBatch.EstimateEndDate,
                BatchIngredients = SelectedBatch.Ingredients.Select(i => new BatchIngredientAPIRequest
                {
                    IngredientId = i.Id,
                    Quantity = (int)i.UsedQuantity
                }).ToList()
            };

            try
            {
                var updatedBatch = await batchService.UpdateAsync(SelectedBatch.Id, request);
                if (updatedBatch != null)
                {
                    var index = AllBatches.IndexOf(SelectedBatch);
                    if (index != -1)
                    {
                        AllBatches[index] = updatedBatch;
                    }
                    UpdateBatches();
                    IsEditCookPopupOpen = false;
                }
            }
            catch (Exception ex)
            {
                HandleException("Error updating batch", ex);
            }
        }

        private void EditCookBatch(BatchResponse batch)
        {
            if (batch == null) return;
            SelectedBatch = batch;
            IsEditCookPopupOpen = true;
        }

        private async void DeleteBatch(BatchResponse batch)
        {
            if (batch == null) return;

            try
            {
                var success = await batchService.DeleteAsync(batch.Id);
                if (success)
                {
                    AllBatches.Remove(batch);
                    UpdateBatches();
                }
            }
            catch (Exception ex)
            {
                HandleException("Error deleting batch", ex);
            }
        }

        private List<BatchIngredientDTO> GetSelectedIngredients()
        {
            return Ingredients.Where(i => i.IsSelected && i.UsedQuantity > 0).ToList();
        }

        private async void AddBatch()
        {
            var selectedIngredients = GetSelectedIngredients();
            if (!selectedIngredients.Any() || SelectedProduct == null) return;

            var request = new BatchRequest
            {
                ProductId = SelectedProduct.Id,
                StartDate = StartDate,
                EstimateEndDate = EstimateEndDate,
                BatchIngredients = selectedIngredients.Select(i => new BatchIngredientAPIRequest
                {
                    IngredientId = i.Id,
                    Quantity = (int)i.UsedQuantity
                }).ToList()
            };

            try
            {
                var newBatch = await batchService.CreateAsync(request);
                if (newBatch != null)
                {
                    AllBatches.Add(newBatch);
                    UpdateBatches();
                    Reset();
                }
            }
            catch (Exception ex)
            {
                HandleException("Error creating batch", ex);
            }
        }

        private void UpdateBatches()
        {
            Batches.Clear();
            foreach (var batch in AllBatches.Where(b => b.Yield == 0))
            {
                Batches.Add(batch);
            }
            UpdateCardsData();
        }

        private void UpdateCardsData()
        {
            TotalBatch = Batches.Count;
            TotalValue = Batches.Sum(b => b.Value);
            ProjectedYield = CalculateProjectedYield();
        }

        private double CalculateProjectedYield()
        {
            // Implementation of yield calculation logic
            return 100; // Placeholder value
        }

        private void HandleException(string message, Exception ex)
        {
            // Implement your error handling logic here
            Console.WriteLine($"{message}: {ex.Message}");
        }
    }
}