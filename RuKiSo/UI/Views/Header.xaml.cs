namespace RuKiSo.UI.Views;

public partial class Header : ContentView
{


    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(Header),
        default(string));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public Header()
	{
        InitializeComponent();
        BindingContext = this;
	}
}