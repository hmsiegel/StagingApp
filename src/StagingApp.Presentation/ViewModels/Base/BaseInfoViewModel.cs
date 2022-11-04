namespace StagingApp.Presentation.ViewModels.Base;
public abstract class BaseInfoViewModel : Screen
{
    public string? Message { get; set; }


    private string? _ipAddress;
    private string? _bohServerName;
    private string? _keyNumber;
    private string? _numTerms;
    private string? _termStr;

    //private bool _isIpAddressEditVisible;
    //private bool _isKeyNumberEditVisible;
    //private bool _isTermStrEditVisible;
    //private bool _isBohServerNameEditVisible;

    [Description("IP ADDRESS:")]
    public string? IpAddress
    {
        get { return _ipAddress; }
        set 
        {
            _ipAddress = value;
            NotifyOfPropertyChange(() => IpAddress);
        }
    }

    [Description("BOH SERVER NAME:")]
    public string? BohServerName
    {
        get { return _bohServerName; }
        set 
        {
            _bohServerName = value;
            NotifyOfPropertyChange(() => BohServerName);
        }
    }

    [Description("KEY NUMBER")]
    public string? KeyNumber
    {
        get { return _keyNumber; }
        set 
        {
            _keyNumber = value;
            NotifyOfPropertyChange(() => KeyNumber);
        }
    }

    [Description("NUMTERMS:")]
    public string? NumTerms
    {
        get { return _numTerms; }
        set 
        {
            _numTerms = value;
            NotifyOfPropertyChange(() => NumTerms);
        }
    }

    [Description("TERMSTR:")]
    public string? TermStr
    {
        get { return _termStr; }
        set 
        {
            _termStr = value;
            NotifyOfPropertyChange(() => TermStr);
        }
    }


    //public bool IsIpAddressEditVisible
    //{
    //    get { return _isIpAddressEditVisible; }
    //    set 
    //    {
    //        _isIpAddressEditVisible = value;
    //        NotifyOfPropertyChange(() => IsIpAddressEditVisible);
    //    }
    //}

    //public bool IsKeyNumberEditVisible
    //{
    //    get { return _isKeyNumberEditVisible; }
    //    set 
    //    {
    //        _isKeyNumberEditVisible = value;
    //        NotifyOfPropertyChange(() => IsKeyNumberEditVisible);
    //    }
    //}

    //public bool IsTermStrEditVisible
    //{
    //    get { return _isTermStrEditVisible; }
    //    set 
    //    {
    //        _isTermStrEditVisible = value;
    //        NotifyOfPropertyChange(() => IsTermStrEditVisible);
    //    }
    //}

    //public bool IsBohServerNameEditVisible
    //{
    //    get { return _isBohServerNameEditVisible; }
    //    set 
    //    {
    //        _isBohServerNameEditVisible = value;
    //        NotifyOfPropertyChange(() => IsBohServerNameEditVisible);
    //    }
    //}

    public abstract void OK();
    public void Cancel()
    {
        TryCloseAsync();
    }

    //public void Edit(string parameter)
    //{
    //    bool isVisible = true;
    //    ToggleButtonVisibility(parameter, isVisible);
    //}
    //public void OkEdit(string parameter)
    //{
    //    bool isVisible = false;
    //    ToggleButtonVisibility(parameter, isVisible);
    //}
    //public void CancelEdit(string parameter)
    //{
    //    bool isVisible = false;
    //    ToggleButtonVisibility(parameter, isVisible);
    //}

    //public abstract void ToggleButtonVisibility(string parameter, bool isVisible);
}
