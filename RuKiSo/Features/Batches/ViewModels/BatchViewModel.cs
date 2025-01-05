using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Models;
using RuKiSo.Features.Services;
using RuKiSo.Resources.Text;
using RuKiSo.Utils;
using RuKiSo.Utils.MVVM;
using RuKiSoBackEnd.Models.DTOs;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public partial class BatchViewModel : BaseViewModel
    {
        #region Fields

        private readonly IGenericService<ProductRespone, ProductRequest> _productService;
        private readonly IGenericService<IngredientRespone, IngredientRequest> _ingredientService;
        private readonly IGenericService<BatchResponse, BatchRequest> _batchService;

        private BatchResponse selectedBatch;
        private ProductRespone? selectedProduct;
        private bool isEditCookPopupOpen;
        private int totalBatch;
        private double totalValue;
        private double projectedYield;
        private DateTime startDate = DateTime.Now;
        private DateTime estimateEndDate = DateTime.Now;

        #endregion

        public BatchViewModel(
            IGenericService<ProductRespone, ProductRequest> productService,
            IGenericService<IngredientRespone, IngredientRequest> ingredientService,
            IGenericService<BatchResponse, BatchRequest> batchService,
            BatchReminderViewModel reminderViewModel,
            IErrorHandlingService errorHandlingService) : base(errorHandlingService)
        {
            _productService = productService;
            _ingredientService = ingredientService;
            _batchService = batchService;
            ReminderViewModel = reminderViewModel;

            InitializeCollections();
            InitializeCommands();
        }

        #region Initialization

        private void InitializeCollections()
        {
            Batches = new();
            Ingredients = new();
            AllBatches = new();
            Products = new();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                await Task.WhenAll(
                    LoadProduct(),
                    LoadIngredient(),
                    LoadAllBaches()
                );
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.LOADING_DASHBOARD, ex);
            }
        }

        private async Task LoadProduct()
        {
            try
            {
                var response = await _productService.GetAllAsync();
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
                HandleException(ErrorMessages.LOADING_PRODUCTS, ex);
            }
        }

        private async Task LoadIngredient()
        {
            try
            {
                var response = await _ingredientService.GetAllAsync();
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
                HandleException(ErrorMessages.LOADING_INGREDIENTS, ex);
            }
        }

        private async Task LoadAllBaches()
        {
            try
            {
                var response = await _batchService.GetAllAsync();
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
                HandleException(ErrorMessages.LOADING_BATCHES, ex);
            }
        }

        #endregion

        #region Properties

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

        public ObservableCollection<BatchResponse> Batches { get; set; }
        public ObservableCollection<BatchIngredientDTO> Ingredients { get; set; }
        public ObservableCollection<BatchResponse> AllBatches { get; set;}
        public ObservableCollection<ProductRespone> Products { get; set; }
        public BatchReminderViewModel ReminderViewModel { get; set; }

        #endregion

        #region Commands

        public ICommand AddBatchCommand { get; set; }
        public ICommand EditCookBatchCommand { get; set; }
        public ICommand DeleteBatchCommand { get; set; }
        public ICommand SaveBatchCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        private void InitializeCommands()
        {
            ResetCommand = new RelayCommand(Reset);
            EditCookBatchCommand = new RelayCommand<BatchResponse>(EditCookBatch);
            SaveBatchCommand = new RelayCommand(SaveBatch);
            DeleteBatchCommand = new RelayCommand<BatchResponse>(DeleteBatch);
            AddBatchCommand = new RelayCommand(AddBatch);
        }
        #endregion

        #region Command Handlers

        private async void SaveBatch()
        {
            if (SelectedBatch == null) return;

            try
            {
                var request = CreateBatchRequest(SelectedBatch);
                var updatedBatch = await _batchService.UpdateAsync(SelectedBatch.Id, request);

                if (updatedBatch != null)
                {
                    UpdateBatchInCollection(updatedBatch);
                    IsEditCookPopupOpen = false;
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.UPDATING_BATCH, ex);
            }
        }

        private BatchRequest CreateBatchRequest(BatchResponse batch)

        {
            return new BatchRequest
            {
                ProductId = batch.Product?.Id ?? 0,
                StartDate = batch.StartDate,
                EstimateEndDate = batch.EstimateEndDate,
                Yield = batch.Yield,
                BatchIngredients = batch.Ingredients.Select(i => new BatchIngredientAPIRequest
                {
                    IngredientId = i.Id,
                    Quantity = (int)i.UsedQuantity
                }).ToList()
            };
        }
        private void EditCookBatch(BatchResponse batch)
        {
            if (batch == null) return;

            SelectedBatch = CreateEditBatchCopy(batch);
            IsEditCookPopupOpen = true;
        }

        private BatchResponse CreateEditBatchCopy(BatchResponse batch)
        {
            return new BatchResponse
            {
                Id = batch.Id,
                StartDate = batch.StartDate,
                EstimateEndDate = batch.EstimateEndDate,
                Yield = batch.Yield,
                Product = batch.Product,
                Ingredients = batch.Ingredients.Select(i => new BatchIngredientDTO
                {
                    Id = i.Id,
                    IngredientName = i.IngredientName,
                    StoredQuantity = i.StoredQuantity,
                    UsedQuantity = i.UsedQuantity,
                    PricePerUnit = i.PricePerUnit,
                    IsSelected = true
                }).ToList()
            };
        }

        private async void DeleteBatch(BatchResponse batch)
        {
            if (batch == null) return;

            try
            {
                var success = await _batchService.DeleteAsync(batch.Id);
                if (success)
                {
                    AllBatches.Remove(batch);
                    UpdateBatches();
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.DELETING_BATCH, ex);
            }
        }

        private async void AddBatch()
        {
            var selectedIngredients = GetSelectedIngredients();
            if (!selectedIngredients.Any() || SelectedProduct == null) return;

            try
            {
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

                var newBatch = await _batchService.CreateAsync(request);
                if (newBatch != null)
                {
                    AllBatches.Add(newBatch);
                    UpdateBatches();
                    Reset();
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.CREATING_BATCH, ex);
            }
        }

        private List<BatchIngredientDTO> GetSelectedIngredients()
        {
            return Ingredients.Where(i => i.IsSelected && i.UsedQuantity > 0).ToList();
        }

        #endregion

        #region Helpers

        private void UpdateBatchInCollection(BatchResponse updatedBatch)
        {
            var index = AllBatches.IndexOf(SelectedBatch);
            if (index != -1)
            {
                if (updatedBatch.Yield > 0)
                {
                    AllBatches.RemoveAt(index);
                }
                else
                {
                    AllBatches[index] = updatedBatch;
                }
            }
            UpdateBatches();
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

        private void UpdateCardsData()
        {
            TotalBatch = Batches.Count;
            TotalValue = Batches.Sum(b => b.Value);
            ProjectedYield = CalculateProjectedYield();
        }

        private double CalculateProjectedYield()
        {
            return Batches.Count * 12;
        }

        #endregion
    }
}