using System.ComponentModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Input;

namespace StagingApp.Main.Controls.Common;
/// <summary>
/// Interaction logic for DeviceInfoRow.xaml
/// </summary>
public partial class DeviceInfoRow : UserControl
{
    public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
        nameof(LabelText),
        typeof(string),
        typeof(DeviceInfoRow),
        new PropertyMetadata(SetLabelText));

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    private static void SetLabelText(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
        (d as DeviceInfoRow)!.InfoLabel.Content = e.NewValue;

    public static readonly DependencyProperty InfoTextBoxProperty = DependencyProperty.Register(
        nameof(InfotextBox),
        typeof(string),
        typeof(DeviceInfoRow),
        new PropertyMetadata(SetTextBoxData));

    public string InfotextBox
    {
        get => (string)GetValue(InfoTextBoxProperty);
        set => SetValue(LabelTextProperty, value);
    }

    private static void SetTextBoxData(DependencyObject d, DependencyPropertyChangedEventArgs e) =>
        (d as DeviceInfoRow)!.InfoLabel.Content = e.NewValue;

    public static readonly DependencyProperty EditCommandProperty = DependencyProperty.Register(
        "EditCommand",
        typeof(ICommand),
        typeof(DeviceInfoRow),
        new PropertyMetadata(null));

    public ICommand EditCommand
    {
        get => (ICommand)GetValue(EditCommandProperty);
        set => SetValue(EditCommandProperty, value);
    }
    public object EditCommandParameter
    {
        get => GetValue(EditCommandProperty);
        set => SetValue(EditCommandProperty, value);
    }

    public static readonly DependencyProperty OkCommandProperty = DependencyProperty.Register(
        "OkCommand",
        typeof(ICommand),
        typeof(DeviceInfoRow),
        new PropertyMetadata(null));

    public ICommand OkCommand
    {
        get => (ICommand)GetValue(OkCommandProperty);
        set => SetValue(OkCommandProperty, value);
    }
    public object OkCommandParameter
    {
        get => GetValue(OkCommandProperty);
        set => SetValue(OkCommandProperty, value);
    }

    public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register(
        "CancelCommand",
        typeof(ICommand),
        typeof(DeviceInfoRow),
        new PropertyMetadata(null));

    public ICommand CancelCommand
    {
        get => (ICommand)GetValue(CancelCommandProperty);
        set => SetValue(CancelCommandProperty, value);
    }

    public object CancelCommandParameter
    {
        get => GetValue(CancelCommandProperty);
        set => SetValue(CancelCommandProperty, value);
    }

    private bool _isEditButtonVisible;

    public bool IsEditButtonVisible
    {
        get => _isEditButtonVisible;
        set 
        {
            _isEditButtonVisible = value;
            OnPropertyChanged(nameof(IsEditButtonVisible));
        }
    }


    public DeviceInfoRow()
    {
        InitializeComponent();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
