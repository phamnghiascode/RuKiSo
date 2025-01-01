using RuKiSo.ViewModels;

namespace RuKiSo.Views;

public partial class DashboardPage : ContentPage
{
	public DashboardPage(DashBoardViewModel dashBoardViewModel)
	{
		InitializeComponent();
		BindingContext = dashBoardViewModel;
    }
}