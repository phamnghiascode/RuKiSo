using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Models;
using RuKiSo.Utils;
using RuKiSo.Utils.MVVM;
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
        private DateTime startDate;
        private DateTime estimateEndDate;
        public ICommand AddBatchCommand { get; }
        public ICommand EditCookBatchCommand { get; }
        public ICommand DeleteBatchCommand { get; }
        public ICommand SaveBatchCommand { get; }
        public ICommand ResetCommand { get; }

        public BatchResponse SelectedBatch
        {
            get { return selectedBatch; }
            set
            {
                selectedBatch = value;
                OnPropertyChanged(nameof(SelectedBatch));
            }
        }
        public ProductRespone? SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime EstimateEndDate
        {
            get { return estimateEndDate; }
            set
            {
                estimateEndDate = value;
                OnPropertyChanged(nameof(EstimateEndDate));
            }
        }

        public bool IsEditCookPopupOpen
        {
            get { return isEditCookPopupOpen; }
            set
            {
                isEditCookPopupOpen = value;
                OnPropertyChanged(nameof(IsEditCookPopupOpen));
            }
        }
        public int TotalBatch
        {
            get { return totalBatch; }
            set
            {
                totalBatch = value;
                OnPropertyChanged(nameof(TotalBatch));
            }
        }

        public double TotalValue
        {
            get { return totalValue; }
            set
            {
                totalValue = value;
                OnPropertyChanged(nameof(TotalValue));
            }
        }

        public double ProjectedYield
        {
            get { return projectedYield; }
            set
            {
                projectedYield = value;
                OnPropertyChanged(nameof(ProjectedYield));
            }
        }
        public ObservableCollection<BatchResponse> Batches { get; set; }
        public ObservableCollection<BatchIngredientDTO> Ingredients { get; set; }
        public ObservableCollection<BatchResponse> AllBatches { get; set; }
        public ObservableCollection<ProductRespone> Products { get; set; }

        private readonly IGenericService<ProductRespone, ProductRequest> productService;
        private readonly IGenericService<IngredientRespone, IngredientRequest> ingredientService;
        private readonly IGenericService<BatchResponse, BatchRequest> batchService;

        public BatchViewModel(IGenericService<ProductRespone, ProductRequest> productService,
                              IGenericService<IngredientRespone, IngredientRequest> ingredientService,
                              IGenericService<BatchResponse, BatchRequest> batchService)
        {
            this.productService = productService;
            this.ingredientService = ingredientService;
            this.batchService = batchService;
            ResetCommand = new RelayCommand(Reset);
            EditCookBatchCommand = new RelayCommand<BatchResponse>(EditCookBatch);
            SaveBatchCommand = new RelayCommand(SaveBatch);
            DeleteBatchCommand = new RelayCommand<BatchResponse>(DeleteBatch);
            AddBatchCommand = new RelayCommand(AddBatch);
            InitializeData();
        }
        private void Reset()
        {
            SelectedProduct = null;
            StartDate = DateTime.Now;
            EstimateEndDate = DateTime.Now;
            foreach (var ingredient in Ingredients)
            {
                ingredient.IsSelected = false;
            }
            OnPropertyChanged(nameof(Ingredients));
        }

        private void SaveBatch()
        {
            var updateBatch = Batches.FirstOrDefault(p => p.Id == SelectedBatch.Id);
            if (updateBatch != null)
            {
                updateBatch.Product = SelectedBatch.Product;
                updateBatch.StartDate = SelectedBatch.StartDate;
                updateBatch.EstimateEndDate = SelectedBatch.EstimateEndDate;
                updateBatch.Ingredients = SelectedBatch.Ingredients;
                updateBatch.Yield = SelectedBatch.Yield;
                UpdateBatches();
                IsEditCookPopupOpen = false;
            }
        }

        private void EditCookBatch(BatchResponse batch)
        {
            SelectedBatch = batch;
            IsEditCookPopupOpen = true;
        }

        private void DeleteBatch(BatchResponse batch)
        {
            if (batch != null && Batches.Contains(batch))
            {
                Batches.Remove(batch);
                UpdateCardsData();
            }
        }

        public List<BatchIngredientDTO> GetSelectedIngredients()
        {
            return Ingredients.Where(ingredient => ingredient.IsSelected && ingredient.UsedQuantity > 0).ToList();
        }

        private void AddBatch()
        {
            var selectedIngredients = GetSelectedIngredients();

            var newBatch = new BatchResponse
            {
                Product = SelectedProduct,
                StartDate = StartDate,
                EstimateEndDate = EstimateEndDate,
                Ingredients = selectedIngredients
            };

            Batches.Add(newBatch);
            UpdateCardsData();
        }

        private async void InitializeData()
        {
            AllBatches = new();
            Batches = new();
            Ingredients = new();
            Batches = new();
            Products = new();

            LoadIngredient();
            LoadProduct();
            LoadAllBaches();
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
                HandleException("Error retrieving data", ex);
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
                HandleException("Error retrieving data", ex);
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
            }
            catch (Exception ex)
            {
                HandleException("Error retrieving data", ex);
            }
            UpdateBatches();
        }
        private void UpdateBatches()
        {
            Batches.Clear();
            foreach (BatchResponse batch in AllBatches.Where(b => b.Yield == 0))
            {
                Batches.Add(batch);
            }
            UpdateCardsData();
        }

        private void UpdateCardsData()
        {
            TotalBatch = Batches.Count;
            TotalValue = Batches.Sum(batch => batch.Value);
            ProjectedYield = 100;
        }
        private void HandleException(string message, Exception ex)
        {
            Console.WriteLine($"{message}: {ex.Message}");
        }
    }
}
