using StagingApp.Domain.Attributes;

namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public sealed class KitchenInfoViewModel : BaseInfoViewModel
{
    private string? _controllerName;
    private string? _controllerNumber;
    private string? _bohIpAddress;

    [Description("CONTROLLER NAME:")]
    [Sort(SortOrder = 1)]
    public string? ControllerName
    {
        get { return _controllerName; }
        set
        {
            _controllerName = value;
            NotifyOfPropertyChange(() => ControllerName);
        }
    }

    [Description("CONTROLLER NUMBER:")]
    [Sort(SortOrder = 2)]
    public string? ControllerNumber
    {
        get { return _controllerNumber; }
        set
        {
            _controllerNumber = value;
            NotifyOfPropertyChange(() => ControllerNumber);
        }
    }

    [Description("BOH IP ADDRESS:")]
    [Sort(SortOrder = 7)]
    public string? BohIpAddress
    {
        get { return _bohIpAddress; }
        set
        {
            _bohIpAddress = value;
            NotifyOfPropertyChange(() => BohIpAddress);
        }
    }

    public override void OK()
    {
        throw new NotImplementedException();
    }
}
