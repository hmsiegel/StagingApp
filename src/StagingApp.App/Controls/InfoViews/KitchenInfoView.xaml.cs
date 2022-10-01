namespace StagingApp.Main.Controls.InfoViews;
/// <summary>
/// Interaction logic for KitchenInfoView.xaml
/// </summary>
public partial class KitchenInfoView : UserControl
{
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title),
        typeof(string),
        typeof(KitchenInfoView),
        new PropertyMetadata(OnTitlePropertyChanged));

    private static void OnTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d != null && d is KitchenInfoView infoView)
        {
            infoView.Title = (string)e.NewValue;
        }
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }


    public KitchenInfoView()
    {
        InitializeComponent();
        DataContext = new KitchenInfoViewModel();
    }
}
