using CommunityToolkit.Maui.Views;
using RuKiSo.UI.Views;

namespace RuKiSo.Views;

public partial class ProductPage : ContentPage
{
	public ProductPage()
	{
		InitializeComponent();
	}

    private void AddProductBtn_Clicked(object sender, EventArgs e)
    {
        AddProductPopup.Show();
    }
}