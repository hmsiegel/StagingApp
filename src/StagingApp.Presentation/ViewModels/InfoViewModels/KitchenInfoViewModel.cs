namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public sealed class KitchenInfoViewModel : BaseInfoViewModel
{
    [Description("CONTROLLER NAME:")]
    public string? ControllerName { get; set; }

    [Description("CONTROLLER NUMBER:")]
    public string? ControllerNumber { get; set; }

    [Description("BOH IP ADDRESS:")]
    public string? BohIpAddress { get; set; }

    private bool _isControllerNameEditVisible;

    public bool IsControllerNameEditVisible
    {
        get { return _isControllerNameEditVisible; }
        set 
        {
            _isControllerNameEditVisible = value;
            NotifyOfPropertyChange(() => IsControllerNameEditVisible);
        }
    }

    private bool _isControllerNumberEditVisible;

    public bool IsControllerNumberEditVisible
    {
        get { return _isControllerNumberEditVisible; }
        set 
        {
            _isControllerNumberEditVisible = value;
            NotifyOfPropertyChange(() => IsControllerNumberEditVisible);
        }
    }

    private bool _isBohIpAddressEditVisible;

    public bool IsBohIpAddressEditVisible
    {
        get { return _isBohIpAddressEditVisible; }
        set 
        {
            _isBohIpAddressEditVisible = value;
            NotifyOfPropertyChange(() => IsBohIpAddressEditVisible);
        }
    }
    public KitchenInfoViewModel()
    {

    }

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


    public override void OK()
    {
        throw new NotImplementedException();
    }

    public override void ToggleButtonVisibility(string parameter, bool isVisible)
    {
        switch (parameter)
        {
            case nameof(ControllerName):
                IsControllerNameEditVisible = isVisible;
                break;
            case nameof(BohServerName):
                IsBohServerNameEditVisible = isVisible;
                break;
            case nameof(KeyNumber):
                IsKeyNumberEditVisible = isVisible;
                break;
            case nameof(IpAddress):
                IsIpAddressEditVisible = isVisible;
                break;
            case nameof(TermStr):
                IsTermStrEditVisible = isVisible;
                break;
            case nameof(ControllerNumber):
                IsControllerNumberEditVisible = isVisible;
                break;
            case nameof(BohIpAddress):
                IsBohIpAddressEditVisible = isVisible;
                break;
            default:
                break;
        }
    }
}
