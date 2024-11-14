using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using Syncfusion.Maui.Graphics.Internals;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public class IngredientViewModel : BaseViewModel
    {
        private IngredientDTO selectedIngredient;
        private bool isPopupOpen;
        private int totalIngredient;
        private double totalValue;
        private double estimateOutput;
        private string name;
        private string unit;
        private int quantity;
        private double purchasePrice;
        public ICommand AddIngredientCommand { get; set; }
        public ICommand OpenClosePopupCommand { get; set; }
        public ICommand DeleteIngredientCommand { get; set; }
        public ICommand QuantityFilterCommand { get; }
        public ICommand PurchasePriceFilterCommand { get; }

        public IngredientDTO SelectedIngredient
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
        public bool IsPopupOpen
        {
            get { return isPopupOpen; }
            set
            {
                isPopupOpen = value;
                OnPropertyChanged(nameof(IsPopupOpen));
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
        public ObservableCollection<IngredientDTO> Ingredients { get; set; }
        public IngredientViewModel()
        {
            PurchasePriceFilterCommand = new RelayCommand(FilterByPurchasePrice);
            QuantityFilterCommand = new RelayCommand(FilterByquantity);
            DeleteIngredientCommand = new RelayCommand<IngredientDTO>(DeleteIngredient);
            AddIngredientCommand = new RelayCommand(AddIngredient);
            OpenClosePopupCommand = new RelayCommand(OpenClosePopup);
            InitData();
        }
        private void UpdateIngredients(IEnumerable<IngredientDTO> filteredIngredients)
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
            UpdateIngredients(filteredIngredients);
        }

        private void FilterByPurchasePrice()
        {
            var filteredIngredients = Ingredients.OrderByDescending(p => p.PurchasePrice).ToList();
            UpdateIngredients(filteredIngredients);
        }

        private void DeleteIngredient(IngredientDTO? ingredient)
        {
            if (ingredient != null && Ingredients.Contains(ingredient))
            {
                Ingredients.Remove(ingredient);
                UpdateCardsInfo();
            }
        }

        private void OpenClosePopup()
        {
            IsPopupOpen = !IsPopupOpen;
        }

        private void AddIngredient()
        {
            IngredientDTO ingredient = new()
            {
                Name = Name,
                Unit = Unit,
                Quantity = Quantity,
                PurchasePrice = PurchasePrice
            };
            Ingredients.Add(ingredient);
            UpdateCardsInfo();
        }

        private void InitData()
        {
            Ingredients = new ObservableCollection<IngredientDTO>()
            {
                new() {Id = 1, Name = "Men thuốc bắc", PurchasePrice = 100, Unit = "Kg", Quantity = 10},
                new() {Id = 2, Name = "Men lá", PurchasePrice = 200, Unit = "Kg", Quantity = 100},
                new() {Id = 3, Name = "Gạo nếp đen", PurchasePrice = 400, Unit = "Kg", Quantity = 20},
                new() {Id = 4, Name = "Nếp cái hoa vàng", PurchasePrice = 100, Unit = "Kg", Quantity = 99},
                new() {Id = 5, Name = "Đòng đòng", PurchasePrice = 10, Unit = "Kg", Quantity = 1},
                new() {Id = 6, Name = "Gạo nếp", PurchasePrice = 990, Unit = "Kg", Quantity = 3},
            };
            UpdateCardsInfo();
        }
        private void UpdateCardsInfo()
        {
            TotalIngredient = Ingredients.Count;
            TotalValue = Ingredients.Sum(i => i.TotalValue);
            EstimatedOutput = Math.Floor(TotalValue);
        }
    }
}
