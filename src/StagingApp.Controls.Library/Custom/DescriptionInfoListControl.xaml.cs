namespace StagingApp.Controls.Library.Custom;
public class DescriptionInfoListControl : Control
{
    static DescriptionInfoListControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DescriptionInfoListControl), new FrameworkPropertyMetadata(typeof(DescriptionInfoListControl)));
    }

    public ReadOnlyCollection<DescriptionDto> Descriptions
    {
        get => (ReadOnlyCollection<DescriptionDto>)GetValue(DescriptionsProperty);
        set { SetValue(_descriptionsPropertyKey, value); }
    }

    private static readonly ReadOnlyCollection<DescriptionDto> _descriptionsEmpty = Array.AsReadOnly(Array.Empty<DescriptionDto>());

    private static readonly DependencyPropertyKey _descriptionsPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(Descriptions),
            typeof(ReadOnlyCollection<DescriptionDto>),
            typeof(DescriptionInfoListControl),
            new PropertyMetadata(_descriptionsEmpty));

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DescriptionsProperty = _descriptionsPropertyKey.DependencyProperty;

    public DescriptionInfoListControl()
    {
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        ReadOnlyCollection<DescriptionDto> descriptions;
        if (e.NewValue is null)
        {
            descriptions = _descriptionsEmpty;
        }
        else
        {
            descriptions = DescriptionPropertyList.GetDescriptions(e.NewValue);
        }

        Descriptions = descriptions;
    }
}
