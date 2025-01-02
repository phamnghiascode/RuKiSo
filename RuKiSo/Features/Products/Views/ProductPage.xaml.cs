using RuKiSo.ViewModels;

namespace RuKiSo.Views;

public partial class ProductPage : ContentPage
{
    private readonly ProductViewModel _viewModel;
    public ProductPage(ProductViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearingAsync();
    }
}