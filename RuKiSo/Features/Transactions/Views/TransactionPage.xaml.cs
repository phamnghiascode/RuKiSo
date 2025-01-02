using RuKiSo.ViewModels;

namespace RuKiSo.Views;

public partial class TransactionPage : ContentPage
{
	private readonly TransactionViewModel _viewModel;
	public TransactionPage(TransactionViewModel transactionViewModel)
	{
		InitializeComponent();
		_viewModel = transactionViewModel;
		BindingContext = transactionViewModel;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearingAsync();
    }
}