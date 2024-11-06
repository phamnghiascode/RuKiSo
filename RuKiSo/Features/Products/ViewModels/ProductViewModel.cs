using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        
        public ObservableCollection<ProductDTO> Products { get; set; }
        public int TotalProduct { get; set; }
        public double TotalValue { get; set; }
        public double EstimatedProfit { get; set; }
        private readonly double percentProfit = 0.4;
        public ProductViewModel()
        {
            EditProductCommand = new Command<ProductDTO>(OnDeleteProduct);
            DeleteProductCommand = new Command<ProductDTO>(OnDeleteProduct);
          InitData(); 
        }

        private void InitData()
        {
            Products = new ObservableCollection<ProductDTO>()
            {
                new() {Id = 1, ImageUrl = "estimatedprofit.png", Name = "Rượu trắng thượng hạng", Ingredients = "Nếp cái hoa vàng, men thuốc bắc", Quantity = 300, Price = 50000},
                new() {Id = 2, ImageUrl = "estimatedprofit.png", Name = "Rượu trắng", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 700, Price = 45000},
                new() {Id = 3, ImageUrl = "estimatedprofit.png", Name = "Rượu trắng nhẹ", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 100, Price = 40000},
                new() {Id = 4, ImageUrl = "estimatedprofit.png", Name = "Rượu đòng đòng", Ingredients = "Nếp cái hoa vàng, bông lúa non, men thuốc bắc", Quantity = 500, Price = 75000},
                new() {Id = 5, ImageUrl = "estimatedprofit.png", Name = "Rượu bách nhật", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 5, Price = 40000},
                new() {Id = 6, ImageUrl = "estimatedprofit.png", Name = "Rượu trắng thượng hạng", Ingredients = "Nếp cái hoa vàng, men thuốc bắc", Quantity = 300, Price = 50000},
                new() {Id = 7, ImageUrl = "estimatedprofit.png", Name = "Rượu trắng", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 700, Price = 45000},
                new() {Id = 8, ImageUrl = "estimatedprofit.png", Name = "Rượu trắng nhẹ", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 100, Price = 40000},
                new() {Id = 9, ImageUrl = "estimatedprofit.png", Name = "Rượu đòng đòng", Ingredients = "Nếp cái hoa vàng, bông lúa non, men thuốc bắc", Quantity = 500, Price = 75000},
                new() {Id = 10, ImageUrl = "estimatedprofit.png", Name = "Rượu bách nhật", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 5, Price = 40000},
                new() {Id = 11, ImageUrl = "estimatedprofit.png", Name = "Rượu trắng thượng hạng", Ingredients = "Nếp cái hoa vàng, men thuốc bắc", Quantity = 300, Price = 50000},
                new() {Id = 12, ImageUrl = "estimatedprofit.png", Name = "Rượu trắng", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 700, Price = 45000},
                new() {Id = 13, ImageUrl = "estimatedprofit.png", Name = "Rượu trắng nhẹ", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 100, Price = 40000},
                new() {Id = 14, ImageUrl = "estimatedprofit.png", Name = "Rượu đòng đòng", Ingredients = "Nếp cái hoa vàng, bông lúa non, men thuốc bắc", Quantity = 500, Price = 75000},
                new() {Id = 15, ImageUrl = "estimatedprofit.png", Name = "Rượu bách nhật", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 5, Price = 40000},
            };
            TotalProduct = Products.Count;
            TotalValue = Products.Sum(product => product.TotalValue);
            EstimatedProfit = TotalValue * percentProfit;
        }
        private void OnEditProduct(ProductDTO product)
        {
            
        }

        private void OnDeleteProduct(ProductDTO product)
        {
            
        }
    }
}
