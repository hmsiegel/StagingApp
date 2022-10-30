namespace StagingApp.Main.Views.ConfigureViews;
/// <summary>
/// Interaction logic for TerminalConfigureView.xaml
/// </summary>
public partial class TerminalConfigureView : UserControl
{
    public TerminalConfigureView()
    {
        InitializeComponent();
        DataContext = new TerminalConfigureViewModel();
    }
}
