namespace StagingApp.Presentation.ViewModels.InfoViewModels;
public sealed class TerminalInfoViewModel : Screen
{
    private string? _terminalName;
    private string? _terminalNumber;
    private string? _gateway;
    private string? _ipAddress;

    [Description("TERMINAL NAME:")]
    [Sort(SortOrder = 1)]
    public string? TerminalName
    {
        get => _terminalName;
        set
        {
            _terminalName = value;
            NotifyOfPropertyChange(() => TerminalName);
        }
    }

    [Description("TERMINAL NUMBER:")]
    [Sort(SortOrder = 2)]
    public string? TerminalNumber
    {
        get => _terminalNumber;
        set
        {
            _terminalNumber = value;
            NotifyOfPropertyChange(() => TerminalNumber);
        }
    }

    [Description("GATEWAY:")]
    [Sort(SortOrder = 4)]
    public string? Gateway
    {
        get => _gateway;
        set
        {
            _gateway = value;
            NotifyOfPropertyChange(() => Gateway);
        }
    }

    [Description("IP ADDRESS:")]
    [Sort(SortOrder = 3)]
    public string? IpAddress
    {
        get => _ipAddress;
        set
        {
            _ipAddress = value;
            NotifyOfPropertyChange(() => IpAddress);
        }
    }

    public TerminalInfoViewModel()
    {

    }

    public void OK()
    {
        throw new NotImplementedException();
    }
    public void Cancel()
    {
        throw new NotImplementedException();
    }
}
