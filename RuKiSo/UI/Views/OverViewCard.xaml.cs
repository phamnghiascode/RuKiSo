namespace RuKiSo.UI.Views;

public partial class OverViewCard : ContentView
{
    public static readonly BindableProperty ImageSourceProperty =
        BindableProperty.Create(
            nameof(ImageSource),
            typeof(string),
            typeof(OverViewCard),
            default(string));

    public string ImageSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(OverViewCard),
            string.Empty);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty DesciptionTextProperty =
        BindableProperty.Create(
            nameof(DesciptionText),
            typeof(string),
            typeof(OverViewCard),
            string.Empty);

    public string DesciptionText
    {
        get => (string)GetValue(DesciptionTextProperty);
        set => SetValue(DesciptionTextProperty, value);
    }

    public static readonly BindableProperty CustomWidthRequestProperty =
        BindableProperty.Create(
            nameof(CustomWidthRequest),
            typeof(double),
            typeof(OverViewCard),
            default(double));

    public double CustomWidthRequest
    {
        get => (double)GetValue(CustomWidthRequestProperty);
        set => SetValue(CustomWidthRequestProperty, value);
    }
    public OverViewCard()
	{
		InitializeComponent();
	}
}