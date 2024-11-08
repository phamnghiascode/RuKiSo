using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;

namespace RuKiSo.ViewModels
{
    public class BatchViewModel : BaseViewModel
    {
        public ObservableCollection<BatchIngredientDTO> Ingredients { get; set; }
        public BatchViewModel()
        {
            InitData();
        }

        private void InitData()
        {
            Ingredients = new()
            {
                new() {Id = 1, IngredientName = "Men lá", StoredQuantity = 10, PricePerUnit = 200},
                new() {Id = 2, IngredientName = "Men thuốc bắc", StoredQuantity = 20, PricePerUnit = 150},
                new() {Id = 3, IngredientName = "Nếp cái hoa vàng", StoredQuantity = 500, PricePerUnit = 20},
                new() {Id = 4, IngredientName = "Nếp đen", StoredQuantity = 400, PricePerUnit = 18},
                new() {Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, PricePerUnit = 50},
                new() {Id = 1, IngredientName = "Men lá", StoredQuantity = 10, PricePerUnit = 200},
                new() {Id = 2, IngredientName = "Men thuốc bắc", StoredQuantity = 20, PricePerUnit = 150},
                new() {Id = 3, IngredientName = "Nếp cái hoa vàng", StoredQuantity = 500, PricePerUnit = 20},
                new() {Id = 4, IngredientName = "Nếp đen", StoredQuantity = 400, PricePerUnit = 18},
                new() {Id = 5, IngredientName = "Đòng đòng", StoredQuantity = 30, PricePerUnit = 50},
            };
        }
    }
}
