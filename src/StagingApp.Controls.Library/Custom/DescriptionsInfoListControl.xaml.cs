namespace StagingApp.Controls.Library.Custom;
public class DescriptionsInfoListControl : Control
{
    static DescriptionsInfoListControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DescriptionsInfoListControl), new FrameworkPropertyMetadata(typeof(DescriptionsInfoListControl)));
    }

    public ReadOnlyCollection<DescriptionInfoDto> Descriptions
    {
        get => (ReadOnlyCollection<DescriptionInfoDto>)GetValue(DescriptionsProperty);
        set { SetValue(_descriptionsPropertyKey, value); }
    }

    public Action CancelEditAction { get; }

    private static readonly ReadOnlyCollection<DescriptionInfoDto> _descriptionsEmpty = Array.AsReadOnly(Array.Empty<DescriptionInfoDto>());

    private static readonly DependencyPropertyKey _descriptionsPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(Descriptions),
            typeof(ReadOnlyCollection<DescriptionInfoDto>),
            typeof(DescriptionsInfoListControl),
            new PropertyMetadata(_descriptionsEmpty));

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DescriptionsProperty = _descriptionsPropertyKey.DependencyProperty;

    public DescriptionsInfoListControl()
    {
        DataContextChanged += OnDataContextChanged;
        CancelEditAction = CancelEdit;
    }

    private void CancelEdit(object? obj)
    {
        ReadOnlyCollection<DescriptionInfoDto> descriptions = obj is null
            ? _descriptionsEmpty
            : DescriptionInfoPropertyList.GetDescriptions(obj);

        Descriptions = descriptions;
        IsReadOnly = true;
    }
    public void CancelEdit() => CancelEdit(DataContext);
 
    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) =>
        CancelEdit(e.NewValue);



    public bool IsReadOnly
    {
        get { return (bool)GetValue(IsReadOnlyProperty); }
        set { SetValue(IsReadOnlyProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsReadOnlyProperty =
        DependencyProperty.Register(
            nameof(IsReadOnly),
            typeof(bool),
            typeof(DescriptionsInfoListControl),
            new PropertyMetadata(true));
}
