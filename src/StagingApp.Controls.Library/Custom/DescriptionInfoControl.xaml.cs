namespace StagingApp.Controls.Library.Custom;

[TemplatePart(Name = _textBoxTemplateName, Type = typeof(TextBox))]
public partial class DescriptionInfoControl : Control
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

    static DescriptionInfoControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(DescriptionInfoControl),
            new FrameworkPropertyMetadata(typeof(DescriptionInfoControl)));
    }

    public DescriptionInfoDto DescriptionSource
    {
        get => (DescriptionInfoDto)GetValue(DescriptionSourceProperty);
        set => SetValue(DescriptionSourceProperty, value);
    }

    // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DescriptionSourceProperty =
        DependencyProperty.Register(
        nameof(DescriptionSource),
        typeof(DescriptionInfoDto),
        typeof(DescriptionInfoControl),
        new PropertyMetadata(null, DescriptionSourceChangedCallback));

    private static readonly PropertyPath _newValuePropertyPath =
        new(typeof(DescriptionInfoDto).GetProperty(nameof(DescriptionInfoDto.NewValue)));

    private static void DescriptionSourceChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DescriptionInfoControl control = (DescriptionInfoControl)d;
        DescriptionInfoDto description = (DescriptionInfoDto)e.NewValue;
        control.ProtectedDescriptionSource = description;
        Binding? binding = null;
        
        if (description is not null)
        {
            binding = new Binding
            {
                Path = _newValuePropertyPath,
                Source = description,
                Mode = BindingMode.TwoWay
            };
        }
        control._textBinding = binding;
        control.SetBindingPartTextbox();
    }

    protected DescriptionInfoDto? ProtectedDescriptionSource { get; private set; }

    public bool IsReadOnly
    {
        get { return (bool)GetValue(IsReadOnlyProperty); }
        set { SetValue(IsReadOnlyProperty, value); }
    }

    protected bool ProtectedIsReadOnly { get; private set; }

    // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsReadOnlyProperty =
        DependencyProperty.Register(
           nameof(IsReadOnly), 
           typeof(bool),
           typeof(DescriptionInfoControl),
           new PropertyMetadata(true, (d,e) => ((DescriptionInfoControl)d).ProtectedIsReadOnly = (bool)e.NewValue));


}
