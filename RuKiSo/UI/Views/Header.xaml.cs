using RuKiSo.ViewModels;

namespace RuKiSo.UI.Views;

public partial class Header : ContentView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(Header),
        default(string));

    public static readonly BindableProperty ReminderViewModelProperty = BindableProperty.Create(
        nameof(ReminderViewModel),
        typeof(BatchReminderViewModel),
        typeof(Header),
        null);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public BatchReminderViewModel ReminderViewModel
    {
        get => (BatchReminderViewModel)GetValue(ReminderViewModelProperty);
        set => SetValue(ReminderViewModelProperty, value);
    }

    public Header()
    {
        InitializeComponent();
    }
}