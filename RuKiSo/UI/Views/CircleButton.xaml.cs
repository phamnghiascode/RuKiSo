namespace RuKiSo.UI.Views;

public partial class CircleButton : ContentView
{
    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
        nameof(ImageSource), typeof(string), typeof(CircleButton), default(string));

    public string ImageSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public static readonly BindableProperty SizeProperty =
        BindableProperty.Create(nameof(Size), typeof(double), typeof(CircleButton), 60.0, propertyChanged: OnSizeChanged);

    public static readonly BindableProperty CustomCornerRadiusProperty =
        BindableProperty.Create(nameof(CustomCornerRadius), typeof(double), typeof(CircleButton), 30.0);

    public static readonly BindableProperty NotificationCountProperty =
        BindableProperty.Create(nameof(NotificationCount), typeof(int), typeof(CircleButton), 0);

    public int NotificationCount
    {
        get => (int)GetValue(NotificationCountProperty);
        set => SetValue(NotificationCountProperty, value);
    }

    public double Size
    {
        get => (double)GetValue(SizeProperty);
        set => SetValue(SizeProperty, value);
    }

    public double CustomCornerRadius
    {
        get => (double)GetValue(CustomCornerRadiusProperty);
        private set => SetValue(CustomCornerRadiusProperty, value);
    }

    public CircleButton()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private static void OnSizeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CircleButton)bindable;
        double newSize = (double)newValue;
        control.CustomCornerRadius = newSize / 2;
    }
}
