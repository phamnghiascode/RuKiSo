using RuKiSo.ViewModels;

namespace RuKiSo.Views;

public partial class DashboardPage : ContentPage
{
	private readonly DashBoardViewModel _viewModel;
	public DashboardPage(DashBoardViewModel dashBoardViewModel)
	{
		InitializeComponent();
        _viewModel = dashBoardViewModel;
		BindingContext = dashBoardViewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearingAsync();
    }
}