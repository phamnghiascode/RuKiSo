using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Models;
using RuKiSo.Features.Services;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public partial class ProductViewModel : BaseViewModel
    {
        private readonly IProductService productService;
        public ProductViewModel(IProductService productService)
        {
            this.productService = productService;
            ResetCommand = new RelayCommand(Reset);
            UpSertProductCommand = new RelayCommand(UpSertProduct);
            EditProductCommand = new RelayCommand<ProductRespone>(EditProduct);
            DeleteProductCommand = new RelayCommand<ProductRespone>(DeleteProduct);
            QuantityFilterCommand = new RelayCommand(OnQuantityFilter);
            PriceFilterCommand = new RelayCommand(OnPriceFilter);

            InitData();
        }

        private void UpSertProduct()
        {
            if (SelectedProduct != null) 
            {
                SelectedProduct.Name = Name;
                SelectedProduct.Ingredients = Ingredients;
                SelectedProduct.Price = Price;
                SelectedProduct.Quantity = Quantity;
                
                var index = Products.IndexOf(SelectedProduct);
                if (index >= 0) { Products[index] = SelectedProduct; }
                SelectedProduct = null;
            }
            else
            {
                ProductRespone product = new()
                {
                    Name = Name,
                    Ingredients = Ingredients,
                    Price = Price,
                    Quantity = Quantity,
                };
                Products.Add(product);
                UpdateCardsInfo();
                Reset();
            }
        }

        private void Reset()
        {
            SelectedProduct = null;
            Name = string.Empty;
            Ingredients = string.Empty;
            Quantity = 0;
            Price = 0;
        }

        private void EditProduct(ProductRespone? product)
        {
            if (product != null)
            {
                SelectedProduct = product;
                Name = product.Name;
                Ingredients = product.Ingredients;
                Quantity = product.Quantity;
                Price = product.Price;
            }
            else return;
        }

        private ProductRespone? selectedProduct;
        private int totalProduct;
        private double totalValue;
        private double estimatedProfit;
        private string name;
        private string ingredients;
        private int quantity;
        private double price;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Ingredients
        {
            get { return ingredients; }
            set
            {
                ingredients = value;
                OnPropertyChanged(nameof(Ingredients));
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

        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public ProductRespone? SelectedProduct 
        { 
            get { return selectedProduct; }
            set {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct)); 
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

        public ObservableCollection<ProductRespone> Products { get; private set; } = new();

        public ICommand ResetCommand { get; }
        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand QuantityFilterCommand { get; }
        public ICommand PriceFilterCommand { get; }
        public ICommand UpSertProductCommand { get; }

        private async void InitData()
        {
            //Products = new ObservableCollection<ProductRespone>
            //{
            //    new() {Id = 1, Name = "Rượu trắng thượng hạng", Ingredients = "Nếp cái hoa vàng, men thuốc bắc", Quantity = 300, Price = 50000},
            //    new() {Id = 2, Name = "Rượu trắng", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 700, Price = 45000},
            //    new() {Id = 3, Name = "Rượu trắng nhẹ", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 100, Price = 40000},
            //    new() {Id = 4, Name = "Rượu đòng đòng", Ingredients = "Nếp cái hoa vàng, bông lúa non, men thuốc bắc", Quantity = 500, Price = 75000},
            //    new() {Id = 5, Name = "Rượu bách nhật", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 5, Price = 40000},
            //};
            //UpdateCardsInfo();
            try
            {
                var respone = await productService.GetAll();
                Products.Clear();
                foreach (var item in respone) 
                {
                    Products.Add(item);
                }
                UpdateCardsInfo();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần
                Console.WriteLine($"Lỗi khi lấy dữ liệu: {ex.Message}");
            }
        }

        private void UpdateCardsInfo()
        {
            TotalProduct = Products.Count;
            TotalValue = Products.Sum(product => product.TotalValue);
            EstimatedProfit = Math.Floor(TotalValue * percentProfit);
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
            }
        }

        private void DeleteProduct(ProductRespone? product)
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

        private void UpdateProducts(IEnumerable<ProductRespone> filteredProducts)
        {
            Products.Clear();
            foreach (var product in filteredProducts)
            {
                Products.Add(product);
            }
        }
    }
}
