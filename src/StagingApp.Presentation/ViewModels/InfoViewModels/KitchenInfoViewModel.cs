namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public sealed class KitchenInfoViewModel : BaseInfoViewModel
{
    private string? _controllerName;
    private string? _controllerNumber;
    private string? _bohIpAddress;

    //private bool _isControllerNameEditVisible;
    //private bool _isControllerNumberEditVisible;
    //private bool _isBohIpAddressEditVisible;

    [Description("CONTROLLER NAME:")]
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
    public string? BohIpAddress
    {
        get { return _bohIpAddress; }
        set 
        {
            _bohIpAddress = value;
            NotifyOfPropertyChange(() => BohIpAddress);
        }
    }

    //public bool IsControllerNameEditVisible
    //{
    //    get { return _isControllerNameEditVisible; }
    //    set 
    //    {
    //        _isControllerNameEditVisible = value;
    //        NotifyOfPropertyChange(() => IsControllerNameEditVisible);
    //    }
    //}

    //public bool IsControllerNumberEditVisible
    //{
    //    get { return _isControllerNumberEditVisible; }
    //    set 
    //    {
    //        _isControllerNumberEditVisible = value;
    //        NotifyOfPropertyChange(() => IsControllerNumberEditVisible);
    //    }
    //}

    //public bool IsBohIpAddressEditVisible
    //{
    //    get { return _isBohIpAddressEditVisible; }
    //    set 
    //    {
    //        _isBohIpAddressEditVisible = value;
    //        NotifyOfPropertyChange(() => IsBohIpAddressEditVisible);
    //    }
    //}

    public override void OK()
    {
        throw new NotImplementedException();
    }

    //public override void ToggleButtonVisibility(string parameter, bool isVisible)
    //{
    //    switch (parameter)
    //    {
    //        case nameof(ControllerName):
    //            IsControllerNameEditVisible = isVisible;
    //            break;
    //        case nameof(BohServerName):
    //            IsBohServerNameEditVisible = isVisible;
    //            break;
    //        case nameof(KeyNumber):
    //            IsKeyNumberEditVisible = isVisible;
    //            break;
    //        case nameof(IpAddress):
    //            IsIpAddressEditVisible = isVisible;
    //            break;
    //        case nameof(TermStr):
    //            IsTermStrEditVisible = isVisible;
    //            break;
    //        case nameof(ControllerNumber):
    //            IsControllerNumberEditVisible = isVisible;
    //            break;
    //        case nameof(BohIpAddress):
    //            IsBohIpAddressEditVisible = isVisible;
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
