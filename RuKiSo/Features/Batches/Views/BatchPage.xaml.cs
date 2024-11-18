using RuKiSo.ViewModels;

namespace RuKiSo.Views;

public partial class BatchPage : ContentPage
{
	public BatchPage(BatchViewModel batchViewModel)
	{
		BindingContext = batchViewModel;
		InitializeComponent();
	}
}