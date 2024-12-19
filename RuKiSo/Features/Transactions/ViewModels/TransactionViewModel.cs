using CommunityToolkit.Mvvm.Input;
using RuKiSo.Features.Models;
using RuKiSo.Utils;
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
        public ObservableCollection<TransactionResponse> Transactions { get; set; }
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
            //EditTransactionCommand = new RelayCommand(EditTransaction);
            //OpenEditTransactionPopupCommand = new RelayCommand<TransactionRequest>(OpenEditTransaction);
            //AddPurchaseTransactionCommand = new RelayCommand<TransactionIngredientDTO>(AddPurchaseTransaction);
            //AddSellTransactionCommand = new RelayCommand<TransactionProductDTO>(AddSellTransaction);
            DeleteTransactionCommand = new RelayCommand<TransactionResponse>(DeleteTransaction);
        }

        //private void EditTransaction()
        //{
        //    if (SelectedTransaction != null)
        //    {
        //        TransactionRequest transaction = new()
        //        {
        //            Name = SelectedTransaction.Name,
        //            TranDate = SelectedTransaction.TranDate,
        //            TranType = SelectedTransaction.TranType,
        //            Value = SelectedTransaction.Value,
        //            Quantity = SelectedTransaction.Quantity
        //        };
        //        var index = Transactions.IndexOf(SelectedTransaction);
        //        Transactions[index] = transaction;
        //        IsPopupOpen = false;
        //    }
        //    else return;
        //}

        private void OpenEditTransaction(TransactionRequest? transaction)
        {
            if (transaction != null)
            {
                SelectedTransaction = transaction;
                IsPopupOpen = true;
            }
            else return;
        }

        //private void AddSellTransaction(TransactionProductDTO? product)
        //{
        //    if (product != null)
        //    {
        //        var newTransaction = new TransactionRequest
        //        {
        //            Name = product.Name,
        //            TranType = true,
        //            TranDate = DateTime.Now,
        //            Quantity = product.UsedQuantity,
        //            Value = product.UsedQuantity * product.Price
        //        };

        //        Transactions.Add(newTransaction);
        //    }
        //    else return;
        //}

        //private void AddPurchaseTransaction(TransactionIngredientDTO? ingredient)
        //{
        //    if (ingredient != null && ingredient.UsedQuantity > 0)
        //    {
        //        var newTransaction = new TransactionRequest
        //        {
        //            Name = ingredient.Name,
        //            TranType = false,
        //            TranDate = DateTime.Now,
        //            Quantity = ingredient.UsedQuantity,
        //            Value = ingredient.UsedQuantity * ingredient.PurchasePrice
        //        };

        //        Transactions.Add(newTransaction);
        //        ingredient.UsedQuantity = 0;
        //    }
        //    else return;
        //}

        private async void DeleteTransaction(TransactionResponse? transaction)
        {
            if(transaction == null || !Transactions.Contains(transaction)) return;
            try
            {
                bool isDeleted = await transactionService.DeleteAsync(transaction.Id);
                if (isDeleted)
                {
                    Transactions.Remove(transaction);
                }
            }
            catch (Exception ex)
            {
                HandleException("Error deleting transaction", ex);
            }
        }
        private void HandleException(string message, Exception ex)
        {
            Console.WriteLine($"{message}: {ex.Message}");
        }
        private async Task InitializeData()
        {
            Transactions = new ObservableCollection<TransactionResponse>();
            Products = new ObservableCollection<TransactionProductDTO>();
            Ingredients = new ObservableCollection<TransactionIngredientDTO>();

            await LoadTransactions();
            await LoadProducts();
            await LoadIngredients();
        }

        private async Task LoadTransactions()
        {
            try
            {
                var response = await transactionService.GetAllAsync();
                if (response?.Any() == true)
                {
                    Transactions.Clear();
                    foreach (var item in response)
                    {
                        Transactions.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException("Error retrieving transactions", ex);
            }
        }

        private async Task LoadProducts()
        {
            try
            {
                var response = await productService.GetAllAsync();
                if (response?.Any() == true)
                {
                    Products.Clear();
                    foreach (var item in response)
                    {
                        Products.Add(item.ToTransactionProductDTO());
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException("Error retrieving products", ex);
            }
        }

        private async Task LoadIngredients()
        {
            try
            {
                var response = await ingredientService.GetAllAsync();
                if (response?.Any() == true)
                {
                    Ingredients.Clear();
                    foreach (var item in response)
                    {
                        Ingredients.Add(item.ToTransactionIngredientDTO());
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException("Error retrieving ingredients", ex);
            }
        }
    }
}
