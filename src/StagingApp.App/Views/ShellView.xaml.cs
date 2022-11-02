using System.Windows;

namespace StagingApp.Main.Views;
/// <summary>
/// Interaction logic for ShellView.xaml
/// </summary>
public partial class ShellView : Window
{
    public ShellView()
    {
        InitializeComponent();

        // This simple initialization needs to be done in XAML
        // DataContext = new ShellViewModel();
    }
}
