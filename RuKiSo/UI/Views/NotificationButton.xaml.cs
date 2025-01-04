using RuKiSo.ViewModels;
using System.Windows.Input;
namespace RuKiSo.UI.Views;
public partial class NotificationButton : ContentView
{
    public static readonly BindableProperty HasNotificationsProperty = BindableProperty.Create(
        nameof(HasNotifications),
        typeof(bool),
        typeof(NotificationButton),
        false);

    public bool HasNotifications
    {
        get => (bool)GetValue(HasNotificationsProperty);
        set => SetValue(HasNotificationsProperty, value);
    }

    public ICommand ShowNotificationsCommand { get; }

    public NotificationButton(BatchReminderViewModel batchReminderViewModel)
    {
        InitializeComponent();
        BindingContext = batchReminderViewModel;
    }
}