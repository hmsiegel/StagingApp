namespace StagingApp.Main.Controls.InfoViews;
/// <summary>
/// Interaction logic for ServerInfoView.xaml
/// </summary>
public partial class ServerInfoView : UserControl
{
    public ServerInfoView()
    {
        InitializeComponent();
        DataContext = new ServerInfoViewModel();
    }
}
