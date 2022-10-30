namespace StagingApp.Main.Views.InfoViews;
/// <summary>
/// Interaction logic for KitchenInfoView.xaml
/// </summary>
public partial class KitchenInfoView : UserControl
{
    public KitchenInfoView()
    {
        InitializeComponent();
        DataContext = new KitchenInfoViewModel();
    }
}
