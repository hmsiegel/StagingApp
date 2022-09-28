namespace StagingApp.Main.Controls.ConfigureViews;
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
