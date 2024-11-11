using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public partial class ProductViewModel : BaseViewModel
    {
        public ProductViewModel()
        {
            EditProductCommand = new RelayCommand<ProductDTO>(OpenEditProductPopup);
            DeleteProductCommand = new RelayCommand<ProductDTO>(DeleteProduct);
            QuantityFilterCommand = new RelayCommand(OnQuantityFilter);
            PriceFilterCommand = new RelayCommand(OnPriceFilter);
            AddProductCommand = new RelayCommand(OpenAddProductPopup);
            SaveProductCommand = new RelayCommand(SaveProduct);
            ClosePopupCommand = new RelayCommand(() => IsPopupOpen = false);

            InitData();
        }

        private ProductDTO selectedProduct;
        private bool isPopupOpen;
        private int totalProduct;
        private double totalValue;
        private double estimatedProfit;

        public ProductDTO SelectedProduct 
        { 
            get { return selectedProduct; }
            set {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct)); 
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
        public int TotalProduct
        {
            get { return totalProduct; }
            set
            {
                totalProduct = value;
                OnPropertyChanged(nameof(TotalProduct));
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

        public double EstimatedProfit
        {
            get { return estimatedProfit; }
            set
            {
                estimatedProfit = value;
                OnPropertyChanged(nameof(EstimatedProfit));
            }
        }


        private readonly double percentProfit = 0.35;

        public ObservableCollection<ProductDTO> Products { get; private set; } = new();

        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand QuantityFilterCommand { get; }
        public ICommand PriceFilterCommand { get; }
        public ICommand AddProductCommand { get; }
        public ICommand SaveProductCommand { get; }
        public ICommand ClosePopupCommand { get; }

        private void InitData()
        {
            Products = new ObservableCollection<ProductDTO>
            {
                new() {Id = 1, ImageUrl = "estimatedprofit.png", Name = "Rượu trắng thượng hạng", Ingredients = "Nếp cái hoa vàng, men thuốc bắc", Quantity = 300, Price = 50000},
                new() {Id = 2, ImageUrl = "estimatedprofit.png", Name = "Rượu trắng", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 700, Price = 45000},
                new() {Id = 3, ImageUrl = "estimatedprofit.png", Name = "Rượu trắng nhẹ", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 100, Price = 40000},
                new() {Id = 4, ImageUrl = "estimatedprofit.png", Name = "Rượu đòng đòng", Ingredients = "Nếp cái hoa vàng, bông lúa non, men thuốc bắc", Quantity = 500, Price = 75000},
                new() {Id = 5, ImageUrl = "estimatedprofit.png", Name = "Rượu bách nhật", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 5, Price = 40000},
            };
            UpdateCardsInfo();
        }

        private void UpdateCardsInfo()
        {
            TotalProduct = Products.Count;
            TotalValue = Products.Sum(product => product.TotalValue);
            EstimatedProfit = Math.Floor(TotalValue * percentProfit);
        }

        private void OpenEditProductPopup(ProductDTO product)
        {
            SelectedProduct = product;
            IsPopupOpen = true;
        }

        private void OpenAddProductPopup()
        {
            SelectedProduct = new ProductDTO();
            IsPopupOpen = true;
        }

        private void SaveProduct()
        {
            if (SelectedProduct != null)
            {
                if (!Products.Contains(SelectedProduct))
                {
                    Products.Add(SelectedProduct);
                }
                else
                {
                    var updateProduct = Products.FirstOrDefault(p => p.Id == SelectedProduct.Id);
                    {
                        updateProduct.Name = SelectedProduct.Name;
                        updateProduct.Quantity = SelectedProduct.Quantity;
                        updateProduct.Price = SelectedProduct.Price;
                    }
                }
                UpdateCardsInfo();
                IsPopupOpen = false;
            }
        }

        private void DeleteProduct(ProductDTO product)
        {
            if (product != null && Products.Contains(product))
            {
                Products.Remove(product);
                UpdateCardsInfo();
            }
        }

        private void OnQuantityFilter()
        {
            var filteredProducts = Products.OrderByDescending(p => p.Quantity).ToList();
            UpdateProducts(filteredProducts);
        }

        private void OnPriceFilter()
        {
            var filteredProducts = Products.OrderByDescending(p => p.Price).ToList();
            UpdateProducts(filteredProducts);
        }

        private void UpdateProducts(IEnumerable<ProductDTO> filteredProducts)
        {
            Products.Clear();
            foreach (var product in filteredProducts)
            {
                Products.Add(product);
            }
        }
    }
}
