namespace StagingApp.Controls.Library.Custom;

[TemplatePart(Name = _textBoxTemplateName, Type = typeof(TextBox))]
public partial class DescriptionControl : Control
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


    private static readonly PropertyPath NewValuePropertyPath = new PropertyPath(typeof(DescriptionDto).GetProperty(nameof(DescriptionDto.NewValue)));

    private static void DescriptionSourceChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DescriptionControl control = (DescriptionControl)d;
        DescriptionDto description = (DescriptionDto)e.NewValue;
        control.ProtectedDescriptionSource = description;

        Binding? binding = null;
        if (description is not null)
        {
            binding = new Binding
            {
                Path = NewValuePropertyPath,
                Source = description,
                Mode = BindingMode.TwoWay
            };
        }
        control._textBinding = binding;

        control.SetBindingPartTextBox();
    }
    protected DescriptionDto? ProtectedDescriptionSource { get; private set; }

    private void SetBindingPartTextBox()
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
        DependencyProperty.Register(
            nameof(IsReadOnly),
            typeof(bool),
            typeof(DescriptionControl),
            new PropertyMetadata(true, (d, e) => ((DescriptionControl)d).ProtectedIsReadOnly = (bool)e.NewValue));

    protected bool ProtectedIsReadOnly { get; private set; }
}
