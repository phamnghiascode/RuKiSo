using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public class TransactionViewModel : BaseViewModel
    {
        private readonly IGenericService<ProductRespone, ProductRequest> productService;
        private readonly IGenericService<IngredientRespone, IngredientRequest> ingredientService;
        private readonly IGenericService<TransactionResponse, TransactionRequest> transactionService;
        private TransactionRequest selectedTransaction;

        public TransactionRequest SelectedTransaction
        {
            get { return selectedTransaction; }
            set 
            { 
                selectedTransaction = value; 
                OnPropertyChanged(nameof(SelectedTransaction)); 
            }
        }

        private bool isPopupOpen;

        public bool IsPopupOpen
        {
            get { return isPopupOpen; }
            set 
            { 
                isPopupOpen = value;
                OnPropertyChanged(nameof(IsPopupOpen));
            }
        }

        public ICommand DeleteTransactionCommand { get; set; }
        public ICommand AddPurchaseTransactionCommand { get; set; }
        public ICommand AddSellTransactionCommand { get; set; }
        public ICommand OpenEditTransactionPopupCommand { get; set; }
        public ICommand EditTransactionCommand { get; set; }
        public ObservableCollection<TransactionProductDTO> Products { get; set; }
        public ObservableCollection<TransactionIngredientDTO> Ingredients { get; set; }
        public ObservableCollection<TransactionRequest> Transactions { get; set; }
        public TransactionViewModel(IGenericService<ProductRespone, ProductRequest> productService,
                                    IGenericService<TransactionResponse, TransactionRequest> transactionService,
                                    IGenericService<IngredientRespone, IngredientRequest> ingredientService)
        {
            this.productService = productService;
            this.transactionService = transactionService;
            this.ingredientService = ingredientService;
            InitializeData();    
            InitializeCommand();
        }
        private void InitializeCommand()
        {
            EditTransactionCommand = new RelayCommand(EditTransaction);
            OpenEditTransactionPopupCommand = new RelayCommand<TransactionRequest>(OpenEditTransaction);
            AddPurchaseTransactionCommand = new RelayCommand<TransactionIngredientDTO>(AddPurchaseTransaction);
            AddSellTransactionCommand = new RelayCommand<TransactionProductDTO>(AddSellTransaction);
            DeleteTransactionCommand = new RelayCommand<TransactionRequest>(DeleteTransaction);
        }

        private void EditTransaction()
        {
            if (SelectedTransaction != null)
            {
                TransactionRequest transaction = new()
                {
                    Name = SelectedTransaction.Name,
                    TranDate = SelectedTransaction.TranDate,
                    TranType = SelectedTransaction.TranType,
                    Value = SelectedTransaction.Value,
                    Quantity = SelectedTransaction.Quantity
                };
                var index = Transactions.IndexOf(SelectedTransaction);
                Transactions[index] = transaction;
                IsPopupOpen = false;
            }
            else return;
        }

        private void OpenEditTransaction(TransactionRequest? transaction)
        {
            if (transaction != null)
            {
                SelectedTransaction = transaction;
                IsPopupOpen = true;
            }
            else return;
        }

        private void AddSellTransaction(TransactionProductDTO? product)
        {
            if (product != null)
            {
                var newTransaction = new TransactionRequest
                {
                    Name = product.Name,
                    TranType = true,
                    TranDate = DateTime.Now,
                    Quantity = product.UsedQuantity,
                    Value = product.UsedQuantity * product.Price
                };

                Transactions.Add(newTransaction);
            }
            else return;
        }

        private void AddPurchaseTransaction(TransactionIngredientDTO? ingredient)
        {
            if (ingredient != null && ingredient.UsedQuantity > 0)
            {
                var newTransaction = new TransactionRequest
                {
                    Name = ingredient.Name,
                    TranType = false,
                    TranDate = DateTime.Now,
                    Quantity = ingredient.UsedQuantity,
                    Value = ingredient.UsedQuantity * ingredient.PurchasePrice
                };

                Transactions.Add(newTransaction);
                ingredient.UsedQuantity = 0; 
            }
            else return;
        }

        private void DeleteTransaction(TransactionRequest? transaction)
        {
           if(transaction != null && Transactions.Contains(transaction))
            {
                Transactions.Remove(transaction);
            }
        }
        private void HandleException(string message, Exception ex)
        {
            Console.WriteLine($"{message}: {ex.Message}");
        }
        private async void InitializeData()
        {
            //Products = new ObservableCollection<TransactionProductDTO>
            //{
            //    new() {Id = 1, Name = "Rượu trắng 45", Ingredients = "Nếp cái hoa vàng, men thuốc bắc", Quantity = 300, Price = 50000},
            //    new() {Id = 2, Name = "Rượu trắng", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 700, Price = 45000},
            //    new() {Id = 3, Name = "Rượu trắng 40", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 100, Price = 40000},
            //    new() {Id = 4, Name = "Rượu đòng đòng", Ingredients = "Nếp cái hoa vàng, bông lúa non, men thuốc bắc", Quantity = 500, Price = 75000},
            //    new() {Id = 4, Name = "Rượu đòng đòng 45", Ingredients = "Nếp cái hoa vàng, bông lúa non, men thuốc bắc", Quantity = 500, Price = 75000},
            //    new() {Id = 5, Name = "Rượu bách nhật", Ingredients = "Nếp đen, men thuốc bắc", Quantity = 5, Price = 40000},
            //};
            //Ingredients = new ObservableCollection<TransactionIngredientDTO>()
            //{
            //    new() {Id = 1, Name = "Men thuốc bắc", PurchasePrice = 100, Unit = "Kg", Quantity = 10},
            //    new() {Id = 2, Name = "Men lá", PurchasePrice = 200, Unit = "Kg", Quantity = 100},
            //    new() {Id = 3, Name = "Gạo nếp đen", PurchasePrice = 400, Unit = "Kg", Quantity = 20},
            //    new() {Id = 4, Name = "Nếp cái hoa vàng", PurchasePrice = 100, Unit = "Kg", Quantity = 99},
            //    new() {Id = 5, Name = "Đòng đòng", PurchasePrice = 10, Unit = "Kg", Quantity = 1},
            //    new() {Id = 6, Name = "Gạo nếp", PurchasePrice = 990, Unit = "Kg", Quantity = 3},
            //};
            try
            {
                var response = await productService.GetAllAsync();
                //if (response != null)
                //{
                //    Products.Clear();
                //    foreach (var item in response)
                //    {
                //        Products.Add(item);
                //    }
                //}
            }
            catch (Exception ex)
            {
                HandleException("Error retrieving data", ex);
            }
            Transactions = new ObservableCollection<TransactionRequest>
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
