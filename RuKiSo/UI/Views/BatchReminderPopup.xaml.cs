using CommunityToolkit.Maui.Views;
using RuKiSo.ViewModels;

namespace RuKiSo.UI.Views;
public partial class BatchReminderPopup : Popup
{
    public BatchReminderPopup(BatchReminderViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}