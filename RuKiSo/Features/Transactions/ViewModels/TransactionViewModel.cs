using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;

namespace RuKiSo.ViewModels
{
    public class TransactionViewModel : BaseViewModel
    {
        public ObservableCollection<ProductDTO> Products { get; set; }
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
        }
    }
}
