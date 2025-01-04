using CommunityToolkit.Maui.Views;
using RuKiSo.ViewModels;

namespace RuKiSo.UI.Views;
public partial class BatchReminder : Popup
{
    public BatchReminder(BatchReminderViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}