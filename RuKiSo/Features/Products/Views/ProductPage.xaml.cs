using RuKiSo.Features.Models;
using RuKiSo.Utils.MVVM;
using RuKiSo.ViewModels;

namespace RuKiSo.Views;

public partial class ProductPage : ContentPage
{
	public ProductPage(ProductViewModel productViewModel)
	{
		InitializeComponent();
		BindingContext = productViewModel;
	}
}