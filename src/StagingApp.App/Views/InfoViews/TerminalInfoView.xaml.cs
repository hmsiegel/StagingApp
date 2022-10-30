namespace StagingApp.Main.Views.InfoViews;
/// <summary>
/// Interaction logic for TerminalInfoView.xaml
/// </summary>
public partial class TerminalInfoView : UserControl
{
    public TerminalInfoView()
    {
        InitializeComponent();
        DataContext = new TerminalInfoViewModel();
    }
}
