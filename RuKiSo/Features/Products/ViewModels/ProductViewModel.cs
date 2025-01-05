using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Models;
using RuKiSo.Features.Services;
using RuKiSo.Resources.Text;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public partial class ProductViewModel : BaseViewModel
    {
        #region Fields

        private readonly IGenericService<ProductRespone, ProductRequest> _productService;
        private const double PercentProfit = 0.2;

        private ProductRespone selectedProduct;
        private string name = string.Empty;
        private string description = string.Empty;
        private int quantity;
        private double price;
        private int totalProduct;
        private double totalValue;
        private double estimatedProfit;
        private bool isPopupOpen;

        #endregion

        public ProductViewModel(
            IGenericService<ProductRespone, ProductRequest> productService,
            BatchReminderViewModel reminderViewModel,
            IErrorHandlingService errorHandlingService) : base(errorHandlingService)
        {
            _productService = productService;
            ReminderViewModel = reminderViewModel;

            InitializeCollections();
            InitializeCommands();
        }

        #region Properties

        public ObservableCollection<ProductRespone> Products { get; set; }
        public BatchReminderViewModel ReminderViewModel { get; }

        public ProductRespone SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged();
            }
        }

        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }

        public int TotalProduct
        {
            get => totalProduct;
            set
            {
                totalProduct = value;
                OnPropertyChanged();
            }
        }

        public double TotalValue
        {
            get => totalValue;
            set
            {
                totalValue = value;
                OnPropertyChanged();
            }
        }

        public double EstimatedProfit
        {
            get => estimatedProfit;
            set
            {
                estimatedProfit = value;
                OnPropertyChanged();
            }
        }

        public bool IsPopupOpen
        {
            get => isPopupOpen;
            set
            {
                isPopupOpen = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Initialization

        private void InitializeCollections()
        {
            Products = new();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                var response = await _productService.GetAllAsync();
                if (response?.Any() == true)
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
                HandleException(ErrorMessages.LOADING_PRODUCTS, ex);
            }
        }

        #endregion

        #region Helpers
        private void Reset()
        {
            SelectedProduct = null;
            Name = string.Empty;
            Description = string.Empty;
            Quantity = 0;
            Price = 0;
            IsPopupOpen = false;
        }

        private void UpdateCardsInfo()
        {
            TotalProduct = Products.Count;
            TotalValue = Products.Sum(product => product.TotalValue);
            EstimatedProfit = Math.Floor(TotalValue * PercentProfit);
        }

        private void UpdateProductList(IEnumerable<ProductRespone> sortedProducts)
        {
            Products.Clear();
            foreach (var product in sortedProducts)
            {
                Products.Add(product);
            }
        }

        #endregion

        #region Sort

        private void SortByQuantity()
        {
            var sortedProducts = Products.OrderByDescending(p => p.Quantity).ToList();
            UpdateProductList(sortedProducts);
        }

        private void SortByPrice()
        {
            var sortedProducts = Products.OrderByDescending(p => p.Price).ToList();
            UpdateProductList(sortedProducts);
        }

        #endregion

        #region Commands

        public ICommand ResetCommand { get; set; }
        public ICommand UpsertProductCommand { get; set; }
        public ICommand EditProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ICommand QuantityFilterCommand { get; set; }
        public ICommand PriceFilterCommand { get; set; }
        private void InitializeCommands()
        {
            ResetCommand = new RelayCommand(Reset);
            UpsertProductCommand = new RelayCommand(UpsertProduct);
            EditProductCommand = new RelayCommand<ProductRespone>(EditProduct);
            DeleteProductCommand = new RelayCommand<ProductRespone>(DeleteProduct);
            QuantityFilterCommand = new RelayCommand(SortByQuantity);
            PriceFilterCommand = new RelayCommand(SortByPrice);
        }

        #endregion

        #region Command Handlers
        private async void UpsertProduct()
        {
            if (SelectedProduct != null)
            {
                await UpdateProduct();
            }
            else
            {
                await CreateProduct();
            }
        }

        private async Task UpdateProduct()
        {
            if (SelectedProduct == null) return;

            try
            {
                var request = CreateProductRequest();
                var response = await _productService.UpdateAsync(SelectedProduct.Id, request);

                if (response != null)
                {
                    UpdateProductInCollection(response);
                    UpdateCardsInfo();
                    Reset();
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.UPDATING_PRODUCT, ex);
            }
        }

        private async Task CreateProduct()
        {
            try
            {
                var request = CreateProductRequest();
                var response = await _productService.CreateAsync(request);

                if (response != null)
                {
                    Products.Add(response);
                    UpdateCardsInfo();
                    Reset();
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.CREATING_PRODUCT, ex);
            }
        }

        private ProductRequest CreateProductRequest()
        {
            return new ProductRequest
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Quantity = Quantity
            };
        }

        private void UpdateProductInCollection(ProductRespone updatedProduct)
        {
            var index = Products.IndexOf(SelectedProduct);
            if (index != -1)
            {
                Products[index] = updatedProduct;
            }
        }

        private void EditProduct(ProductRespone product)
        {
            if (product == null) return;

            SelectedProduct = product;
            Name = product.Name;
            Description = product.Description;
            Quantity = product.Quantity;
            Price = product.Price;
            IsPopupOpen = true;
        }

        private async void DeleteProduct(ProductRespone product)
        {
            if (product == null || !Products.Contains(product)) return;

            try
            {
                var isDeleted = await _productService.DeleteAsync(product.Id);
                if (isDeleted)
                {
                    Products.Remove(product);
                    UpdateCardsInfo();
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.DELETING_PRODUCT, ex);
            }
        }

        #endregion
    }
}