using RuKiSo.ViewModels;

namespace RuKiSo.Views;

public partial class BatchPage : ContentPage
{
	private readonly BatchViewModel _viewModel;
	public BatchPage(BatchViewModel batchViewModel)
	{
		InitializeComponent();
		_viewModel = batchViewModel;
		BindingContext = batchViewModel;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearingAsync();
    }
}