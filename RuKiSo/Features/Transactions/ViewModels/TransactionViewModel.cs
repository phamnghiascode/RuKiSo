using CommunityToolkit.Mvvm.Input;
using RuKiSo.Entities;
using RuKiSo.Features.Models;
using RuKiSo.Features.Services;
using RuKiSo.Resources.Text;
using RuKiSo.Utils;
using RuKiSo.Utils.MVVM;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RuKiSo.ViewModels
{
    public partial class TransactionViewModel : BaseViewModel
    {

        #region Fields

        private readonly IGenericService<ProductRespone, ProductRequest> _productService;
        private readonly IGenericService<IngredientRespone, IngredientRequest> _ingredientService;
        private readonly IGenericService<TransactionResponse, TransactionRequest> _transactionService;

        private TransactionResponse selectedTransaction;
        private bool isPopupOpen;

        #endregion

        public TransactionViewModel(
            IGenericService<ProductRespone, ProductRequest> productService,
            IGenericService<IngredientRespone, IngredientRequest> ingredientService,
            IGenericService<TransactionResponse, TransactionRequest> transactionService,
            BatchReminderViewModel reminderViewModel,
            IErrorHandlingService errorHandlingService) : base(errorHandlingService)
        {
            _productService = productService;
            _ingredientService = ingredientService;
            _transactionService = transactionService;
            ReminderViewModel = reminderViewModel;

            InitializeCollections();
            InitializeCommands();
        }

        #region Initialization

        private void InitializeCollections()
        {
            Transactions = new();
            Products = new();
            Ingredients = new();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                await Task.WhenAll(
                    LoadTransactions(),
                    LoadProducts(),
                    LoadIngredients()
                );
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.LOADING_DASHBOARD, ex);
            }
        }

        private async Task LoadTransactions()
        {
            try
            {
                var response = await _transactionService.GetAllAsync();
                if (response?.Any() == true)
                {
                    Transactions.Clear();
                    foreach (var item in response.OrderByDescending(t => t.TranDate))
                    {
                        Transactions.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.LOADING_TRANSACTIONS, ex);
            }
        }

        private async Task LoadProducts()
        {
            try
            {
                var response = await _productService.GetAllAsync();
                if (response?.Any() == true)
                {
                    Products.Clear();
                    foreach (var item in response.OrderByDescending(p => p.Quantity))
                    {
                        Products.Add(item.ToTransactionProductDTO());
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.LOADING_PRODUCTS, ex);
            }
        }

        private async Task LoadIngredients()
        {
            try
            {
                var response = await _ingredientService.GetAllAsync();
                if (response?.Any() == true)
                {
                    Ingredients.Clear();
                    foreach (var item in response.OrderBy(i => i.Quantity))
                    {
                        Ingredients.Add(item.ToTransactionIngredientDTO());
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.LOADING_INGREDIENTS, ex);
            }
        }

        #endregion

        #region Properties

        public TransactionResponse SelectedTransaction
        {
            get => selectedTransaction;
            set
            {
                selectedTransaction = value;
                OnPropertyChanged(nameof(SelectedTransaction));
            }
        }

        public bool IsPopupOpen
        {
            get => isPopupOpen;
            set
            {
                isPopupOpen = value;
                OnPropertyChanged(nameof(IsPopupOpen));
            }
        }

        public ObservableCollection<TransactionProductDTO> Products { get; set; }
        public ObservableCollection<TransactionIngredientDTO> Ingredients { get; set; }
        public ObservableCollection<TransactionResponse> Transactions { get; set; }
        public BatchReminderViewModel ReminderViewModel { get; }

#endregion

        #region Commands

        public ICommand EditTransactionCommand { get; set; }
        public ICommand OpenEditTransactionPopupCommand { get; set; }
        public ICommand AddPurchaseTransactionCommand { get; set; }
        public ICommand AddSellTransactionCommand { get; set; }
        public ICommand DeleteTransactionCommand { get; set; }
        private void InitializeCommands()
        {
            EditTransactionCommand = new RelayCommand(EditTransaction);
            OpenEditTransactionPopupCommand = new RelayCommand<TransactionResponse>(OpenEditTransaction);
            AddPurchaseTransactionCommand = new RelayCommand<TransactionIngredientDTO>(AddPurchaseTransaction);
            AddSellTransactionCommand = new RelayCommand<TransactionProductDTO>(AddSellTransaction);
            DeleteTransactionCommand = new RelayCommand<TransactionResponse>(DeleteTransaction);
        }

        #endregion

        #region Command Handlers

        private async void EditTransaction()
        {
            if (SelectedTransaction == null) return;

            try
            {
                var request = CreateTransactionRequest(SelectedTransaction);
                var updatedTransaction = await _transactionService.UpdateAsync(SelectedTransaction.Id, request);

                if (updatedTransaction != null)
                {
                    UpdateTransactionInCollection(updatedTransaction);
                    IsPopupOpen = false;
                    await Task.WhenAll(LoadProducts(), LoadIngredients());
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.UPDATING_TRANSACTION, ex);
            }
        }

        private TransactionRequest CreateTransactionRequest(TransactionResponse transaction)
        {
            return new TransactionRequest
            {
                ProductId = transaction.ProductId,
                IngredientId = transaction.IngredientId,
                TranDate = transaction.TranDate,
                TranType = transaction.TranType,
                Value = transaction.Value,
                Quantity = transaction.Quantity
            };
        }

        private void OpenEditTransaction(TransactionResponse? transaction)
        {
            if (transaction == null) return;

            SelectedTransaction = transaction;
            IsPopupOpen = true;
        }

        private async void AddSellTransaction(TransactionProductDTO? product)
        {
            if (product == null || !IsValidQuantity(product.UsedQuantity.ToString()))
            {
                HandleException(ErrorMessages.INVALID_TRANSACTION, new Exception("Số lượng không hợp lệ"));
                return;
            }
            try
            {
                var request = new TransactionRequest
                {
                    TranType = true,
                    Quantity = product.UsedQuantity,
                    Value = product.UsedQuantity * product.Price,
                    TranDate = DateTime.Now,
                    ProductId = product.Id,
                };

                var response = await _transactionService.CreateAsync(request);
                if (response != null)
                {
                    Transactions.Add(response);
                    await Task.WhenAll(LoadProducts(), LoadIngredients());
                    product.UsedQuantity = 0;
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.CREATING_TRANSACTION, ex);
            }
        }

        private async void AddPurchaseTransaction(TransactionIngredientDTO? ingredient)
        {
            if (ingredient == null || !IsValidQuantity(ingredient.UsedQuantity.ToString()))
            {
                HandleException(ErrorMessages.INVALID_TRANSACTION, new Exception("Số lượng không hợp lệ"));
                return;
            }
            try
            {
                var request = new TransactionRequest
                {
                    TranType = false,
                    Quantity = ingredient.UsedQuantity,
                    Value = ingredient.UsedQuantity * ingredient.PurchasePrice,
                    TranDate = DateTime.Now,
                    IngredientId = ingredient.Id,
                };

                var response = await _transactionService.CreateAsync(request);
                if (response != null)
                {
                    Transactions.Add(response);
                    await Task.WhenAll(LoadProducts(), LoadIngredients());
                    ingredient.UsedQuantity = 0;
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.CREATING_TRANSACTION, ex);
            }
        }

        private async void DeleteTransaction(TransactionResponse? transaction)
        {
            if (transaction == null || !Transactions.Contains(transaction)) return;

            try
            {
                var isDeleted = await _transactionService.DeleteAsync(transaction.Id);
                if (isDeleted)
                {
                    Transactions.Remove(transaction);
                    await Task.WhenAll(LoadProducts(), LoadIngredients());
                }
            }
            catch (Exception ex)
            {
                HandleException(ErrorMessages.DELETING_TRANSACTION, ex);
            }
        }

        private void UpdateTransactionInCollection(TransactionResponse updatedTransaction)
        {
            var index = Transactions.IndexOf(SelectedTransaction);
            if (index != -1)
            {
                Transactions[index] = updatedTransaction;
            }
        }

        #endregion

        #region Helpers

        private bool IsValidQuantity(string input)
        {
            return !string.IsNullOrEmpty(input) &&
                   int.TryParse(input, out int value) &&
                   value >= 0;
        }

        #endregion
    }
}