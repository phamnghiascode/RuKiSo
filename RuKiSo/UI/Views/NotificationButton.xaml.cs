using System.Runtime.Intrinsics.Arm;

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
	public NotificationButton()
	{
		InitializeComponent();
	}
}