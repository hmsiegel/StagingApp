namespace StagingApp.Presentation.ViewModels.ConfigureViewModels;
public sealed class TerminalConfigureViewModel : BaseConfigureViewModel
{
    private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    private string? _terminalName;
    private string? _ipAddress;

    public TerminalConfigureViewModel()
    {

    }

    protected override void OnViewLoaded(object view)
    {
        base.OnViewLoaded(view);
        CheckForMarkerFiles();
    }

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

    public override void ValidateInput()
    {
        throw new NotImplementedException();
        // TODO: Implement data input validation
    }

    public override void Configure()
    {
        throw new NotImplementedException();
        // TODO: Implement the configure method to start the configuration
    }
}
