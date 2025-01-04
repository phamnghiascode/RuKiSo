using RuKiSo.ViewModels;

namespace RuKiSo.Views;

public partial class ProductPage : ContentPage
{
    private readonly ProductViewModel _viewModel;
    private readonly BatchReminderViewModel _reminderViewModel;
    public ProductPage(ProductViewModel viewModel, BatchReminderViewModel reminderViewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _reminderViewModel = reminderViewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearingAsync();
    }
}