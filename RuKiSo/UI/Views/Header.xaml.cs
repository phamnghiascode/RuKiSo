using System.Collections.ObjectModel;

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

    public ObservableCollection<string> ScaleValues { get; set; }
    public Header()
	{
        ScaleValues = new ObservableCollection<string>
        {
            "100%",
            "125%",
            "150%",
            "175%",
            "200%"
        };
        InitializeComponent();
        BindingContext = this;
	}
}