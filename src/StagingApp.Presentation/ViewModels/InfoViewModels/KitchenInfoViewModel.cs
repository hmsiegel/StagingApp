using StagingApp.Controls.Library.Models;
using System.ComponentModel;
using System.Windows;

namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public partial class KitchenInfoViewModel : OkCancelCommandsVM
{
    [ObservableProperty]
    [Description("Controller Name")]
    private string? _controllerName;

    [ObservableProperty]
    [Description("Controller Number")]
    private string? _controllerNumber;

    [ObservableProperty]
    [Description("BOH ServerName")]
    private string? _bohServerName;

    [ObservableProperty]
    [Description("TERMSTR")]
    private string? _termStr;

    [ObservableProperty]
    [Description("Key Number")]
    private string? _keyNumber;

    [ObservableProperty]
    [Description("IP Address")]
    private string? _ipAddress;

    [ObservableProperty]
    [Description("BOH IP Address")]
    private string? _bohIpAddress;

}
