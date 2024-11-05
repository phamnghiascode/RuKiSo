using RuKiSo.Models;

namespace RuKiSo.UI.Views;

public partial class MostPurchaseCustomers : ContentView
{
    private int _currentIndex = 0;
    public MostPurchaseCustomers()
	{
		InitializeComponent();
	}
    private void Button_Clicked(object sender, EventArgs e)
    {
        IList<CustomerDTO> items = QuickTransferCollectionView.ItemsSource as IList<CustomerDTO>;
        if (items == null || items.Count == 0)
            return;

        _currentIndex++;
        if (_currentIndex >= items.Count)
        {
            _currentIndex = 0;
        }

        QuickTransferCollectionView.ScrollTo(_currentIndex, position: ScrollToPosition.Center, animate: true);
    }
}