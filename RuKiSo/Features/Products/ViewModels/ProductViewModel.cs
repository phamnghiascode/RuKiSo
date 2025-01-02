using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        private readonly IGenericService<ProductRespone, ProductRequest> productService;
        private const double PercentProfit = 0.2;

        private ProductRespone? selectedProduct;
        private string name = string.Empty;
        private string description = string.Empty;
        private int quantity;
        private double price;
        private int totalProduct;
        private double totalValue;
        private double estimatedProfit;

        public ObservableCollection<ProductRespone> Products { get; } = new();

        public ProductViewModel(IGenericService<ProductRespone, ProductRequest> productService)
        {
            this.productService = productService;
            InitializeCommands();
        }

        public ProductRespone? SelectedProduct
        {
            get => selectedProduct;
            set
            {
                if (selectedProduct != value)
                {
                    selectedProduct = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => description;
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Quantity
        {
            get => quantity;
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Price
        {
            get => price;
            set
            {
                if (price != value)
                {
                    price = value;
                    OnPropertyChanged();
                }
            }
        }

        public int TotalProduct
        {
            get => totalProduct;
            set
            {
                if (totalProduct != value)
                {
                    totalProduct = value;
                    OnPropertyChanged();
                }
            }
        }

        public double TotalValue
        {
            get => totalValue;
            set
            {
                if (totalValue != value)
                {
                    totalValue = value;
                    OnPropertyChanged();
                }
            }
        }

        public double EstimatedProfit
        {
            get => estimatedProfit;
            set
            {
                if (estimatedProfit != value)
                {
                    estimatedProfit = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ResetCommand { get; private set; }
        public ICommand UpSertProductCommand { get; private set; }
        public ICommand EditProductCommand { get; private set; }
        public ICommand DeleteProductCommand { get; private set; }
        public ICommand QuantityFilterCommand { get; private set; }
        public ICommand PriceFilterCommand { get; private set; }

        private void InitializeCommands()
        {
            ResetCommand = new RelayCommand(Reset);
            UpSertProductCommand = new RelayCommand(UpSertProduct);
            EditProductCommand = new RelayCommand<ProductRespone>(EditProduct);
            DeleteProductCommand = new RelayCommand<ProductRespone>(DeleteProduct);
            QuantityFilterCommand = new RelayCommand(OnQuantityFilter);
            PriceFilterCommand = new RelayCommand(OnPriceFilter);
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                var response = await productService.GetAllAsync();
                if (response != null)
                {
                    Products.Clear();
                    foreach (var item in response)
                    {
                        Products.Add(item);
                    }
                    UpdateCardsInfo();
                }
            }
            catch (Exception ex)
            {
                HandleException("Error retrieving data", ex);
            }
        }

        private void UpSertProduct()
        {
            if (SelectedProduct != null)
            {
                UpdateProduct();
            }
            else
            {
                CreateProduct();
            }
        }

        private async void UpdateProduct()
        {
            if (SelectedProduct == null) return;

            var updateProduct = new ProductRequest
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Quantity = Quantity
            };

            try
            {
                var response = await productService.UpdateAsync(SelectedProduct.Id, updateProduct);
                if (response != null)
                {
                    var index = Products.IndexOf(SelectedProduct);
                    if (index >= 0)
                    {
                        Products[index] = response;
                    }
                    UpdateCardsInfo();
                    Reset();
                }
            }
            catch (Exception ex)
            {
                HandleException("Error updating product", ex);
            }
        }

        private async void CreateProduct()
        {
            var product = new ProductRequest
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Quantity = Quantity,
            };

            try
            {
                var response = await productService.CreateAsync(product);
                if (response != null)
                {
                    Products.Add(response);
                    UpdateCardsInfo();
                    Reset();
                }
            }
            catch (Exception ex)
            {
                HandleException("Error creating product", ex);
            }
        }

        private void Reset()
        {
            SelectedProduct = null;
            Name = string.Empty;
            Description = string.Empty;
            Quantity = 0;
            Price = 0;
        }

        private void EditProduct(ProductRespone? product)
        {
            if (product == null) return;

            SelectedProduct = product;
            Name = product.Name;
            Description = product.Description;
            Quantity = product.Quantity;
            Price = product.Price;
        }

        private async void DeleteProduct(ProductRespone? product)
        {
            if (product == null || !Products.Contains(product)) return;

            try
            {
                bool isDeleted = await productService.DeleteAsync(product.Id);
                if (isDeleted)
                {
                    Products.Remove(product);
                    UpdateCardsInfo();
                }
            }
            catch (Exception ex)
            {
                HandleException("Error deleting product", ex);
            }
        }

        private void UpdateCardsInfo()
        {
            TotalProduct = Products.Count;
            TotalValue = Products.Sum(product => product.TotalValue);
            EstimatedProfit = Math.Floor(TotalValue * PercentProfit);
        }

        private void OnQuantityFilter()
        {
            var filteredProducts = Products.OrderByDescending(p => p.Quantity).ToList();
            UpdateProductList(filteredProducts);
        }

        private void OnPriceFilter()
        {
            var filteredProducts = Products.OrderByDescending(p => p.Price).ToList();
            UpdateProductList(filteredProducts);
        }

        private void UpdateProductList(IEnumerable<ProductRespone> filteredProducts)
        {
            Products.Clear();
            foreach (var product in filteredProducts)
            {
                Products.Add(product);
            }
        }
    }
}