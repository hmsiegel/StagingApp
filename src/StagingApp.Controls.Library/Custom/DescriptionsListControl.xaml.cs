namespace StagingApp.Controls.Library.Custom;

public class DescriptionsListControl : Control
{
    static DescriptionsListControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DescriptionsListControl), new FrameworkPropertyMetadata(typeof(DescriptionsListControl)));
    }

    public ReadOnlyCollection<DescriptionDto> Descriptions
    {
        get => (ReadOnlyCollection<DescriptionDto>)GetValue(DescriptionsProperty);
        set { SetValue(DescriptionsPropertyKey, value); }
    }

    private static readonly ReadOnlyCollection<DescriptionDto> _descriptionsEmpty = Array.AsReadOnly(Array.Empty<DescriptionDto>());

    private static readonly DependencyPropertyKey DescriptionsPropertyKey =
        DependencyProperty.RegisterReadOnly(
            nameof(Descriptions),
            typeof(ReadOnlyCollection<DescriptionDto>),
            typeof(DescriptionsListControl),
            new PropertyMetadata(_descriptionsEmpty));

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DescriptionsProperty = DescriptionsPropertyKey.DependencyProperty;

    public DescriptionsListControl()
    {
        DataContextChanged += OnDataContextChanged;
        CancelEditAction = CancelEdit;
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e) => CancelEdit(e.NewValue);

    private void CancelEdit(object? obj)
    {
        ReadOnlyCollection<DescriptionDto> descriptions = obj is null
            ? _descriptionsEmpty
            : DescriptionPropertyList.GetDescriptions(obj);

        Descriptions = descriptions;
        IsReadOnly = true;
    }

    public void CancelEdit() => CancelEdit(DataContext);

    public Action CancelEditAction { get; }

    /// <summary>
    /// ReadOnly Mode.
    /// </summary>
    public bool IsReadOnly
    {
        get => (bool)GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    /// <summary><see cref="DependencyProperty"/> for property <see cref="IsReadOnly"/>.</summary>
    public static readonly DependencyProperty IsReadOnlyProperty =
        DependencyProperty.Register(nameof(IsReadOnly), typeof(bool), typeof(DescriptionsListControl), new PropertyMetadata(true));

}
