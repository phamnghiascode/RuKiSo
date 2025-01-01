using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public class IngredientViewModel : BaseViewModel
    {
        private IngredientRespone? selectedIngredient;
        private int totalIngredient;
        private double totalValue;
        private double estimateOutput;
        private string name;
        private string unit;
        private int quantity;
        private double purchasePrice;
        private bool isQuantityEnabled;
        private bool isPurchasePriceEnabled;
        private readonly IGenericService<IngredientRespone, IngredientRequest> ingredientService;

        public ICommand UpSertIngredientCommand { get; set; }
        public ICommand OpenClosePopupCommand { get; set; }
        public ICommand DeleteIngredientCommand { get; set; }
        public ICommand EditIngredientCommand { get; set; }
        public ICommand QuantityFilterCommand { get; set; }
        public ICommand PurchasePriceFilterCommand { get; set; }
        public ICommand ResetCommand { get; set; }

        public bool IsQuantityEnabled
        {
            get { return isQuantityEnabled; }
            set
            {
                isQuantityEnabled = value;
                OnPropertyChanged(nameof(IsQuantityEnabled));
            }
        }

        public bool IsPurchasePriceEnabled
        {
            get { return isPurchasePriceEnabled; }
            set
            {
                isPurchasePriceEnabled = value;
                OnPropertyChanged(nameof(IsPurchasePriceEnabled));
            }
        }
        public IngredientRespone? SelectedIngredient
        {
            get { return selectedIngredient; }
            set
            {
                selectedIngredient = value;
                OnPropertyChanged(nameof(SelectedIngredient));
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                OnPropertyChanged(nameof(Unit));
            }
        }

        public double PurchasePrice
        {
            get { return purchasePrice; }
            set
            {
                purchasePrice = value;
                OnPropertyChanged(nameof(PurchasePrice));
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }
        public int TotalIngredient
        {
            get { return totalIngredient; }
            set
            {
                totalIngredient = value;
                OnPropertyChanged(nameof(TotalIngredient));
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

        public double EstimatedOutput
        {
            get { return estimateOutput; }
            set
            {
                estimateOutput = value;
                OnPropertyChanged(nameof(EstimatedOutput));
            }
        }
        public ObservableCollection<IngredientRespone> Ingredients { get; set; } = new();
        public IngredientViewModel(IGenericService<IngredientRespone, IngredientRequest> ingredientService)
        {
            this.ingredientService = ingredientService;
            ResetCommand = new RelayCommand(Reset);
            EditIngredientCommand = new RelayCommand<IngredientRespone>(EditIngredient);
            PurchasePriceFilterCommand = new RelayCommand(FilterByPurchasePrice);
            QuantityFilterCommand = new RelayCommand(FilterByquantity);
            DeleteIngredientCommand = new RelayCommand<IngredientRespone>(DeleteIngredient);
            UpSertIngredientCommand = new RelayCommand(UpSertIngredient);
            InitializeData();
        }

        private void EditIngredient(IngredientRespone? ingredient)
        {
            if (ingredient != null)
            {
                IsPurchasePriceEnabled = true;
                IsQuantityEnabled = true;
                SelectedIngredient = ingredient;
                Name = SelectedIngredient.Name;
                Unit = SelectedIngredient.Unit;
                PurchasePrice = SelectedIngredient.PurchasePrice;
                Quantity = SelectedIngredient.Quantity;
            }
            else return;
        }

        private void UpdateFilteredIngredients(IEnumerable<IngredientRespone> filteredIngredients)
        {
            Ingredients.Clear();
            foreach (var item in filteredIngredients)
            {
                Ingredients.Add(item);
            }
        }
        private void FilterByquantity()
        {
            var filteredIngredients = Ingredients.OrderByDescending(p => p.Quantity).ToList();
            UpdateFilteredIngredients(filteredIngredients);
        }

        private void FilterByPurchasePrice()
        {
            var filteredIngredients = Ingredients.OrderByDescending(p => p.PurchasePrice).ToList();
            UpdateFilteredIngredients(filteredIngredients);
        }

        private async void DeleteIngredient(IngredientRespone? ingredient)
        {
            if (ingredient != null && Ingredients.Contains(ingredient))
            {
                try
                {
                    bool isDeleted = await ingredientService.DeleteAsync(ingredient.Id);
                    if (isDeleted)
                    {
                        Ingredients.Remove(ingredient);
                        UpdateCardsInfo();
                    }
                    else return;
                }
                catch (Exception ex)
                {
                    HandleException("Error retrieving data", ex);
                }
            }
        }

        private void UpSertIngredient()
        {
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
            IngredientRequest ingredient = new()
            {
                Name = Name,
                Unit = Unit,
                PurchasePrice = PurchasePrice,
                Quantity = Quantity,
            };
            try
            {
                IngredientRespone? respone = await ingredientService.CreateAsync(ingredient);
                if (respone != null)
                {
                    Ingredients.Add(respone);
                    UpdateCardsInfo();
                    Reset();
                }
            }
            catch (Exception ex)
            {
                HandleException("Error retrieving data", ex);
            }
        }

        private async void UpdateIngredient()
        {
            SelectedIngredient.Name = Name;
            SelectedIngredient.Unit = Unit;
            SelectedIngredient.PurchasePrice = PurchasePrice;
            SelectedIngredient.Quantity = Quantity;

            IngredientRequest updateIngredient = new IngredientRequest
            {
                Name = Name,
                Unit = Unit,
                PurchasePrice = PurchasePrice,
                Quantity = Quantity
            };

            try
            {
                IngredientRespone? respone = await ingredientService.UpdateAsync(SelectedIngredient.Id, updateIngredient);
                if (respone != null)
                {
                    var index = Ingredients.IndexOf(SelectedIngredient);
                    if (index >= 0) { Ingredients[index] = respone; }
                    UpdateCardsInfo();
                    SelectedIngredient = null;
                    Reset();
                }
            }
            catch (Exception ex)
            {
                HandleException("Error retrieving data", ex);
            }
        }

        private async void InitializeData()
        {
            try
            {
                var respone = await ingredientService.GetAllAsync();
                if (respone != null)
                {
                    Ingredients.Clear();
                    foreach (var item in respone)
                    {
                        Ingredients.Add(item);
                    }
                    UpdateCardsInfo();
                }
            }
            catch (Exception ex)
            {
                HandleException("Error retrieving data", ex);
            }
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
    }
}
