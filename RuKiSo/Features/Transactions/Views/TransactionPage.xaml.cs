using RuKiSo.ViewModels;

namespace RuKiSo.Views;

public partial class TransactionPage : ContentPage
{
	public TransactionPage(TransactionViewModel transactionViewModel)
	{
		InitializeComponent();
		BindingContext = transactionViewModel;
	}
}