using RuKiSo.ViewModels;

namespace RuKiSo.Views;

public partial class IngredientPage : ContentPage
{
    private readonly IngredientViewModel _viewModel;
    public IngredientPage(IngredientViewModel ingredientViewModel)
	{
		InitializeComponent();
		_viewModel = ingredientViewModel;
		BindingContext = ingredientViewModel;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.OnAppearingAsync();
    }
}