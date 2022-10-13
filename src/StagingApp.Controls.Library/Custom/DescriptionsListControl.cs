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
		set { SetValue(_descriptionsPropertyKey, value); }
	}

	private static readonly ReadOnlyCollection<DescriptionDto> _descriptionsEmpty = Array.AsReadOnly(Array.Empty<DescriptionDto>());

	private static readonly DependencyPropertyKey _descriptionsPropertyKey =
		DependencyProperty.RegisterReadOnly(
			nameof(Descriptions),
			typeof(ReadOnlyCollection<DescriptionDto>),
			typeof(DescriptionsListControl),
			new PropertyMetadata(_descriptionsEmpty));

	// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
	public static readonly DependencyProperty DescriptionsProperty = _descriptionsPropertyKey.DependencyProperty;

	public DescriptionsListControl()
	{
		DataContextChanged += OnDataContextChanged;
	}

	private DescriptionPropertyList? _descriptionPropertyList;

	private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (e.NewValue is null)
		{
			_descriptionPropertyList = null;
			Descriptions = _descriptionsEmpty;
		}
		else
		{
			_descriptionPropertyList = new DescriptionPropertyList(e.NewValue);
		}
		Descriptions = _descriptionPropertyList?.Descriptions ?? _descriptionsEmpty;
	}
}
