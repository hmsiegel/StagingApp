namespace StagingApp.Controls.Library.Custom;
public class DescriptionInfoControl : DescriptionControl, INotifyPropertyChanged
{
    public Visibility IsEditButtonVisible
    {
        get { return (Visibility)GetValue(MyPropertyProperty); }
        set
        {
            SetValue(MyPropertyProperty, value);
            OnPropertyChanged(nameof(IsEditButtonVisible));
        }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MyPropertyProperty =
        DependencyProperty.Register(
            nameof(IsEditButtonVisible),
            typeof(Visibility),
            typeof(DescriptionInfoControl),
            new PropertyMetadata(false));

    private void EditButton()
    {

    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
