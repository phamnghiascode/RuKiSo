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
                UpdateProduct();
            }
            else
            {
                CreateProduct();
            }
        }
        private async void UpdateProduct()
        {
            SelectedProduct.Name = Name;
            SelectedProduct.Description = Description;
            SelectedProduct.Price = Price;
            SelectedProduct.Quantity = Quantity;

            ProductRequest updateProduct = new ProductRequest
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Quantity = Quantity
            };

            try
            {
                ProductRespone? respone = await productService.Update(selectedProduct.Id, updateProduct);
                if (respone != null)
                {
                    var index = Products.IndexOf(SelectedProduct);
                    if (index >= 0) { Products[index] = SelectedProduct; }
                    UpdateCardsInfo();
                    SelectedProduct = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xảy ra lỗi trong quá trình cập nhật sản phẩm {ex.Message}");
            }
        }

        private async void CreateProduct()
        {
            ProductRequest product = new()
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Quantity = Quantity,
            };
            try
            {
                ProductRespone? respone = await productService.Create(product);
                if (respone != null)
                {
                    Products.Add(respone);
                    UpdateCardsInfo();
                    Reset();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tạo mới dữ liệu: {ex.Message}");
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
            if (product != null)
            {
                SelectedProduct = product;
                Name = product.Name;
                Description = product.Description;
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
        private string description;
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

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
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
            try
            {
                var respone = await productService.GetAll();
                if (respone != null)
                {
                    Products.Clear();
                    foreach (var item in respone)
                    {
                        Products.Add(item);
                    }
                    UpdateCardsInfo();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy dữ liệu: {ex.Message}");
            }
        }

        private void UpdateCardsInfo()
        {
            TotalProduct = Products.Count;
            TotalValue = Products.Sum(product => product.TotalValue);
            EstimatedProfit = Math.Floor(TotalValue * percentProfit);
        }

        private async void DeleteProduct(ProductRespone? product)
        {
            if (product != null && Products.Contains(product))
            {
                try
                {
                    bool isDeleted = await productService.Delete(product.Id);
                    if (isDeleted)
                    {
                        Products.Remove(product);
                        UpdateCardsInfo();
                    }
                    else return;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Lỗi khi lấy dữ liệu: {ex.Message}");
                }
                
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
