using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;

namespace RuKiSo.ViewModels
{
    public class IngredientViewModel : BaseViewModel
    {
        private IngredientDTO selectedIngredient;
        private bool isPopupOpen;
        private int totalIngredient;
        private double totalValue;
        private double estimateOutput;

        public IngredientDTO SelectedIngredient
        {
            get { return selectedIngredient; }
            set
            {
                selectedIngredient = value;
                OnPropertyChanged(nameof(SelectedIngredient));
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
            InitData();
        }

        private void InitData()
        {
            Ingredients = new ObservableCollection<IngredientDTO>()
            {
                new() {Id = 1, Name = "Men thuốc bắc", ImageUrl = "estimatedprofit.png", Price = 100, Unit = "Kg", Quantity = 10},
                new() {Id = 1, Name = "Men thuốc bắc", ImageUrl = "estimatedprofit.png", Price = 100, Unit = "Kg", Quantity = 10},
                new() {Id = 1, Name = "Men thuốc bắc", ImageUrl = "estimatedprofit.png", Price = 100, Unit = "Kg", Quantity = 10},
                new() {Id = 1, Name = "Men thuốc bắc", ImageUrl = "estimatedprofit.png", Price = 100, Unit = "Kg", Quantity = 10},
                new() {Id = 1, Name = "Men thuốc bắc", ImageUrl = "estimatedprofit.png", Price = 100, Unit = "Kg", Quantity = 10},
                new() {Id = 1, Name = "Men thuốc bắc", ImageUrl = "estimatedprofit.png", Price = 100, Unit = "Kg", Quantity = 10},
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
