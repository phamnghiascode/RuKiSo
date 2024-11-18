using RuKiSo.ViewModels;

namespace RuKiSo.Views;

public partial class IngredientPage : ContentPage
{
	public IngredientPage(IngredientViewModel ingredientViewModel)
	{
		InitializeComponent();
		BindingContext = ingredientViewModel;
	}
}