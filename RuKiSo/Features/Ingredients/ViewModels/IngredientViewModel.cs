using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Models;
using RuKiSo.Features.Services;
using RuKiSo.Resources.Text;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public partial class IngredientViewModel : BaseViewModel
    {
        #region Fields

        private readonly IGenericService<IngredientRespone, IngredientRequest> _ingredientService;

        private IngredientRespone? selectedIngredient;
        private int totalIngredient;
        private double totalValue;
        private double estimateOutput;
        private string name = string.Empty;
        private string unit = string.Empty;
        private int quantity;
        private double purchasePrice;
        private bool isQuantityEnabled;
        private bool isPurchasePriceEnabled;

        #endregion

        public IngredientViewModel(
            IGenericService<IngredientRespone, IngredientRequest> ingredientService,
            BatchReminderViewModel reminderViewModel,
            IErrorHandlingService errorHandlingService) : base(errorHandlingService)
        {
            _ingredientService = ingredientService;
            ReminderViewModel = reminderViewModel;

            InitializeCommands();
            InitializeCollections();
        }

        #region Properties

        public ObservableCollection<IngredientRespone> Ingredients { get; set; }
        public BatchReminderViewModel ReminderViewModel { get; }

        public IngredientRespone? SelectedIngredient
        {
            get => selectedIngredient;
            set
            {
                selectedIngredient = value;
                OnPropertyChanged();
            }
        }

        public bool IsQuantityEnabled
        {
            get => isQuantityEnabled;
            set
            {
                isQuantityEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool IsPurchasePriceEnabled
        {
            get => isPurchasePriceEnabled;
            set
            {
                isPurchasePriceEnabled = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Unit
        {
            get => unit;
            set
            {
                unit = value;
                OnPropertyChanged();
            }
        }

        public double PurchasePrice
        {
            get => purchasePrice;
            set
            {
                purchasePrice = value;
                OnPropertyChanged();
            }
        }

        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged();
            }
        }

        public int TotalIngredient
        {
            get => totalIngredient;
            set
            {
                totalIngredient = value;
                OnPropertyChanged();
            }
        }

        public double TotalValue
        {
            get => totalValue;
            set
            {
                totalValue = value;
                OnPropertyChanged();
            }
        }

        public double EstimatedOutput
        {
            get => estimateOutput;
            set
            {
                estimateOutput = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand UpSertIngredientCommand { get; set; }
        public ICommand DeleteIngredientCommand { get; set; }
        public ICommand EditIngredientCommand { get; set; }
        public ICommand QuantityFilterCommand { get; set; }
        public ICommand PurchasePriceFilterCommand { get; private set; }
        public ICommand ResetCommand { get; set; }

        private void InitializeCommands()
        {
            ResetCommand = new RelayCommand(Reset);
            EditIngredientCommand = new RelayCommand<IngredientRespone>(EditIngredient);
            PurchasePriceFilterCommand = new RelayCommand(FilterByPurchasePrice);
            QuantityFilterCommand = new RelayCommand(FilterByQuantity);
            DeleteIngredientCommand = new RelayCommand<IngredientRespone>(DeleteIngredient);
            UpSertIngredientCommand = new RelayCommand(UpSertIngredient);
        }

        #endregion

        #region Initialization

        private void InitializeCollections()
        {
            Ingredients = new ObservableCollection<IngredientRespone>();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                var response = await _ingredientService.GetAllAsync();
                if (response != null)
                {
                    Ingredients.Clear();
                    foreach (var item in response)
                    {
                        Ingredients.Add(item);
                    }
                    UpdateCardsInfo();
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.LOADING_INGREDIENTS, ex);
            }
        }

        #endregion

        #region Command Handlers

        private void EditIngredient(IngredientRespone? ingredient)
        {
            if (ingredient == null) return;

            IsPurchasePriceEnabled = true;
            IsQuantityEnabled = true;
            SelectedIngredient = ingredient;
            Name = SelectedIngredient.Name;
            Unit = SelectedIngredient.Unit;
            PurchasePrice = SelectedIngredient.PurchasePrice;
            Quantity = SelectedIngredient.Quantity;
        }

        private async void DeleteIngredient(IngredientRespone? ingredient)
        {
            if (ingredient == null || !Ingredients.Contains(ingredient)) return;

            try
            {
                bool isDeleted = await _ingredientService.DeleteAsync(ingredient.Id);
                if (isDeleted)
                {
                    Ingredients.Remove(ingredient);
                    UpdateCardsInfo();
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.DELETING_INGREDIENT, ex);
            }
        }

        private void UpSertIngredient()
        {
            if (!IsIngredientValid())
            {
                HandleException(ErrorMessages.INVALID_INGREDIENT, new Exception("Nguyên liệu không hợp lệ"));
                return;
            }
            if (SelectedIngredient != null)
            {
                UpdateIngredient();
            }
            else
            {
                CreateIngredient();
            }
        }

        private async void CreateIngredient()
        {
            var ingredient = new IngredientRequest
            {
                Name = Name,
                Unit = Unit,
                PurchasePrice = PurchasePrice,
                Quantity = Quantity,
            };

            try
            {
                var response = await _ingredientService.CreateAsync(ingredient);
                if (response != null)
                {
                    Ingredients.Add(response);
                    UpdateCardsInfo();
                    Reset();
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.CREATING_INGREDIENT, ex);
            }
        }

        private async void UpdateIngredient()
        {
            if (SelectedIngredient == null) return;

            var updateIngredient = new IngredientRequest
            {
                Name = Name,
                Unit = Unit,
                PurchasePrice = PurchasePrice,
                Quantity = Quantity
            };

            try
            {
                var response = await _ingredientService.UpdateAsync(SelectedIngredient.Id, updateIngredient);
                if (response != null)
                {
                    var index = Ingredients.IndexOf(SelectedIngredient);
                    if (index >= 0)
                    {
                        Ingredients[index] = response;
                    }
                    UpdateCardsInfo();
                    Reset();
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.UPDATING_INGREDIENT, ex);
            }
        }

        #endregion

        #region Filtering

        private void FilterByQuantity()
        {
            var filteredIngredients = Ingredients.OrderByDescending(p => p.Quantity).ToList();
            UpdateFilteredIngredients(filteredIngredients);
        }

        private void FilterByPurchasePrice()
        {
            var filteredIngredients = Ingredients.OrderByDescending(p => p.PurchasePrice).ToList();
            UpdateFilteredIngredients(filteredIngredients);
        }

        private void UpdateFilteredIngredients(IEnumerable<IngredientRespone> filteredIngredients)
        {
            Ingredients.Clear();
            foreach (var item in filteredIngredients)
            {
                Ingredients.Add(item);
            }
        }

        #endregion

        #region Helpers
        private bool IsIngredientValid()
        {
            return !(string.IsNullOrWhiteSpace(Name)
                    || string.IsNullOrWhiteSpace(Unit)
                    || Name.Length > 30
                    || Unit.Length > 30
                    || PurchasePrice < 0
                    || Quantity < 0);
        }
        private void UpdateCardsInfo()
        {
            TotalIngredient = Ingredients.Count;
            TotalValue = Ingredients.Sum(i => i.TotalValue);
            EstimatedOutput = Math.Floor(TotalValue);
        }

        private void Reset()
        {
            IsPurchasePriceEnabled = false;
            IsQuantityEnabled = false;
            SelectedIngredient = null;
            Name = string.Empty;
            Unit = string.Empty;
            Quantity = 0;
            PurchasePrice = 0;
        }

        #endregion
    }
}