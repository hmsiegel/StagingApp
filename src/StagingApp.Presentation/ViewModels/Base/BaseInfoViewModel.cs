namespace StagingApp.Presentation.ViewModels.Base;
public abstract class BaseInfoViewModel : Screen
{
    public string? Message { get; set; }


    private string? _ipAddress;
    private string? _bohServerName;
    private string? _keyNumber;
    private string? _numTerms;
    private string? _termStr;

    [Description("IP ADDRESS:")]
    [Sort(SortOrder = 3)]
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
    [Sort(SortOrder = 6)]
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
    [Sort(SortOrder = 3)]
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
    [Sort(SortOrder = 4)]
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
    [Sort(SortOrder = 5)]
    public string? TermStr
    {
        get { return _termStr; }
        set 
        {
            _termStr = value;
            NotifyOfPropertyChange(() => TermStr);
        }
    }

    public abstract void OK();
    public void Cancel()
    {
        TryCloseAsync();
    }
}
