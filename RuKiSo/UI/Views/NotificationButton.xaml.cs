using System.Runtime.Intrinsics.Arm;
using System.Windows.Input;

namespace RuKiSo.UI.Views;

public partial class NotificationButton : ContentView
{

    public static readonly BindableProperty HaveNotiProperty = BindableProperty.Create(
        nameof(HaveNotiProperty),
        typeof(bool),
        typeof(NotificationButton),
        default(bool));

    public string HaveNoti
    {
        get => (string)GetValue(HaveNotiProperty);
        set => SetValue(HaveNotiProperty, value);
    }

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(NotificationButton),
        default(ICommand));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
	public NotificationButton()
	{
		InitializeComponent();
	}
}