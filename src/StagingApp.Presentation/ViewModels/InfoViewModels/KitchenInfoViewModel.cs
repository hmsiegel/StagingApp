namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public sealed class KitchenInfoViewModel : BaseInfoViewModel
{
    [Description("Controller Name:")]
    public string? ControllerName { get; set; }

    [Description("Controller Number:")]
    public string? ControllerNumber { get; set; }

    [Description("BOH IP Address:")]
    public string? BohIpAddress { get; set; }

    public KitchenInfoViewModel(
        string? controllerName,
        string? controllerNumber,
        string? ipAddress,
        string? keyNumber,
        string? termStr,
        string? bohServerName,
        string? bohIpAddress,
        string? message)
    {
        ControllerName = controllerName;
        ControllerNumber = controllerNumber;
        IpAddress = ipAddress;
        KeyNumber = keyNumber;
        TermStr = termStr;
        BohServerName = bohServerName;
        BohIpAddress = bohIpAddress;
        Message = message;
    }


    public override void Ok()
    {
        throw new NotImplementedException();
    }
}
