using Microsoft.Maui.Controls;

namespace CBA_app.Templates;

public partial class LabelEntryControl : ContentView
{
    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(LabelEntryControl), string.Empty, propertyChanged: OnLabelOrPlaceholderChanged);

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public static readonly BindableProperty EntryTextProperty =
        BindableProperty.Create(nameof(EntryText), typeof(string), typeof(LabelEntryControl), string.Empty, BindingMode.TwoWay);

    public string EntryText
    {
        get => (string)GetValue(EntryTextProperty);
        set => SetValue(EntryTextProperty, value);
    }

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(LabelEntryControl), null, propertyChanged: OnLabelOrPlaceholderChanged);

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    // Propiedad solo lectura para el Placeholder efectivo
    public string PlaceholderEffective
    {
        get => string.IsNullOrEmpty(Placeholder) ? LabelText : Placeholder;
    }

    public LabelEntryControl()
    {
        InitializeComponent();
        BindingContext = this;
    }

    // Notifica a la UI cuando LabelText o Placeholder cambian
    private static void OnLabelOrPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (LabelEntryControl)bindable;
        control.OnPropertyChanged(nameof(PlaceholderEffective));
    }
}