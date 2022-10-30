namespace StagingApp.Presentation.ViewModels.Base;
public abstract class BaseInfoViewModel : Screen
{
    public string? Message { get; set; }

    [Description("IP ADDRESS:")]
    public string? IpAddress { get; set; }

    [Description("BOH SERVER NAME:")]
    public string? BohServerName { get; set; }

    [Description("KEY NUMBER")]
    public string? KeyNumber { get; set; }

    [Description("NUMTERMS:")]
    public string? NumTerms { get; set; }

    [Description("TERMSTR:")]
    public string? TermStr { get; set; }

    private bool _isIpAddressEditVisible;

    public bool IsIpAddressEditVisible
    {
        get { return _isIpAddressEditVisible; }
        set 
        {
            _isIpAddressEditVisible = value;
            NotifyOfPropertyChange(() => IsIpAddressEditVisible);
        }
    }
    private bool _isKeyNumberEditVisible;

    public bool IsKeyNumberEditVisible
    {
        get { return _isKeyNumberEditVisible; }
        set 
        {
            _isKeyNumberEditVisible = value;
            NotifyOfPropertyChange(() => IsKeyNumberEditVisible);
        }
    }
    private bool _isTermStrEditVisible;

    public bool IsTermStrEditVisible
    {
        get { return _isTermStrEditVisible; }
        set 
        {
            _isTermStrEditVisible = value;
            NotifyOfPropertyChange(() => IsTermStrEditVisible);
        }
    }
    private bool _isBohServerNameEditVisible;

    public bool IsBohServerNameEditVisible
    {
        get { return _isBohServerNameEditVisible; }
        set 
        {
            _isBohServerNameEditVisible = value;
            NotifyOfPropertyChange(() => IsBohServerNameEditVisible);
        }
    }

    public abstract void OK();
    public void Cancel()
    {
        TryCloseAsync();
    }

    public void Edit(string parameter)
    {
        bool isVisible = true;
        ToggleButtonVisibility(parameter, isVisible);
    }
    public void OkEdit(string parameter)
    {
        bool isVisible = false;
        ToggleButtonVisibility(parameter, isVisible);
    }
    public void CancelEdit(string parameter)
    {
        bool isVisible = false;
        ToggleButtonVisibility(parameter, isVisible);
    }

    public abstract void ToggleButtonVisibility(string parameter, bool isVisible);
}
