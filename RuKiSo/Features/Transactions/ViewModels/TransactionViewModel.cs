using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.Foundation.Metadata;

namespace RuKiSo.ViewModels
{
    public class TransactionViewModel : BaseViewModel
    {
        public ObservableCollection<ProductDTO> Products { get; set; }
        public ObservableCollection<IngredientDTO> Ingredients { get; set; }
        public ObservableCollection<TransactionDTO> Transactions { get; set; }
        public TransactionViewModel()
        {
            InitData();    
        }

        private void InitData()
        {
            Products = new ObservableCollection<ProductDTO>
            {
                new() {Id = 1, Name = "Rượu trắng 45", Ingredients = "Nếp cái hoa vàng, men thuốc bắc", Quantity = 300, Price = 50000},
                new() {Id = 2, Name = "Rượu trắng", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 700, Price = 45000},
                new() {Id = 3, Name = "Rượu trắng 40", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 100, Price = 40000},
                new() {Id = 4, Name = "Rượu đòng đòng", Ingredients = "Nếp cái hoa vàng, bông lúa non, men thuốc bắc", Quantity = 500, Price = 75000},
                new() {Id = 4, Name = "Rượu đòng đòng 45", Ingredients = "Nếp cái hoa vàng, bông lúa non, men thuốc bắc", Quantity = 500, Price = 75000},
                new() {Id = 5, Name = "Rượu bách nhật", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 5, Price = 40000},
            };
            Ingredients = new ObservableCollection<IngredientDTO>()
            {
                new() {Id = 1, Name = "Men thuốc bắc", PurchasePrice = 100, Unit = "Kg", Quantity = 10},
                new() {Id = 2, Name = "Men lá", PurchasePrice = 200, Unit = "Kg", Quantity = 100},
                new() {Id = 3, Name = "Gạo nếp đen", PurchasePrice = 400, Unit = "Kg", Quantity = 20},
                new() {Id = 4, Name = "Nếp cái hoa vàng", PurchasePrice = 100, Unit = "Kg", Quantity = 99},
                new() {Id = 5, Name = "Đòng đòng", PurchasePrice = 10, Unit = "Kg", Quantity = 1},
                new() {Id = 6, Name = "Gạo nếp", PurchasePrice = 990, Unit = "Kg", Quantity = 3},
            };
            Transactions = new ObservableCollection<TransactionDTO>
            {
                new() { Name = "Rượu đòng đòng 30", TranType = true, TranDate = DateTime.Now, Quantity = 10, Value= 100},
                new() { Name = "Rượu trắng 45", TranType = false, TranDate = DateTime.Now, Quantity = 10, Value= 100},
                new() { Name = "Rượu trắng 35", TranType = true, TranDate = DateTime.Now, Quantity = 10, Value= 100},
                new() { Name = "Rượu bách nhật", TranType = false, TranDate = DateTime.Now, Quantity = 10, Value= 100},
                new() { Name = "Rượu đong đòng 45", TranType = true, TranDate = DateTime.Now, Quantity = 10, Value= 100},
            };
        }
    }
}
