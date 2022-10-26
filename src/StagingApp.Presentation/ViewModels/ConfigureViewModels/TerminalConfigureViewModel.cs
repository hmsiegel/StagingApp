namespace StagingApp.Presentation.ViewModels.ConfigureViewModels;
public sealed class TerminalConfigureViewModel : BaseConfigureViewModel
{
    private string? _terminalName;
    private string? _ipAddress;

    [Description("TERMINAL NAME:")]
    public string? TerminalName
    {
        get => _terminalName;
        set
        {
            _terminalName = value;
            NotifyOfPropertyChange(() => TerminalName);
        }
    }

    [Description("IP ADDRESS:")]
    public string? IpAddress
    {
        get => _ipAddress;
        set
        {
            _ipAddress = value;
            NotifyOfPropertyChange(() => IpAddress);
        }
    }

    public override void Configure()
    {
        throw new NotImplementedException();
    }

    public override void ValidateInput()
    {
        throw new NotImplementedException();
    }
}
