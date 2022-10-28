namespace StagingApp.Controls.Library.Custom;

[TemplatePart(Name = _textBoxTemplateName, Type = typeof(TextBox))]
public class DescriptionControl : Control
{
    private const string _textBoxTemplateName = "PART_TextBox";
    private TextBox? _partTextBox;
    private Binding? _textBinding;

    public override void OnApplyTemplate()
    {
        _partTextBox = GetTemplateChild(_textBoxTemplateName) as TextBox;
        SetBindingPartTextbox();
    }

    private void SetBindingPartTextbox()
    {
        if (_partTextBox is TextBox tbox)
        {
            if (_textBinding is null)
            {
                BindingOperations.ClearBinding(tbox, TextBox.TextProperty);
            }
            else
            {
                tbox.SetBinding(TextBox.TextProperty, _textBinding);
            }
        }
    }

    static DescriptionControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(DescriptionControl),
            new FrameworkPropertyMetadata(typeof(DescriptionControl)));
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

    private static readonly PropertyPath _newValuePropertyPath =
        new(typeof(DescriptionDto).GetProperty(nameof(DescriptionDto.NewValue)));

    private static void DescriptionSourceChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DescriptionControl control = (DescriptionControl)d;
        Binding? binding = null;
        if (e.NewValue is DescriptionDto description)
        {
            binding = new Binding
            {
                Path = _newValuePropertyPath,
                Source = description.Source,
                Mode = BindingMode.TwoWay
            };
        }
        control._textBinding = binding;
        control.SetBindingPartTextbox();
    }
}
