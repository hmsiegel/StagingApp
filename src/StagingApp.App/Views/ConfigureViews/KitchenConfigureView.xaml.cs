namespace StagingApp.Main.Views.ConfigureViews;
/// <summary>
/// Interaction logic for KitchenConfigureView.xaml
/// </summary>
public partial class KitchenConfigureView : UserControl
{
    public KitchenConfigureView()
    {
        InitializeComponent();
        DataContext = new KitchenConfigureViewModel();
    }
}
