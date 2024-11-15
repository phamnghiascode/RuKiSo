
using RuKiSo.Features.Models;

namespace RuKiSo.UI.Views;

public partial class CustomHorizontalCollectionView : ContentView
{
    private int _currentIndex = 0;
    public CustomHorizontalCollectionView()
	{
		InitializeComponent();
	}
    private void Button_Clicked(object sender, EventArgs e)
    {
        IList<MostUsedIngredient> items = MostUsedIngredientCollectionView.ItemsSource as IList<MostUsedIngredient>;
        if (items == null || items.Count == 0)
            return;

        _currentIndex++;
        if (_currentIndex >= items.Count)
        {
            _currentIndex = 0;
        }

        MostUsedIngredientCollectionView.ScrollTo(_currentIndex, position: ScrollToPosition.Center, animate: true);
    }
}