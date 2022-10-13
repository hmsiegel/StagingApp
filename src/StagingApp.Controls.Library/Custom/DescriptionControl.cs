namespace StagingApp.Controls.Library.Custom;

[TemplatePart(Name = _textBoxTemplateName, Type = typeof(TextBox))]
public class DescriptionControl : Control
{
    private const string _textBoxTemplateName = "PART_Textbox";
    private TextBox? _partTextBox;
    private Binding? _textBinding;

    public override void OnApplyTemplate()
    {
        _partTextBox = GetTemplateChild(_textBoxTemplateName) as TextBox;
        if (_partTextBox is TextBox tbox)
        {
            if (_textBinding is Binding binding)
            {
                tbox.SetBinding(TextBox.TextProperty, binding);
            }
            else
            {
                BindingOperations.ClearBinding(tbox, TextBox.TextProperty);
            }
        }
    }

    static DescriptionControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(DescriptionControl), new FrameworkPropertyMetadata(typeof(DescriptionControl)));
    }

    public DescriptionDto DescriptionSource
    {
        get => (DescriptionDto)GetValue(DescriptionSourceProperty);
        set => SetValue(DescriptionSourceProperty, value);
    }

    // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DescriptionSourceProperty =
        DependencyProperty.Register(
        nameof(DescriptionSource),
        typeof(DescriptionDto),
        typeof(DescriptionControl),
        new PropertyMetadata(null, DescriptionSourceChangedCallback));

    private static void DescriptionSourceChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DescriptionControl control = (DescriptionControl)d;
        Binding? binding = null;
        if (e.NewValue is DescriptionDto description)
        {
            binding = new Binding
            {
                Path = description.Path,
                Source = description.Source
            };
            if (description.IsReadonly)
            {
                binding.Mode = BindingMode.OneWay;
            }
            else
            {
                binding.Mode = BindingMode.TwoWay;
            }
        }
        control._textBinding = binding;
        if (control._partTextBox is TextBox tbox)
        {
            if (binding is null)
            {
                BindingOperations.ClearBinding(tbox, TextBox.TextProperty);
            }
            else
            {
                tbox.SetBinding(TextBox.TextProperty, binding);
            }
        }
    }
}
